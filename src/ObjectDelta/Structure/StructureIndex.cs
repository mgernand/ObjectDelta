namespace ObjectDelta.Structure
{
	using System;
	using global::ObjectDelta.Structure.Reflection;
	using global::ObjectDelta.Utilities;
	using JetBrains.Annotations;

	/// <summary>
	///     A structure index that holds the flat path to the value and the value itself.
	/// </summary>
	[PublicAPI]
	public sealed class StructureIndex
	{
		/// <summary>
		///     Creates a new instance of the <see cref="StructureIndex" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		internal StructureIndex(string name, string path, Type type, object value)
		{
			Guard.ThrowIfNullOrWhiteSpace(name);
			Guard.ThrowIfNullOrWhiteSpace(path);
			Guard.ThrowIfNull(type);

			this.Name = name;
			this.Path = path;
			this.Type = type;
			this.Value = value;

			this.IsNumeric = type.IsNumeric();
			this.IsPrimitive = type.IsPrimitive();
			this.IsComplex = !type.IsPrimitive() && !type.IsEnumerableType();
			this.IsEnumerable = type.IsEnumerableType();
		}

		/// <summary>
		///     Gets the name of the property.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     Gets the path to the value of the property.
		/// </summary>
		public string Path { get; }

		/// <summary>
		///     Gets the type of the value.
		/// </summary>
		public Type Type { get; }

		/// <summary>
		///     Gets the value.
		/// </summary>
		public object Value { get; }

		/// <summary>
		///     Flag, indicating if the value is numeric.
		/// </summary>
		public bool IsNumeric { get; }

		/// <summary>
		///     Flag, indicating if the value is a primitive.
		/// </summary>
		public bool IsPrimitive { get; }

		/// <summary>
		///     Flag, indicating if the value is a complex type.
		/// </summary>
		public bool IsComplex { get; set; }

		/// <summary>
		///     Flag, indicating if the value is an enumerable.
		/// </summary>
		public bool IsEnumerable { get; set; }

		private string ValueDisplayString => this.Value != null ? $@"""{this.Value}""" : "null";

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{this.Path} = {this.ValueDisplayString}";
		}
	}
}
