namespace ObjectStructure
{
	using System;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructureIndex
	{
		internal StructureIndex(string name, string path, object value, Type type)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(path, nameof(path));
			Guard.Against.Null(type, nameof(type));

			this.Name = name;
			this.Path = path;
			this.Value = value;
			this.Type = type;
			this.IsNumeric = type.IsNumeric();
		}

		public string Name { get; }

		public string Path { get; }

		public object Value { get; }

		public Type Type { get; }

		public bool IsNumeric { get; }
	}
}
