namespace ObjectDelta.Structure
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a factory that creates <see cref="StructureIndices" /> instances.
	/// </summary>
	[PublicAPI]
	public interface IStructureIndicesFactory
	{
		/// <summary>
		///     Creates the <see cref="StructureIndices" /> for the given <see cref="StructureSchema" /> and instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="structureSchema"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		StructureIndices CreateIndices<T>(StructureSchema structureSchema, T item);
	}
}
