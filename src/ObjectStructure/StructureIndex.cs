namespace ObjectStructure
{
	using System;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructureIndex
	{
		internal StructureIndex(string name, string path, Type type, object value)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(path, nameof(path));
			Guard.Against.Null(type, nameof(type));

			this.Name = name;
			this.Path = path;
			this.Type = type;
			this.Value = value;

			this.IsNumeric = type.IsNumeric();
			this.IsPrimitive = type.IsPrimitive();
			// TODO
		}

		public string Name { get; }

		public string Path { get; }

		public Type Type { get; }

		public object Value { get; }

		public bool IsNumeric { get; }

		public bool IsPrimitive { get; }

		public bool IsComplex { get; set; }

		public bool IsEnumerable { get; set; }

		private string ValueDisplayString
		{
			get
			{
				if(this.Value == null)
				{
					return "null";
				}

				return $@"""{this.Value}""";
			}
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{this.Path} = {this.ValueDisplayString}";
		}
	}
}
