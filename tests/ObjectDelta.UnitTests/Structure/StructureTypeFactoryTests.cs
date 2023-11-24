namespace ObjectDelta.UnitTests.Structure
{
	using FluentAssertions;
	using global::ObjectDelta.Structure;
	using NUnit.Framework;

	[TestFixture]
	public class StructureTypeFactoryTests
	{
		private class ModelClass
		{
			public string String { get; set; }
		}

		private struct ModelStruct
		{
			public string String { get; set; }
		}

		[Test]
		public void ShouldCreateStructuredTypeForClass()
		{
			IStructureTypeFactory factory = new StructureTypeFactory();
			StructureType structureType = factory.CreateType<ModelClass>();

			structureType.Should().NotBeNull();
			structureType.Name.Should().Be(nameof(ModelClass));
			structureType.Type.Should().Be<ModelClass>();
			structureType.Properties.Should().NotBeNullOrEmpty();
			structureType.Properties.Should().HaveCount(1);
		}

		[Test]
		public void ShouldCreateStructuredTypeForStruct()
		{
			IStructureTypeFactory factory = new StructureTypeFactory();
			StructureType structureType = factory.CreateType<ModelStruct>();

			structureType.Should().NotBeNull();
			structureType.Name.Should().Be(nameof(ModelStruct));
			structureType.Type.Should().Be<ModelStruct>();
			structureType.Properties.Should().NotBeNullOrEmpty();
			structureType.Properties.Should().HaveCount(1);
		}
	}
}
