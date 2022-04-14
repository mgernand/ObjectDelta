namespace ObjectStructure
{
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class Structure
	{
		internal Structure(StructureSchema schema, StructureIndex[] indices = null)
		{
			Guard.Against.Null(schema, nameof(schema));

			this.Schema = schema;
			this.Indices = new StructureIndices(indices);
		}

		public string Name => this.Schema.Name;

		public StructureSchema Schema { get; }

		public StructureIndices Indices { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Schema.Name;
		}
	}
}
