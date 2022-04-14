namespace ObjectStructure
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructurePropertyInfo
	{
		internal StructurePropertyInfo(string name, Type type, Attribute[] attributes, StructureProperty parent = null)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.Null(type, nameof(type));
			Guard.Against.Null(attributes, nameof(attributes));

			this.Name = name;
			this.Type = type;
			this.Attributes = attributes;
			this.Parent = parent;
		}

		public string Name { get; }

		public Type Type { get; }

		public Attribute[] Attributes { get; }

		public StructureProperty Parent { get; }
	}
}
