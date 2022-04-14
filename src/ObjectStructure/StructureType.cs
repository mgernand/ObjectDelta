namespace ObjectStructure
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

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
			Guard.Against.Null(type, nameof(type));

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
	}
}
