namespace ObjectStructure
{
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IStructurePropertyFactory
	{
		StructureProperty CreateProperty(PropertyInfo propertyInfo, StructureProperty parent);
	}
}
