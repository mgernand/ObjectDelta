namespace ObjectDelta
{
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using Structurizer;

	[PublicAPI]
	public sealed class ObjectComparer
	{
		public ObjectDelta<T> Compare<T>(T firstObject, T secondObject) where T : class
		{
			IStructureBuilder builder = StructureBuilderCache<T>.Builder;

			IStructure firstObjectStructure = builder.CreateStructure(firstObject);
			IStructure secondObjectStructure = builder.CreateStructure(secondObject);

			IList<PropertyDelta> propertyDeltas = firstObjectStructure.Indexes
				.Zip(secondObjectStructure.Indexes, (first, second) =>
				{
					if((first.Value == null) && (second.Value == null))
					{
						return null;
					}

					if(ReferenceEquals(first.Value, second.Value))
					{
						return null;
					}

					if(Equals(first, second))
					{
						return null;
					}

					return new PropertyDelta(first.DataType, first.Name, first.Path, first.Value, second.Value);
				})
				.Where(propertyDelta => propertyDelta != null)
				.ToList();

			return new ObjectDelta<T>(firstObject, secondObject, propertyDeltas.ToArray());
		}
	}
}
