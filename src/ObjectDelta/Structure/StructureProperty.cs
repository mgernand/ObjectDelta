namespace ObjectDelta.Structure
{
	using System;
	using global::ObjectDelta.Structure.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///     A class that holds the info about a property.
	/// </summary>
	[PublicAPI]
	public sealed class StructureProperty
	{
		private readonly DynamicGetter getter;
		private readonly StructurePropertyInfo info;

		/// <summary>
		///     Creates a new instance of the <see cref="StructureProperty" /> type.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="getter"></param>
		internal StructureProperty(StructurePropertyInfo info, DynamicGetter getter)
		{
			this.info = info;
			this.getter = getter;

			this.IsRootProperty = info.Parent == null;
			this.IsSimple = this.Type.IsSimpleType();
			this.IsComplex = !this.Type.IsSimpleType() && !this.Type.IsEnumerableType();
			this.IsEnumerable = !this.Type.IsSimpleType() && this.Type.IsEnumerableType();
			this.IsElement = this.Parent != null && (this.Parent.IsElement || this.Parent.IsEnumerable);
			this.ElementType = this.IsEnumerable ? this.Type.GetEnumerableElementType() : null;
			this.Path = BuildPath(this);
		}

		/// <summary>
		///     Gets the name of the property.
		/// </summary>
		public string Name => this.info.Name;

		/// <summary>
		///     Gets the complete flat path of the property.
		/// </summary>
		public string Path { get; }

		/// <summary>
		///     Gets the type of the property.
		/// </summary>
		public Type Type => this.info.Type;

		/// <summary>
		///     Gets the attributes added to the property.
		/// </summary>
		public Attribute[] Attributes => this.info.Attributes;

		/// <summary>
		///     Flag, indicating if the property is contained in the corresponding <see cref="StructureType" />.
		/// </summary>
		public bool IsRootProperty { get; }

		/// <summary>
		///     Flag, indicating if the property is a simple type.
		/// </summary>
		public bool IsSimple { get; }

		/// <summary>
		///     Flag, indicating if the property is a complex type.
		/// </summary>
		public bool IsComplex { get; }

		/// <summary>
		///     Flag, indicating if the property is an enumerable type.
		/// </summary>
		public bool IsEnumerable { get; }

		/// <summary>
		///     Flag, indicating if the property is an element of an enumerable.
		/// </summary>
		public bool IsElement { get; }

		/// <summary>
		///     Gets the type of the enumerable element.
		/// </summary>
		public Type ElementType { get; }

		/// <summary>
		///     Gets the parent property (in case of complex types and enumerable).
		/// </summary>
		internal StructureProperty Parent => this.info.Parent;

		/// <summary>
		///     Gets the value of the property from the given instance.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public object GetValue(object obj)
		{
			return obj != null ? this.getter.Invoke(obj) : null;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Path;
		}

		private static string BuildPath(StructureProperty property)
		{
			return property.IsRootProperty
				? property.Name
				: string.Concat(BuildPath(property.Parent, property.Name));
		}

		private static string BuildPath(StructureProperty parent, string name)
		{
			return parent == null
				? name
				: string.Concat(parent.Path, parent.IsEnumerable ? "[*]." : ".", name);
		}
	}
}
