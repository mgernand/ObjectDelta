namespace ObjectDelta.Structure
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A factory that creates <see cref="StructureType" /> instances.
	/// </summary>
	[PublicAPI]
	public interface IStructureTypeFactory
	{
		/// <summary>
		///     Creates a <see cref="StructureType" /> for the given type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		StructureType CreateType<T>();

		/// <summary>
		///     Creates a <see cref="StructureType" /> for the given type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		StructureType CreateType(Type type);
	}
}
