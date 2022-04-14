namespace ObjectStructure
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class Structure
	{
		internal Structure(string name, StructureIndex[] indices)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			this.Name = name;
			this.Indices = indices ?? Array.Empty<StructureIndex>();
		}

		public string Name { get; }

		public StructureIndex[] Indices { get; }
	}
}
