namespace ObjectStructure
{
	using System;
	using JetBrains.Annotations;
	using ObjectStructure.Reflection;

	[PublicAPI]
	public sealed class StructureProperty
	{
		private readonly DynamicGetter getter;
		private readonly StructurePropertyInfo info;

		internal StructureProperty(StructurePropertyInfo info, DynamicGetter getter)
		{
			this.info = info;
			this.getter = getter;

			this.IsRootProperty = info.Parent == null;
			this.IsSimple = this.Type.IsSimpleType();
			this.IsComplex = !this.Type.IsSimpleType() && !this.Type.IsEnumerableType();
			this.IsEnumerable = !this.Type.IsSimpleType() && this.Type.IsEnumerableType();
			this.IsElement = (this.Parent != null) && (this.Parent.IsElement || this.Parent.IsEnumerable);
			this.ElementType = this.IsEnumerable ? this.Type.GetEnumerableElementType() : null;
			this.Path = PropertyPathBuilder.BuildPath(this);
		}

		public string Name => this.info.Name;

		public string Path { get; }

		public Type Type => this.info.Type;

		public StructureProperty Parent => this.info.Parent;

		public bool IsRootProperty { get; }

		public bool IsSimple { get; }

		public bool IsComplex { get; }

		public bool IsEnumerable { get; }

		public bool IsElement { get; }

		public Type ElementType { get; }

		public Attribute[] Attributes => this.info.Attributes;

		public object GetValue(object obj)
		{
			return this.getter.Invoke(obj);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Path;
		}
	}
}
