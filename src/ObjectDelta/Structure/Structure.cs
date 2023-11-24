namespace ObjectDelta.Structure
{
	using global::ObjectDelta.Utilities;
	using JetBrains.Annotations;

	/// <summary>
	///     The result of the <see cref="IStructureBuilder" /> that contains the <see cref="StructureSchema" />
	///     and the optional <see cref="StructureIndices" />.
	/// </summary>
	[PublicAPI]
	public sealed class Structure
	{
		internal Structure(StructureSchema schema, StructureIndices indices = null)
		{
			Guard.ThrowIfNull(schema, nameof(schema));

			this.Schema = schema;
			this.Indices = indices ?? new StructureIndices();
		}

		/// <summary>
		///     Gets the name of the schema.
		/// </summary>
		public string Name => this.Schema.Name;

		/// <summary>
		///     Gets the schema.
		/// </summary>
		public StructureSchema Schema { get; }

		/// <summary>
		///     Gets the indices.
		/// </summary>
		public StructureIndices Indices { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Schema.Name;
		}
	}
}
