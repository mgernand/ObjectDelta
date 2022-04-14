namespace ObjectStructure
{
	using JetBrains.Annotations;

	[PublicAPI]
	public interface ISchemaFactory
	{
		StructureSchema CreateSchema(StructureType structureType);
	}
}
