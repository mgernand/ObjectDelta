namespace ObjectDelta.UnitTests.Structure
{
	using System;
	using FluentAssertions;
	using global::ObjectDelta.Structure;
	using global::ObjectDelta.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class StructureBuilderTests
	{
		private class GenericModel<T>
		{
			public T Property { get; set; }
		}

		private class ModelClass
		{
			public string String { get; set; }
		}

		private struct ModelStruct
		{
			public string String { get; set; }
		}

		private class SingleSimplePropertyModel : SinglePropertyModel<string>
		{
		}

		[Test]
		public void ShouldCreateSchemaAndIndicesForSingleSimpleProperty()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(new SingleSimplePropertyModel());

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(SingleSimplePropertyModel));
			structure.Schema.Should().NotBeNull();
			structure.Schema.Name.Should().Be(nameof(SingleSimplePropertyModel));
			structure.Schema.IndexAccessors.Should().NotBeNullOrEmpty();
			structure.Schema.IndexAccessors.Should().HaveCount(1);
			structure.Indices.Should().NotBeNullOrEmpty();
			structure.Indices.Should().HaveCount(1);
		}

		[Test]
		public void ShouldCreateSchemaForSingleSimpleProperty()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure<SingleSimplePropertyModel>();

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(SingleSimplePropertyModel));
			structure.Schema.Should().NotBeNull();
			structure.Schema.Name.Should().Be(nameof(SingleSimplePropertyModel));
			structure.Schema.IndexAccessors.Should().NotBeNullOrEmpty();
			structure.Schema.IndexAccessors.Should().HaveCount(1);
			structure.Indices.Should().NotBeNull();
			structure.Indices.Should().BeEmpty();
		}

		[Test]
		public void ShouldCreateStructureForClassType()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure<ModelClass>();

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(ModelClass));
			structure.Schema.Should().NotBeNull();
			structure.Indices.Should().NotBeNull();
			structure.Indices.Should().BeEmpty();
		}

		[Test]
		public void ShouldCreateStructureForInstance()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(new ModelClass());

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(ModelClass));
			structure.Schema.Should().NotBeNull();
			structure.Indices.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void ShouldCreateStructureForStructType()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure<ModelStruct>();

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(ModelStruct));
			structure.Schema.Should().NotBeNull();
			structure.Indices.Should().NotBeNull();
			structure.Indices.Should().BeEmpty();
		}

		[Test]
		public void ShouldCreateStructureForValue()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(new ModelStruct());

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(ModelStruct));
			structure.Schema.Should().NotBeNull();
			structure.Indices.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void ShouldNotCreateSchemaForGenericType()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(typeof(GenericModel<string>));

			structure.Should().BeNull();
		}

		[Test]
		public void ShouldNotCreateSchemaForGenericTypeDefinition()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(typeof(GenericModel<>));

			structure.Should().BeNull();
		}

		[Test]
		public void ShouldNotCreateStructureForTypeType()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure<Type>();

			structure.Should().BeNull();
		}
	}
}
