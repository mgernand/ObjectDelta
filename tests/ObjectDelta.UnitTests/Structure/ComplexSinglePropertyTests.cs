namespace ObjectDelta.UnitTests.Structure
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.Utilities.Extensions;
	using global::ObjectDelta.Structure;
	using global::ObjectDelta.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ComplexSinglePropertyTests
	{
		private class Complex
		{
			public string String { get; set; }
		}

		private class SingleComplexProperty : SinglePropertyModel<Complex>
		{
		}

		private static IEnumerable<object[]> TestCases()
		{
			yield return new object[]
			{
				typeof(SingleComplexProperty),
				nameof(SingleComplexProperty),
				nameof(SingleComplexProperty.Property),
				nameof(SingleComplexProperty.Property),
				typeof(Complex)
			};
		}

		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void ShouldCreateCorrectIndices(
			Type type,
			string expectedSchemaName,
			string expectedPropertyName,
			string expectedPropertyPath,
			Type expectedPropertyType)
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(type.CreateInstance());

			structure.Indices.Should().NotBeNullOrEmpty();
			structure.Indices.Should().HaveCount(2);
			structure.Indices[0].Type.Should().Be(expectedPropertyType);
			structure.Indices[1].Type.Should().Be<string>();

			Console.WriteLine(structure.Indices);
		}

		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void ShouldCreateCorrectSchema(
			Type type,
			string expectedSchemaName,
			string expectedPropertyName,
			string expectedPropertyPath,
			Type expectedPropertyType)
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(type);

			structure.Schema.Should().NotBeNull();
			structure.Schema.Name.Should().Be(expectedSchemaName);

			structure.Schema.StructureType.Should().NotBeNull();
			structure.Schema.StructureType.Name.Should().Be(expectedSchemaName);
			structure.Schema.StructureType.Properties.Should().NotBeNullOrEmpty();
			structure.Schema.StructureType.Properties.Should().HaveCount(2);
			structure.Schema.StructureType.Properties[0].Name.Should().Be(expectedPropertyName);
			structure.Schema.StructureType.Properties[0].Path.Should().Be(expectedPropertyPath);
			structure.Schema.StructureType.Properties[0].Type.Should().Be(expectedPropertyType);
			structure.Schema.StructureType.Properties[0].IsComplex.Should().BeTrue();

			structure.Schema.StructureType.Properties[1].Name.Should().Be("String");
			structure.Schema.StructureType.Properties[1].Path.Should().Be(expectedPropertyPath + ".String");
			structure.Schema.StructureType.Properties[1].Type.Should().Be<string>();
			structure.Schema.StructureType.Properties[1].IsSimple.Should().BeTrue();

			Console.WriteLine(structure.Schema);
		}
	}
}
