namespace ObjectDelta.UnitTests.Structure
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using global::ObjectDelta.Structure;
	using global::ObjectDelta.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerableSinglePropertyTests
	{
		private class SingleSimpleEnumerableProperty : SinglePropertyModel<string[]>
		{
		}

		private class SingleComplexEnumerableProperty : SinglePropertyModel<Complex[]>
		{
		}

		private class Complex
		{
			public string String { get; set; }
		}

		private static IEnumerable<object[]> TypeTestCases()
		{
			yield return new object[]
			{
				typeof(SingleSimpleEnumerableProperty),
				nameof(SingleSimpleEnumerableProperty),
				nameof(SingleSimpleEnumerableProperty.Property),
				nameof(SingleSimpleEnumerableProperty.Property),
				typeof(string[]),
				1
			};
			yield return new object[]
			{
				typeof(SingleComplexEnumerableProperty),
				nameof(SingleComplexEnumerableProperty),
				nameof(SingleComplexEnumerableProperty.Property),
				nameof(SingleComplexEnumerableProperty.Property),
				typeof(Complex[]),
				2
			};
		}

		private static IEnumerable<object[]> InstanceTestCases()
		{
			yield return new object[]
			{
				new SingleSimpleEnumerableProperty(),
				typeof(string[]),
				1
			};
			yield return new object[]
			{
				new SingleSimpleEnumerableProperty { Property = Array.Empty<string>() },
				typeof(string[]),
				1
			};
			yield return new object[]
			{
				new SingleSimpleEnumerableProperty { Property = new string[] { "Hello" } },
				typeof(string[]),
				2
			};
			yield return new object[]
			{
				new SingleComplexEnumerableProperty(),
				typeof(Complex[]),
				2
			};
			yield return new object[]
			{
				new SingleComplexEnumerableProperty { Property = Array.Empty<Complex>() },
				typeof(Complex[]),
				1
			};
			yield return new object[]
			{
				new SingleComplexEnumerableProperty { Property = new Complex[] { null } },
				typeof(Complex[]),
				2
			};
			yield return new object[]
			{
				new SingleComplexEnumerableProperty { Property = new Complex[] { new Complex() } },
				typeof(Complex[]),
				3
			};
			yield return new object[]
			{
				new SingleComplexEnumerableProperty { Property = new Complex[] { new Complex(), new Complex() } },
				typeof(Complex[]),
				5
			};
			yield return new object[]
			{
				new SingleComplexEnumerableProperty { Property = new Complex[] { new Complex(), new Complex(), new Complex() } },
				typeof(Complex[]),
				7
			};
			yield return new object[]
			{
				new SingleComplexEnumerableProperty { Property = new Complex[] { new Complex(), null, new Complex(), new Complex() } },
				typeof(Complex[]),
				8
			};
		}

		[Test]
		[TestCaseSource(nameof(InstanceTestCases))]
		public void ShouldCreateCorrectIndices(
			object instance,
			Type expectedPropertyType,
			int expectedIndexCount)
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(instance);

			structure.Indices.Should().NotBeNullOrEmpty();
			structure.Indices.Should().HaveCount(expectedIndexCount);
			structure.Indices[0].Type.Should().Be(expectedPropertyType);

			Console.WriteLine(structure.Indices);
		}

		[Test]
		[TestCaseSource(nameof(TypeTestCases))]
		public void ShouldCreateCorrectSchema(
			Type type,
			string expectedSchemaName,
			string expectedPropertyName,
			string expectedPropertyPath,
			Type expectedPropertyType,
			int expectedPropertyCount)
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(type);

			structure.Schema.Should().NotBeNull();
			structure.Schema.Name.Should().Be(expectedSchemaName);

			structure.Schema.StructureType.Should().NotBeNull();
			structure.Schema.StructureType.Name.Should().Be(expectedSchemaName);
			structure.Schema.StructureType.Properties.Should().NotBeNullOrEmpty();
			structure.Schema.StructureType.Properties.Should().HaveCount(expectedPropertyCount);
			structure.Schema.StructureType.Properties[0].Name.Should().Be(expectedPropertyName);
			structure.Schema.StructureType.Properties[0].Path.Should().Be(expectedPropertyPath);
			structure.Schema.StructureType.Properties[0].Type.Should().Be(expectedPropertyType);
			structure.Schema.StructureType.Properties[0].IsEnumerable.Should().BeTrue();

			Console.WriteLine(structure.Schema);
		}
	}
}
