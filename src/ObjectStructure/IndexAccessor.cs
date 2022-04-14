namespace ObjectStructure
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class IndexAccessor
	{
		private readonly StructureProperty structureProperty;

		internal IndexAccessor(StructureProperty structureProperty)
		{
			this.structureProperty = structureProperty;
		}

		public string Path => this.structureProperty.Path;

		public Type Type => this.structureProperty.Type;

		public bool IsEnumerable => this.structureProperty.IsEnumerable;

		public bool IsElement => this.structureProperty.IsElement;

		internal StructureIndexValue[] GetValues<T>(T item)
		{
			// TODO

			return new StructureIndexValue[]
			{
				//new StructureIndexValue(" ", null)
			};
		}
	}
}
