namespace ObjectStructure
{
	using Fluxera.Guards;

	internal sealed class StructureIndexValue
	{
		internal StructureIndexValue(string path, object value)
		{
			Guard.Against.NullOrWhiteSpace(path, nameof(path));

			this.Path = path;
			this.Value = value;
		}

		public string Path { get; }

		public object Value { get; }
	}
}
