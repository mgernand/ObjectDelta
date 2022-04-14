namespace ObjectStructure
{
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructureBuilder : IStructureBuilder
	{
		private readonly ISchemaFactory schemaFactory;
		private readonly IStructureIndicesFactory structureIndicesFactory;
		private readonly IStructureTypeFactory structureTypeFactory;

		public StructureBuilder(
			IStructureTypeFactory structureTypeFactory = null,
			ISchemaFactory schemaFactory = null,
			IStructureIndicesFactory structureIndicesFactory = null)
		{
			this.structureTypeFactory = structureTypeFactory ?? new StructureTypeFactory();
			this.schemaFactory = schemaFactory ?? new SchemaFactory();
			this.structureIndicesFactory = structureIndicesFactory ?? new StructureIndicesFactory();
		}

		/// <inheritdoc />
		public Structure CreateStructure<T>(T item)
		{
			Guard.Against.Null(item, nameof(item));

			StructureType structureType = this.structureTypeFactory.CreateType<T>();
			StructureSchema structureSchema = this.schemaFactory.CreateSchema(structureType);

			StructureIndex[] structureIndices = this.CreateIndices(structureSchema, item);
			return new Structure(structureSchema, structureIndices);
		}

		/// <inheritdoc />
		public Structure CreateStructure<T>()
		{
			StructureType structureType = this.structureTypeFactory.CreateType<T>();
			StructureSchema structureSchema = this.schemaFactory.CreateSchema(structureType);

			return new Structure(structureSchema);
		}

		private StructureIndex[] CreateIndices<T>(StructureSchema structureSchema, T item)
		{
			return this.structureIndicesFactory.CreateIndices(structureSchema, item);
		}
	}
}
