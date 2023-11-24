namespace ObjectDelta.Structure
{
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a factory that creates <see cref="StructureProperty" /> instances.
	/// </summary>
	[PublicAPI]
	public interface IStructurePropertyFactory
	{
		/// <summary>
		///     Creates a <see cref="StructureProperty" /> from the given <see cref="PropertyInfo" /> and
		///     optional parent <see cref="StructureProperty" />.
		/// </summary>
		/// <param name="propertyInfo"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		StructureProperty CreateProperty(PropertyInfo propertyInfo, StructureProperty parent = null);
	}
}
