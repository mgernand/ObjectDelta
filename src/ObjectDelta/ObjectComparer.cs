namespace ObjectDelta
{
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ObjectComparer
	{
		public ObjectDelta<T> Compare<T>(T firstObject, T secondObject) where T : class
		{
			//IStructureBuilder builder = new StructureBuilder();

			//Structure firstObjectStructure = builder.CreateStructure(firstObject);
			//Structure secondObjectStructure = builder.CreateStructure(secondObject);

			//IList<PropertyDelta> propertyDeltas = firstObjectStructure.Indices
			//	.Zip(secondObjectStructure.Indices, (first, second) =>
			//	{
			//		if((first.Value == null) && (second.Value == null))
			//		{
			//			return null;
			//		}

			//		if(ReferenceEquals(first.Value, second.Value))
			//		{
			//			return null;
			//		}

			//		if(Equals(first.Value, second.Value))
			//		{
			//			return null;
			//		}

			//		if(Equals(first, second))
			//		{
			//			return null;
			//		}

			//		return new PropertyDelta(first.Type, first.Name, first.Path, first.Value, second.Value);
			//	})
			//	.Where(propertyDelta => propertyDelta != null)
			//	.ToList();

			//return new ObjectDelta<T>(firstObject, secondObject, propertyDeltas.ToArray());
			return null;
		}
	}
}
