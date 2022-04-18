namespace ObjectDelta.UnitTests
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Threading;
	using FluentAssertions;
	using global::ObjectDelta.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ComplexTests
	{
		public class Complex
		{
			public string String { get; set; }

			public int Integer { get; set; }

			public Guid? NullableGuid { get; set; }
		}

		public class ComplexModel : GenericModel<Complex>
		{
			public static readonly ComplexModel First = new ComplexModel
			{
				Property = new Complex
				{
					String = "Hello",
					Integer = 420,
					NullableGuid = Guid.Parse("4b55257b-6d62-4b71-8382-312c2e39dcd3")
				}
			};

			public static readonly ComplexModel Second = new ComplexModel
			{
				Property = new Complex
				{
					String = null,
					Integer = 0,
					NullableGuid = null
				}
			};

			public static readonly ComplexModel Third = new ComplexModel
			{
				Property = new Complex
				{
					String = "World",
					Integer = 69,
					NullableGuid = Guid.Parse("90a1310e-da9d-464c-9514-f4bb5f8dbb83")
				}
			};

			public static readonly ComplexModel Null = new ComplexModel
			{
				Property = null
			};
		}

		public static object[] NullTestData =
		{
			new object[] { ComplexModel.First, ComplexModel.Null, typeof(Complex), ComplexModel.First.Property, null, 1 },
			new object[] { ComplexModel.Null, ComplexModel.First, typeof(Complex), null, ComplexModel.First.Property, 4 },
		};

		public static object[] TestData =
		{
			new object[] { ComplexModel.First, ComplexModel.Second, typeof(string), "Property.String", "Hello", null },
			new object[] { ComplexModel.Second, ComplexModel.First, typeof(string), "Property.String", null, "Hello" },

			new object[] { ComplexModel.First, ComplexModel.Second, typeof(int), "Property.Integer", 420, 0 },
			new object[] { ComplexModel.Second, ComplexModel.First, typeof(int), "Property.Integer", 0, 420 },

			new object[] { ComplexModel.First, ComplexModel.Second, typeof(Guid?), "Property.NullableGuid", Guid.Parse("4b55257b-6d62-4b71-8382-312c2e39dcd3"), null },
			new object[] { ComplexModel.Second, ComplexModel.First, typeof(Guid?), "Property.NullableGuid", null, Guid.Parse("4b55257b-6d62-4b71-8382-312c2e39dcd3") },

			new object[] { ComplexModel.First, ComplexModel.Third, typeof(string), "Property.String", "Hello", "World" },
			new object[] { ComplexModel.Third, ComplexModel.First, typeof(string), "Property.String", "World", "Hello" },

			new object[] { ComplexModel.First, ComplexModel.Third, typeof(int), "Property.Integer", 420, 69 },
			new object[] { ComplexModel.Third, ComplexModel.First, typeof(int), "Property.Integer", 69, 420 },

			new object[] { ComplexModel.First, ComplexModel.Third, typeof(Guid?), "Property.NullableGuid", Guid.Parse("4b55257b-6d62-4b71-8382-312c2e39dcd3"), Guid.Parse("90a1310e-da9d-464c-9514-f4bb5f8dbb83") },
			new object[] { ComplexModel.Third, ComplexModel.First, typeof(Guid?), "Property.NullableGuid", Guid.Parse("90a1310e-da9d-464c-9514-f4bb5f8dbb83"), Guid.Parse("4b55257b-6d62-4b71-8382-312c2e39dcd3") },
		};

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldCreateDeltaForComplexNestedProperty(
			ComplexModel first,
			ComplexModel second,
			Type expectedType,
			string expectedPath,
			object expectedOldValue,
			object expectedNewValue)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

			ObjectDelta<ComplexModel> result = first.CompareTo(second);
			Console.WriteLine(result);

			result.Should().NotBeNull();
			result.HasChanges.Should().BeTrue();
			result.PropertyDeltas.Should().HaveCount(4);

			result.PropertyDeltas.Should().Contain(x => x.Path == expectedPath);
			PropertyDelta propertyDelta = result.PropertyDeltas.First(x => x.Path == expectedPath);
			propertyDelta.Name.Should().Be(expectedPath.Split('.').Last());
			propertyDelta.Path.Should().Be(expectedPath);
			propertyDelta.Type.Should().Be(expectedType);
			propertyDelta.OldValue.Should().Be(expectedOldValue);
			propertyDelta.NewValue.Should().Be(expectedNewValue);
		}

		[Test]
		[TestCaseSource(nameof(NullTestData))]
		public void ShouldCreateDeltaForSingleComplexProperty(
			ComplexModel first,
			ComplexModel second,
			Type expectedType,
			object expectedOldValue,
			object expectedNewValue,
			int expectedDeltaCount)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

			ObjectDelta<ComplexModel> result = first.CompareTo(second);
			Console.WriteLine(result);

			result.Should().NotBeNull();
			result.HasChanges.Should().BeTrue();
			result.PropertyDeltas.Should().HaveCount(expectedDeltaCount);
			result.PropertyDeltas[0].Name.Should().Be("Property");
			result.PropertyDeltas[0].Path.Should().Be("Property");
			result.PropertyDeltas[0].Type.Should().Be(expectedType);
			result.PropertyDeltas[0].OldValue.Should().Be(expectedOldValue);
			result.PropertyDeltas[0].NewValue.Should().Be(expectedNewValue);
		}
	}
}
