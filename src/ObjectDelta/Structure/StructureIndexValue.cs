namespace ObjectDelta.Structure
{
	using global::ObjectDelta.Utilities;
	using JetBrains.Annotations;

	/// <summary>
	///		A structure index value that holds the flat path to the value and the value itself.
	/// </summary>
	[PublicAPI]
	public sealed class StructureIndexValue
	{
		/// <summary>
		///     Creates a new instance of the <see cref="StructureIndexValue" /> type.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="value"></param>
		internal StructureIndexValue(string path, object value)
		{
			Guard.ThrowIfNullOrWhiteSpace(path);

			this.Path = path;
			this.Value = value;
		}

		/// <summary>
		///     Gets the path to the value of the property.
		/// </summary>
		public string Path { get; }

		/// <summary>
		///     Gets the value of the property.
		/// </summary>
		public object Value { get; }
	}
}
