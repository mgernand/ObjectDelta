namespace ObjectDelta.Structure
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class StructureTypeFactory : IStructureTypeFactory
	{
		private static IDictionary<Type, StructureType> typeCache = new Dictionary<Type, StructureType>();

		private IStructureTypeReflector reflector;

		/// <summary>
		///     Creates a new instance of the <see cref="StructureTypeFactory" /> type.
		/// </summary>
		/// <param name="reflector"></param>
		public StructureTypeFactory(IStructureTypeReflector reflector = null)
		{
			this.reflector = reflector ?? new StructureTypeReflector();
		}

		/// <inheritdoc />
		StructureType IStructureTypeFactory.CreateType<T>()
		{
			return this.CreateType(typeof(T));
		}

		/// <inheritdoc />
		public StructureType CreateType(Type type)
		{
			ArgumentNullException.ThrowIfNull(type, nameof(type));

			if(!typeCache.TryGetValue(type, out StructureType structureType))
			{
				StructureProperty[] properties = this.reflector.GetProperties(type);
				structureType = new StructureType(type, properties);

				typeCache.Add(type, structureType);
			}

			return structureType;
		}
	}
}
