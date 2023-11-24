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
	public class SimpleSinglePropertyTests
	{
		private class SingleStringProperty : SinglePropertyModel<string>
		{
		}

		private class SingleIntegerProperty : SinglePropertyModel<int>
		{
		}

		private class SingleNullableIntegerProperty : SinglePropertyModel<int?>
		{
		}

		private class SingleGuidProperty : SinglePropertyModel<Guid>
		{
		}

		private class SingleNullableGuidProperty : SinglePropertyModel<Guid?>
		{
		}

		private class SingleStructProperty : SinglePropertyModel<Struct>
		{
		}

		private class SingleNullableStructProperty : SinglePropertyModel<Struct?>
		{
		}

		private struct Struct
		{
		}

		private static IEnumerable<object[]> TestCases()
		{
			yield return new object[]
			{
				typeof(SingleStringProperty),
				nameof(SingleStringProperty),
				nameof(SingleStringProperty.Property),
				nameof(SingleStringProperty.Property),
				typeof(string)
			};
			yield return new object[]
			{
				typeof(SingleIntegerProperty),
				nameof(SingleIntegerProperty),
				nameof(SingleIntegerProperty.Property),
				nameof(SingleIntegerProperty.Property),
				typeof(int)
			};
			yield return new object[]
			{
				typeof(SingleNullableIntegerProperty),
				nameof(SingleNullableIntegerProperty),
				nameof(SingleNullableIntegerProperty.Property),
				nameof(SingleNullableIntegerProperty.Property),
				typeof(int?)
			};
			yield return new object[]
			{
				typeof(SingleGuidProperty),
				nameof(SingleGuidProperty),
				nameof(SingleGuidProperty.Property),
				nameof(SingleGuidProperty.Property),
				typeof(Guid)
			};
			yield return new object[]
			{
				typeof(SingleNullableGuidProperty),
				nameof(SingleNullableGuidProperty),
				nameof(SingleNullableGuidProperty.Property),
				nameof(SingleNullableGuidProperty.Property),
				typeof(Guid?)
			};
			yield return new object[]
			{
				typeof(SingleStructProperty),
				nameof(SingleStructProperty),
				nameof(SingleStructProperty.Property),
				nameof(SingleGuidProperty.Property),
				typeof(Struct)
			};
			yield return new object[]
			{
				typeof(SingleNullableStructProperty),
				nameof(SingleNullableStructProperty),
				nameof(SingleNullableStructProperty.Property),
				nameof(SingleNullableStructProperty.Property),
				typeof(Struct?)
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
			structure.Indices.Should().HaveCount(1);
			structure.Indices[0].Type.Should().Be(expectedPropertyType);

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
			structure.Schema.StructureType.Properties.Should().HaveCount(1);
			structure.Schema.StructureType.Properties[0].Name.Should().Be(expectedPropertyName);
			structure.Schema.StructureType.Properties[0].Path.Should().Be(expectedPropertyPath);
			structure.Schema.StructureType.Properties[0].Type.Should().Be(expectedPropertyType);
			structure.Schema.StructureType.Properties[0].IsSimple.Should().BeTrue();

			Console.WriteLine(structure.Schema);
		}
	}
}
