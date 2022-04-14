namespace ObjectStructure
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class StructureIndices : IEnumerable<StructureIndex>
	{
		private readonly StructureIndex[] indices;

		internal StructureIndices(StructureIndex[] indices)
		{
			this.indices = indices ?? Array.Empty<StructureIndex>();
		}

		public StructureIndex this[int index] => this.indices[index];

		/// <inheritdoc />
		public IEnumerator<StructureIndex> GetEnumerator()
		{
			return this.indices.Cast<StructureIndex>().GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			foreach(StructureIndex index in this)
			{
				builder.AppendLine(index.ToString());
			}

			return builder.ToString();
		}
	}
}
