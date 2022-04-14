namespace ObjectStructure
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructureSchema
	{
		internal StructureSchema(StructureType structureType, IndexAccessor[] indexAccessors)
		{
			Guard.Against.Null(structureType, nameof(structureType));

			this.StructureType = structureType;
			this.IndexAccessors = indexAccessors ?? Array.Empty<IndexAccessor>();
		}

		public string Name => this.StructureType.Name;

		public StructureType StructureType { get; }

		internal IndexAccessor[] IndexAccessors { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.StructureType.ToString();
		}
	}
}
