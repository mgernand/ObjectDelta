namespace ObjectStructure
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class IndexAccessor
	{
		private readonly StructurePropertyCallStack callStack;
		private readonly StructureProperty property;

		internal IndexAccessor(StructureProperty property)
		{
			Guard.Against.Null(property, nameof(property));

			this.property = property;
			this.callStack = StructurePropertyCallStack.Create(property);
		}

		public string Name => this.property.Name;

		public string Path => this.property.Path;

		public Type Type => this.property.Type;

		public bool IsSimple => this.property.IsSimple;

		public bool IsComplex => this.property.IsComplex;

		public bool IsEnumerable => this.property.IsEnumerable;

		public bool IsElement => this.property.IsElement;

		internal StructureIndexValue[] GetValues<T>(T item)
		{
			if(this.property.IsRootProperty)
			{
				object rootPropertyValue = this.property.GetValue(item);

				if(this.property.IsEnumerable)
				{
					return CollectionOfValuesToList(rootPropertyValue as IEnumerable, this.property).ToArray();
				}

				return new StructureIndexValue[]
				{
					new StructureIndexValue(this.property.Path, rootPropertyValue)
				};
			}

			return this.EvaluateCallStack(item).ToArray();
		}

		private IEnumerable<StructureIndexValue> EvaluateCallStack<T>(T startNode, int startIndex = 0, string startPath = null)
		{
			startPath ??= this.callStack[0].Name;

			object currentNode = startNode;
			int maxIndex = this.callStack.Length - 1;
			for(int index = startIndex; index < this.callStack.Length; index++)
			{
				if(currentNode == null)
				{
					return null;
				}

				StructureProperty currentProperty = this.callStack[index];

				IEnumerable enumerableNode = currentNode as IEnumerable;
				bool isLastProperty = index == maxIndex;
				if(isLastProperty)
				{
					return enumerableNode != null
						? ExtractValuesForEnumerableNode(enumerableNode, currentProperty, startPath)
						: ExtractValuesForSimpleNode(currentNode, currentProperty, startPath);
				}

				if(enumerableNode == null)
				{
					currentNode = currentProperty.GetValue(currentNode);
				}
				else
				{
					IList<StructureIndexValue> values = new List<StructureIndexValue>();
					int i = -1;
					foreach(object node in enumerableNode)
					{
						i += 1;

						if(node != null)
						{
							IEnumerable<StructureIndexValue> tempValues = this.EvaluateCallStack(
								currentProperty.GetValue(node),
								index + 1,
								$"{currentProperty.Parent.Path}[{i}].{currentProperty.Name}");

							if(tempValues != null)
							{
								values.AddRange(tempValues);
							}
						}
					}

					return values;
				}
			}

			return null;
		}

		private static IList<StructureIndexValue> ExtractValuesForEnumerableNode(IEnumerable nodes, StructureProperty property, string startPath)
		{
			IList<StructureIndexValue> values = nodes is ICollection collection
				? new List<StructureIndexValue>(collection.Count)
				: new List<StructureIndexValue>();

			int i = -1;
			foreach(object node in nodes)
			{
				i += 1;
				string path = $"{startPath}[{i}]";

				if(node != null)
				{
					object nodeValue = property.GetValue(node);

					if(nodeValue is IEnumerable enumerableNode && !(nodeValue is string))
					{
						values.AddRange(CollectionOfValuesToList(enumerableNode, property, path));
					}
					else
					{
						values.Add(new StructureIndexValue($"{path}.{property.Name}", nodeValue));
					}
				}
			}

			return values;
		}

		private static IList<StructureIndexValue> ExtractValuesForSimpleNode(object node, StructureProperty property, string startPath)
		{
			object currentValue = property.GetValue(node);

			if(!property.IsEnumerable)
			{
				return new List<StructureIndexValue>
				{
					new StructureIndexValue($"{startPath}.{property.Name}", currentValue)
				};
			}

			return CollectionOfValuesToList(currentValue as IEnumerable, property, startPath);
		}

		private static IList<StructureIndexValue> CollectionOfValuesToList(IEnumerable elements, StructureProperty property, string startPath = null)
		{
			IList<StructureIndexValue> values = elements is ICollection collection
				? new List<StructureIndexValue>(collection.Count + 1)
				: new List<StructureIndexValue>();

			if(startPath != null)
			{
				startPath = $"{startPath}.";
			}

			values.Add(new StructureIndexValue($"{startPath}{property.Name}", elements));

			int i = 0;
			foreach(object element in elements)
			{
				values.Add(new StructureIndexValue($"{startPath}{property.Name}[{i}]", element));
				i += 1;
			}

			return values;
		}
	}
}
