namespace ObjectStructure
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IStructureTypeFactory
	{
		StructureType CreateType<T>();

		StructureType CreateType(Type type);
	}
}
