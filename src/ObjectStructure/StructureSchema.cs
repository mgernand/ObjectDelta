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

		public StructureType StructureType { get; }

		public IndexAccessor[] IndexAccessors { get; }

		public string Name => this.StructureType.Name;
	}
}
