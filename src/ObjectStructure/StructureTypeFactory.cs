namespace ObjectStructure
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructureTypeFactory : IStructureTypeFactory
	{
		private static IDictionary<Type, StructureType> typeCache = new Dictionary<Type, StructureType>();

		private IStructureTypeReflector reflector;

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
			Guard.Against.Null(type, nameof(type));

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
