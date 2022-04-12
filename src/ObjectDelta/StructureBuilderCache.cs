namespace ObjectDelta
{
	using Structurizer;
	using Structurizer.Configuration;

	internal static class StructureBuilderCache<T> where T : class
	{
		internal static readonly IStructureBuilder Builder;

		static StructureBuilderCache()
		{
			StructureTypeConfigurations typeConfig = new StructureTypeConfigurations();
			typeConfig.Register<T>();
			Builder = StructureBuilder.Create(typeConfig);
		}
	}
}
