namespace ObjectDelta.Structure
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::ObjectDelta.Utilities;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class SchemaFactory : ISchemaFactory
	{
		private static IDictionary<Type, StructureSchema> schemaCache = new Dictionary<Type, StructureSchema>();

		private static readonly string missingMembersMessage =
			"The item of type '{0}' has no members that can be indexed. " +
			"There's no point in treating items that has nothing to index.";

		/// <inheritdoc />
		public StructureSchema CreateSchema(StructureType structureType)
		{
			Guard.ThrowIfNull(structureType, nameof(structureType));

			if(!schemaCache.TryGetValue(structureType.Type, out StructureSchema structureSchema))
			{
				IndexAccessor[] indexAccessors = GetIndexAccessors(structureType);
				if(indexAccessors == null || indexAccessors.Length < 1)
				{
					throw new InvalidOperationException(string.Format(missingMembersMessage, structureType.Name));
				}

				structureSchema = new StructureSchema(structureType, indexAccessors);
				schemaCache.Add(structureType.Type, structureSchema);
			}

			return structureSchema;
		}

		private static IndexAccessor[] GetIndexAccessors(StructureType structureType)
		{
			IList<IndexAccessor> indexAccessors = new List<IndexAccessor>(structureType.Properties.Length);

			foreach(StructureProperty structureProperty in structureType.Properties)
			{
				indexAccessors.Add(new IndexAccessor(structureProperty));
			}

			return indexAccessors.ToArray();
		}
	}
}
