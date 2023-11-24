namespace ObjectDelta.Structure
{
	using System;
	using global::ObjectDelta.Utilities;
	using JetBrains.Annotations;

	/// <summary>
	///     Contains the schema of a <see cref="StructureType" />.
	/// </summary>
	[PublicAPI]
	public sealed class StructureSchema
	{
		/// <summary>
		///     Creates a new instance of the <see cref="StructureSchema" /> type.
		/// </summary>
		/// <param name="structureType"></param>
		/// <param name="indexAccessors"></param>
		internal StructureSchema(StructureType structureType, IndexAccessor[] indexAccessors)
		{
			Guard.ThrowIfNull(structureType);

			this.StructureType = structureType;
			this.IndexAccessors = indexAccessors ?? Array.Empty<IndexAccessor>();
		}

		/// <summary>
		///     Gets the name of the type.
		/// </summary>
		public string Name => this.StructureType.Name;

		/// <summary>
		///     Gets the structure type.
		/// </summary>
		public StructureType StructureType { get; }

		/// <summary>
		///     Gets the index accessors of all properties.
		/// </summary>
		internal IndexAccessor[] IndexAccessors { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.StructureType.ToString();
		}
	}
}
