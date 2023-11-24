namespace ObjectDelta.Structure
{
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StructureIndicesFactory : IStructureIndicesFactory
	{
		/// <inheritdoc />
		public StructureIndices CreateIndices<T>(StructureSchema structureSchema, T item)
		{
			IList<StructureIndex> result = new List<StructureIndex>();

			foreach(IndexAccessor indexAccessor in structureSchema.IndexAccessors)
			{
				StructureIndexValue[] values = indexAccessor.GetValues(item);

				foreach(StructureIndexValue value in values)
				{
					result.Add(new StructureIndex(indexAccessor.Name, value.Path, indexAccessor.Type, value.Value));
				}
			}

			return new StructureIndices(result.ToArray());
		}
	}
}
