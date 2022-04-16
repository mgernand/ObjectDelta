namespace ObjectDelta
{
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using ObjectStructure;

	/// <summary>
	///     A comparer that creates the delta between to instances.
	/// </summary>
	[PublicAPI]
	public static class ObjectComparer
	{
		/// <summary>
		///     Compares the given instances and creates the delta between them.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="firstObject"></param>
		/// <param name="secondObject"></param>
		/// <returns></returns>
		public static ObjectDelta<T> Compare<T>(T firstObject, T secondObject) where T : class
		{
			IStructureBuilder builder = new StructureBuilder();

			Structure firstObjectStructure = builder.CreateStructure(firstObject);
			Structure secondObjectStructure = builder.CreateStructure(secondObject);

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

					return new PropertyDelta(
						first.Type,
						first.Name,
						first.Path,
						first.Value,
						second.Value);
				})
				.Where(propertyDelta => propertyDelta != null)
				.ToList();

			return new ObjectDelta<T>(firstObject, secondObject, propertyDeltas.ToArray());
		}
	}
}
