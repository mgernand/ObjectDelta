namespace ObjectStructure
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IStructureTypeReflector
	{
		StructureProperty[] GetProperties(Type type);
	}
}
