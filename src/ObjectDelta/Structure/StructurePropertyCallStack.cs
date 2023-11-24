namespace ObjectDelta.Structure
{
	using System.Collections.Generic;
	using System.Linq;

	internal sealed class StructurePropertyCallStack
	{
		private readonly StructureProperty[] stack;

		/// <summary>
		///     Creates a new instance of the <see cref="StructurePropertyCallStack" /> type.
		/// </summary>
		/// <param name="stack"></param>
		private StructurePropertyCallStack(StructureProperty[] stack)
		{
			this.stack = stack;
		}

		/// <summary>
		///     Gets the property for the given index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public StructureProperty this[int index] => this.stack[index];

		/// <summary>
		///     Gets the length of the stack.
		/// </summary>
		public int Length => this.stack.Length;

		/// <summary>
		///     Creates the stack for the given property.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static StructurePropertyCallStack Create(StructureProperty property)
		{
			return new StructurePropertyCallStack(BuildCallStack(property).Reverse().ToArray());
		}

		private static IEnumerable<StructureProperty> BuildCallStack(StructureProperty structureProperty)
		{
			if(structureProperty.IsRootProperty)
			{
				return new StructureProperty[] { structureProperty };
			}

			List<StructureProperty> properties = new List<StructureProperty> { structureProperty };
			properties.AddRange(BuildCallStack(structureProperty.Parent));

			return properties;
		}
	}
}
