namespace ObjectStructure
{
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IStructureBuilder
	{
		Structure CreateStructure<T>(T item);

		Structure CreateStructure<T>();
	}
}
