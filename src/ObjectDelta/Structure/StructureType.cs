namespace ObjectDelta.Structure
{
	using System;
	using System.Text;
	using JetBrains.Annotations;

	/// <summary>
	///     Contains the info of a <see cref="Type" />.
	/// </summary>
	[PublicAPI]
	public sealed class StructureType
	{
		/// <summary>
		///     Creates a new instance of the <see cref="StructureType" /> type.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="properties"></param>
		internal StructureType(Type type, StructureProperty[] properties)
		{
			ArgumentNullException.ThrowIfNull(type, nameof(type));

			this.Type = type;
			this.Name = type.Name;
			this.Properties = properties;
		}

		/// <summary>
		///     Gets the .NET type.
		/// </summary>
		public Type Type { get; }

		/// <summary>
		///     Gets the name of the type.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     Gets the properties of the type.
		/// </summary>
		public StructureProperty[] Properties { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			foreach(StructureProperty property in this.Properties)
			{
				builder.AppendLine(property.ToString());
			}

			return builder.ToString();
		}
	}
}
