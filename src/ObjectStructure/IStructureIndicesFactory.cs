namespace ObjectStructure
{
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IStructureIndicesFactory
	{
		StructureIndex[] CreateIndices<T>(StructureSchema structureSchema, T item);
	}
}
