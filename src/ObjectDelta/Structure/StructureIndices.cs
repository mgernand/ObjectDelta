namespace ObjectDelta.Structure
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using JetBrains.Annotations;

	/// <summary>
	///     A special enumerable class that yields <see cref="StructureIndex" /> instances.
	/// </summary>
	[PublicAPI]
	public sealed class StructureIndices : IEnumerable<StructureIndex>
	{
		private readonly StructureIndex[] indices;

		/// <summary>
		///     Creates an new instance of the <see cref="StructureIndices" /> type.
		/// </summary>
		/// <param name="indices"></param>
		internal StructureIndices(StructureIndex[] indices = null)
		{
			this.indices = indices ?? Array.Empty<StructureIndex>();
		}

		/// <summary>
		///     Gets the items at the given index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
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
