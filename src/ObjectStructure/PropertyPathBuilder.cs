namespace ObjectStructure
{
	internal static class PropertyPathBuilder
	{
		public static string BuildPath(StructureProperty property)
		{
			return property.IsRootProperty
				? property.Name
				: string.Concat(BuildPath(property.Parent, property.Name));
		}

		private static string BuildPath(StructureProperty parent, string name)
		{
			return parent == null
				? name
				: string.Concat(parent.Path, parent.IsEnumerable ? "[*]." : ".", name);
		}
	}
}
