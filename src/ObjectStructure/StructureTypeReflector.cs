namespace ObjectStructure
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using ObjectStructure.Reflection;

	[PublicAPI]
	public sealed class StructureTypeReflector : IStructureTypeReflector
	{
		public const BindingFlags PropertyBindingFlags = BindingFlags.Public | BindingFlags.Instance;

		private readonly IStructurePropertyFactory propertyFactory;

		public StructureTypeReflector(IStructurePropertyFactory propertyFactory = null)
		{
			this.propertyFactory = propertyFactory ?? new StructurePropertyFactory();
		}

		/// <inheritdoc />
		public StructureProperty[] GetProperties(Type type)
		{
			return this.GetProperties(type, null);
		}

		private StructureProperty[] GetProperties(Type type, StructureProperty parent)
		{
			PropertyInfo[] initialPropertyInfos = this.GetPropertyInfos(type);
			if(initialPropertyInfos.Length == 0)
			{
				return Array.Empty<StructureProperty>();
			}

			IList<StructureProperty> properties = new List<StructureProperty>();

			// Get the simple type properties.
			PropertyInfo[] simplePropertyInfos = this.GetSimplePropertyInfos(initialPropertyInfos);
			properties.AddRange(simplePropertyInfos.Select(x => this.propertyFactory.CreateProperty(x, parent)));

			// Get the complex type properties.
			PropertyInfo[] complexPropertyInfos = this.GetComplexPropertyInfos(initialPropertyInfos);
			foreach(PropertyInfo complexPropertyInfo in complexPropertyInfos)
			{
				StructureProperty complexProperty = this.propertyFactory.CreateProperty(complexPropertyInfo, parent);
				properties.Add(complexProperty);

				StructureProperty[] innerSimpleProperties = this.GetProperties(complexProperty.Type, complexProperty);
				properties.AddRange(innerSimpleProperties);
			}

			// Get the enumerable properties.
			PropertyInfo[] enumerablePropertyInfos = this.GetEnumerablePropertyInfos(initialPropertyInfos, parent);
			foreach(PropertyInfo enumerablePropertyInfo in enumerablePropertyInfos)
			{
				StructureProperty enumerableProperty = this.propertyFactory.CreateProperty(enumerablePropertyInfo, parent);
				properties.Add(enumerableProperty);

				if(!enumerableProperty.ElementType.IsSimpleType())
				{
					StructureProperty[] elementProperties = this.GetProperties(enumerableProperty.ElementType, enumerableProperty);
					properties.AddRange(elementProperties);
				}
			}

			return properties.ToArray();
		}

		private PropertyInfo[] GetPropertyInfos(Type type)
		{
			return type.GetProperties(PropertyBindingFlags);
		}

		private PropertyInfo[] GetSimplePropertyInfos(PropertyInfo[] propertyInfos, StructureProperty parent = null)
		{
			return propertyInfos.Where(x => x.PropertyType.IsSimpleType()).ToArray();
		}

		private PropertyInfo[] GetComplexPropertyInfos(PropertyInfo[] propertyInfos, StructureProperty parent = null)
		{
			if(propertyInfos.Length == 0)
			{
				return Array.Empty<PropertyInfo>();
			}

			return propertyInfos
				.Where(x =>
					!x.PropertyType.IsSimpleType() &&
					!x.PropertyType.IsEnumerableType())
				.ToArray();
		}

		private PropertyInfo[] GetEnumerablePropertyInfos(PropertyInfo[] propertyInfos, StructureProperty parent = null)
		{
			if(propertyInfos.Length == 0)
			{
				return Array.Empty<PropertyInfo>();
			}

			return propertyInfos
				.Where(x =>
					!x.PropertyType.IsSimpleType() &&
					x.PropertyType.IsEnumerableType())
				.ToArray();
		}
	}
}
