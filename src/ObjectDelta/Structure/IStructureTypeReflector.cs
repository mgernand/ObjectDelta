namespace ObjectDelta.Structure
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a service that reflects a given type and its properties.
	/// </summary>
	[PublicAPI]
	public interface IStructureTypeReflector
	{
		/// <summary>
		///     Gets the <see cref="StructureProperty" /> instances for the given type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		StructureProperty[] GetProperties(Type type);
	}
}
