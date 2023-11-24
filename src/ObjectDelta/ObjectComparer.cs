namespace ObjectDelta
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::ObjectDelta.Structure;
	using global::ObjectDelta.Utilities;
	using JetBrains.Annotations;

	/// <summary>
	///     A comparer that creates the delta between to instances.
	/// </summary>
	[PublicAPI]
	public static class ObjectComparer
	{
		/// <summary>
		///     Compares the given instances and creates the delta between them.
		/// </summary>
		/// <param name="firstObject"></param>
		/// <param name="secondObject"></param>
		/// <returns></returns>
		public static ObjectDelta Compare(object firstObject, object secondObject)
		{
			ObjectDelta<object> objectDelta = Compare<object>(firstObject, secondObject);
			return new ObjectDelta(firstObject.GetType(), objectDelta.OldObject, objectDelta.NewObject, objectDelta.PropertyDeltas);
		}

		/// <summary>
		///     Compares the given instances and creates the delta between them.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="firstObject"></param>
		/// <param name="secondObject"></param>
		/// <returns></returns>
		public static ObjectDelta<T> Compare<T>(T firstObject, T secondObject) where T : class
		{
			Guard.ThrowIfNull(firstObject);
			Guard.ThrowIfNull(secondObject);

			IStructureBuilder builder = new StructureBuilder();

			Structure.Structure firstObjectStructure = builder.CreateStructure(firstObject);
			Structure.Structure secondObjectStructure = builder.CreateStructure(secondObject);

			IList<PropertyDelta> nulledComplexDeltas = new List<PropertyDelta>();

			IEnumerable<PropertyDelta> propertyDeltas = firstObjectStructure.Indices
				.Zip(secondObjectStructure.Indices, (first, second) =>
				{
					if(first.Value == null && second.Value == null)
					{
						return null;
					}

					if(ReferenceEquals(first.Value, second.Value))
					{
						return null;
					}

					if(Equals(first.Value, second.Value))
					{
						return null;
					}

					if(Equals(first, second))
					{
						return null;
					}

					PropertyDelta propertyDelta = new PropertyDelta(first.Type, first.Name, first.Path, first.Value, second.Value);

					// A complex type property has been set to null.
					if(first.IsComplex && first.Value != null && second.Value == null)
					{
						nulledComplexDeltas.Add(propertyDelta);
					}

					// A nested property of a complex type property has been set to null.
					foreach(PropertyDelta nulledPropertyDelta in nulledComplexDeltas)
					{
						if(propertyDelta.Path.StartsWith($"{nulledPropertyDelta.Name}."))
						{
							return null;
						}
					}

					return propertyDelta;
				})
				.Where(propertyDelta => propertyDelta != null)
				.ToList();

			foreach(PropertyDelta propertyDelta in nulledComplexDeltas)
			{
				propertyDeltas = propertyDeltas.Where(x => !x.Path.StartsWith($"{propertyDelta.Name}."));
			}

			return new ObjectDelta<T>(firstObject, secondObject, propertyDeltas.ToArray());
		}
	}
}
