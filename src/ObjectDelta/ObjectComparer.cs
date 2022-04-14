namespace ObjectDelta
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using ObjectStructure;

	[PublicAPI]
	public sealed class ObjectComparer
	{
		public ObjectDelta<T> Compare<T>(T firstObject, T secondObject) where T : class
		{
			IStructureBuilder builder = new StructureBuilder();

			Structure firstObjectStructure = builder.CreateStructure(firstObject);
			Structure secondObjectStructure = builder.CreateStructure(secondObject);

			IDictionary<string, StructureIndex> firstDict = firstObjectStructure.Indices.ToDictionary(x => x.Path, x => x);
			IDictionary<string, StructureIndex> secondDict = secondObjectStructure.Indices.ToDictionary(x => x.Path, x => x);

			foreach(string key in firstDict.Keys)
			{
				if(!secondDict.ContainsKey(key))
				{
					Console.WriteLine("Index available in first but missing in second.");
				}
			}

			foreach(string key in secondDict.Keys)
			{
				if(!firstDict.ContainsKey(key))
				{
					Console.WriteLine("Index available in second but missing in first.");
				}
			}

			IList<PropertyDelta> propertyDeltas = firstObjectStructure.Indices
				.Zip(secondObjectStructure.Indices, (first, second) =>
				{
					if((first.Value == null) && (second.Value == null))
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

					return new PropertyDelta(first.Type, first.Name, first.Path, first.Value, second.Value);
				})
				.Where(propertyDelta => propertyDelta != null)
				.ToList();

			return new ObjectDelta<T>(firstObject, secondObject, propertyDeltas.ToArray());
		}
	}
}
