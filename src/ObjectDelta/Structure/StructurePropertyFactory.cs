namespace ObjectDelta.Structure
{
	using System;
	using System.Linq;
	using System.Reflection;
	using global::ObjectDelta.Structure.Reflection;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StructurePropertyFactory : IStructurePropertyFactory
	{
		/// <inheritdoc />
		public StructureProperty CreateProperty(PropertyInfo propertyInfo, StructureProperty parent)
		{
			return new StructureProperty(
				ConvertInfo(propertyInfo, parent),
				DynamicPropertyFactory.GetterFor(propertyInfo));
		}

		private static StructurePropertyInfo ConvertInfo(PropertyInfo propertyInfo, StructureProperty parent = null)
		{
			return new StructurePropertyInfo(
				propertyInfo.Name,
				propertyInfo.PropertyType,
				propertyInfo.GetCustomAttributes<Attribute>(true).ToArray(),
				parent);
		}
	}
}
