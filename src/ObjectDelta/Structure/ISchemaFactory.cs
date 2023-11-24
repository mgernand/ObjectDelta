namespace ObjectDelta.Structure
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a factory that creates <see cref="StructureSchema" /> instances´.
	/// </summary>
	[PublicAPI]
	public interface ISchemaFactory
	{
		/// <summary>
		///     Creates a <see cref="StructureSchema" /> for the given <see cref="StructureType" />.
		/// </summary>
		/// <param name="structureType"></param>
		/// <returns></returns>
		StructureSchema CreateSchema(StructureType structureType);
	}
}
