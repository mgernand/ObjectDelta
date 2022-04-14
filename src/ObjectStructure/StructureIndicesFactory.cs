namespace ObjectStructure
{
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructureIndicesFactory : IStructureIndicesFactory
	{
		/// <inheritdoc />
		public StructureIndex[] CreateIndices<T>(StructureSchema structureSchema, T item)
		{
			IList<StructureIndex> result = new List<StructureIndex>();

			foreach(IndexAccessor indexAccessor in structureSchema.IndexAccessors)
			{
				StructureIndexValue[] values = indexAccessor.GetValues(item);

				// TODO
			}

			return result.ToArray();
		}
	}
}
