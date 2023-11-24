namespace ObjectDelta.Structure
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a builder that creates <see cref="Structure" /> instances.
	/// </summary>
	[PublicAPI]
	public interface IStructureBuilder
	{
		/// <summary>
		///     Creates a <see cref="Structure" /> instance for the given type and instance. The structure
		///     contains the <see cref="StructureSchema" /> and the <see cref="StructureIndices" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		Structure CreateStructure<T>(T item);

		/// <summary>
		///     Creates a <see cref="Structure" /> instance for the given type. The structure contains
		///     the <see cref="StructureSchema" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		Structure CreateStructure<T>();

		/// <summary>
		///     Creates a <see cref="Structure" /> instance for the given type. The structure contains
		///     the <see cref="StructureSchema" />.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		Structure CreateStructure(Type type);
	}
}
