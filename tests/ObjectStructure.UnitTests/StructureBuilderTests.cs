namespace ObjectStructure.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;
	using ObjectStructure.UnitTests.Model;

	[TestFixture]
	public class StructureBuilderTests
	{
		[Test]
		public void ShouldProvideValueForComplexProperty()
		{
			IStructureBuilder builder = new StructureBuilder();
			ComplexModel instance = new ComplexModel
			{
				ComplexProperty = new ComplexType
				{
					StringProperty = "Hello"
				}
			};
			Structure structure = builder.CreateStructure(instance);

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(ComplexModel));
			structure.Indices.Should().HaveCount(2);

			structure.Indices[0].Type.Should().Be(typeof(ComplexType));
			structure.Indices[0].Name.Should().Be("ComplexProperty");
			structure.Indices[0].Path.Should().Be("ComplexProperty");
			structure.Indices[0].Value.Should().Be(instance.ComplexProperty);

			structure.Indices[1].Type.Should().Be(typeof(string));
			structure.Indices[1].Name.Should().Be("StringProperty");
			structure.Indices[1].Path.Should().Be("ComplexProperty.StringProperty");
			structure.Indices[1].Value.Should().Be("Hello");
		}

		[Test]
		public void ShouldProvideValueForSimpleEnumerableProperty()
		{
			IStructureBuilder builder = new StructureBuilder();
			SimpleEnumerableModel instance = new SimpleEnumerableModel
			{
				ListProperty = new List<string> { "Hello", "World" }
			};
			Structure structure = builder.CreateStructure(instance);

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(SimpleEnumerableModel));
			structure.Indices.Should().HaveCount(3);
		}

		[Test]
		public void ShouldProvideValueForSimpleProperty()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(new SimpleModel
			{
				StringProperty = "Hello"
			});

			structure.Should().NotBeNull();
			structure.Name.Should().Be(nameof(SimpleModel));
			structure.Indices.Should().HaveCount(1);

			structure.Indices[0].Type.Should().Be(typeof(string));
			structure.Indices[0].Name.Should().Be("StringProperty");
			structure.Indices[0].Path.Should().Be("StringProperty");
			structure.Indices[0].Value.Should().Be("Hello");
		}
	}
}
