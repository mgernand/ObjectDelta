namespace ObjectStructure.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;
	using ObjectStructure.UnitTests.Model;

	[TestFixture]
	public class StructureTypeFactoryTests
	{
		[Test]
		public void ShouldCreateStructureTypeForComplexEnumerableProperty()
		{
			Type type = typeof(SingleComplexEnumerableProperty);
			IStructureTypeFactory factory = new StructureTypeFactory();
			StructureType structureType = factory.CreateType<SingleComplexEnumerableProperty>();

			structureType.Should().NotBeNull();

			structureType.Type.Should().Be(type);
			structureType.Name.Should().Be(type.Name);
			structureType.Properties.Should().NotBeNullOrEmpty();
			structureType.Properties.Should().HaveCount(2);

			structureType.Properties[0].Type.Should().Be(typeof(IList<Complex>));
			structureType.Properties[0].Name.Should().Be("ListProperty");
			structureType.Properties[0].Path.Should().Be("ListProperty");

			structureType.Properties[1].Type.Should().Be(typeof(string));
			structureType.Properties[1].Name.Should().Be("StringProperty");
			structureType.Properties[1].Path.Should().Be("ListProperty[*].StringProperty");

			//structureType.Properties[0].Parent.Should().BeNull();
			//structureType.Properties[0].Attributes.Should().HaveCount(0);
			//structureType.Properties[0].IsRootProperty.Should().BeTrue();
			//structureType.Properties[0].IsEnumerable.Should().BeFalse();
			//structureType.Properties[0].IsElement.Should().BeFalse();
			//structureType.Properties[0].ElementType.Should().BeNull();
		}

		[Test]
		public void ShouldCreateStructureTypeForComplexProperty()
		{
			Type type = typeof(SingleComplexProperty);
			IStructureTypeFactory factory = new StructureTypeFactory();
			StructureType structureType = factory.CreateType<SingleComplexProperty>();

			structureType.Should().NotBeNull();

			structureType.Type.Should().Be(type);
			structureType.Name.Should().Be(type.Name);
			structureType.Properties.Should().NotBeNullOrEmpty();
			structureType.Properties.Should().HaveCount(2);

			structureType.Properties[0].Type.Should().Be(typeof(Complex));
			structureType.Properties[0].Name.Should().Be("ComplexProperty");
			structureType.Properties[0].Path.Should().Be("ComplexProperty");

			structureType.Properties[1].Type.Should().Be(typeof(string));
			structureType.Properties[1].Name.Should().Be("StringProperty");
			structureType.Properties[1].Path.Should().Be("ComplexProperty.StringProperty");

			//structureType.Properties[0].Parent.Should().BeNull();
			//structureType.Properties[0].Attributes.Should().HaveCount(0);
			//structureType.Properties[0].IsRootProperty.Should().BeTrue();
			//structureType.Properties[0].IsEnumerable.Should().BeFalse();
			//structureType.Properties[0].IsElement.Should().BeFalse();
			//structureType.Properties[0].ElementType.Should().BeNull();
		}

		[Test]
		public void ShouldCreateStructureTypeForSimpleEnumerableProperty()
		{
			Type type = typeof(SingleSimpleEnumerableProperty);
			IStructureTypeFactory factory = new StructureTypeFactory();
			StructureType structureType = factory.CreateType<SingleSimpleEnumerableProperty>();

			structureType.Should().NotBeNull();

			structureType.Type.Should().Be(type);
			structureType.Name.Should().Be(type.Name);
			structureType.Properties.Should().NotBeNullOrEmpty();
			structureType.Properties.Should().HaveCount(1);

			structureType.Properties[0].Type.Should().Be(typeof(IList<string>));
			structureType.Properties[0].Name.Should().Be(nameof(SingleSimpleEnumerableProperty.ListProperty));
			structureType.Properties[0].Path.Should().Be(nameof(SingleSimpleEnumerableProperty.ListProperty));

			//structureType.Properties[0].Parent.Should().BeNull();
			//structureType.Properties[0].Attributes.Should().HaveCount(0);
			//structureType.Properties[0].IsRootProperty.Should().BeTrue();
			//structureType.Properties[0].IsEnumerable.Should().BeFalse();
			//structureType.Properties[0].IsElement.Should().BeFalse();
			//structureType.Properties[0].ElementType.Should().BeNull();
		}

		[Test]
		public void ShouldCreateStructureTypeForSimpleProperty()
		{
			Type type = typeof(SingleStringProperty);
			IStructureTypeFactory factory = new StructureTypeFactory();
			StructureType structureType = factory.CreateType<SingleStringProperty>();

			structureType.Should().NotBeNull();

			structureType.Type.Should().Be(type);
			structureType.Name.Should().Be(type.Name);
			structureType.Properties.Should().NotBeNullOrEmpty();
			structureType.Properties.Should().HaveCount(1);

			structureType.Properties[0].Type.Should().Be(typeof(string));
			structureType.Properties[0].Name.Should().Be(nameof(SingleStringProperty.StringProperty));
			structureType.Properties[0].Path.Should().Be(nameof(SingleStringProperty.StringProperty));
			//structureType.Properties[0].Parent.Should().BeNull();
			//structureType.Properties[0].Attributes.Should().HaveCount(0);
			//structureType.Properties[0].IsRootProperty.Should().BeTrue();
			//structureType.Properties[0].IsEnumerable.Should().BeFalse();
			//structureType.Properties[0].IsElement.Should().BeFalse();
			//structureType.Properties[0].ElementType.Should().BeNull();
		}
	}
}
