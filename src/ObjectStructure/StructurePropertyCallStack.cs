namespace ObjectStructure
{
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Utilities.Extensions;

	internal sealed class StructurePropertyCallStack
	{
		private readonly StructureProperty[] stack;

		private StructurePropertyCallStack(StructureProperty[] stack)
		{
			this.stack = stack;
		}

		internal StructureProperty this[int index] => this.stack[index];

		internal int Length => this.stack.Length;

		internal static StructurePropertyCallStack Create(StructureProperty property)
		{
			return new StructurePropertyCallStack(BuildCallStack(property).Reverse().ToArray());
		}

		private static IEnumerable<StructureProperty> BuildCallStack(StructureProperty structureProperty)
		{
			if(structureProperty.IsRootProperty)
			{
				return new StructureProperty[] { structureProperty };
			}

			IList<StructureProperty> properties = new List<StructureProperty> { structureProperty };
			properties.AddRange(BuildCallStack(structureProperty.Parent));

			return properties;
		}
	}
}
