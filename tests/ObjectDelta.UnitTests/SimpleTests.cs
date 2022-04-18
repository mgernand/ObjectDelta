namespace ObjectDelta.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Threading;
	using FluentAssertions;
	using global::ObjectDelta.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class SimpleTests
	{
		private class Complex
		{
			public static readonly Complex Empty = new Complex();
		}

		private enum ModelEnum
		{
			One,
			Two,
			Three
		}

		private static readonly IList<string> EmptyList = Enumerable.Empty<string>().ToList();

		private static readonly ICollection<string> EmptyCollection = Enumerable.Empty<string>().ToList();

		private static readonly IEnumerable<string> EmptyEnumerable = Enumerable.Empty<string>();

		private class StringModel : GenericModel<string>
		{
		}

		private class IntegerModel : GenericModel<int>
		{
		}

		private class NullableIntegerModel : GenericModel<int?>
		{
		}

		private class DecimalModel : GenericModel<decimal>
		{
		}

		private class NullableDecimalModel : GenericModel<decimal?>
		{
		}

		private class FloatModel : GenericModel<float>
		{
		}

		private class NullableFloatModel : GenericModel<float?>
		{
		}

		private class DoubleModel : GenericModel<double>
		{
		}

		private class NullableDoubleModel : GenericModel<double?>
		{
		}

		private class GuidModel : GenericModel<Guid>
		{
		}

		private class NullableGuidModel : GenericModel<Guid?>
		{
		}

		private class EnumModel : GenericModel<ModelEnum>
		{
		}

		private class EnumGuidModel : GenericModel<ModelEnum?>
		{
		}

		private class ComplexModel : GenericModel<Complex>
		{
		}

		private class ArrayModel : GenericModel<byte[]>
		{
		}

		private class ListModel : GenericModel<IList<string>>
		{
		}

		private class CollectionModel : GenericModel<ICollection<string>>
		{
		}

		private class EnumerableModel : GenericModel<IEnumerable<string>>
		{
		}

		public static object[] TestData =
		{
			new object[] { new StringModel { Property = "Hello" }, new StringModel { Property = "World" }, typeof(string), "Hello", "World" },
			new object[] { new StringModel { Property = "Hello" }, new StringModel { Property = null }, typeof(string), "Hello", null },
			new object[] { new StringModel { Property = "Hello" }, new StringModel { Property = string.Empty }, typeof(string), "Hello", string.Empty },
			new object[] { new StringModel { Property = null }, new StringModel { Property = "World" }, typeof(string), null, "World" },
			new object[] { new StringModel { Property = null }, new StringModel { Property = string.Empty }, typeof(string), null, string.Empty },
			new object[] { new IntegerModel { Property = 420 }, new IntegerModel { Property = 69 }, typeof(int), 420, 69 },
			new object[] { new NullableIntegerModel { Property = 420 }, new NullableIntegerModel { Property = null }, typeof(int?), 420, null },
			new object[] { new NullableIntegerModel { Property = null }, new NullableIntegerModel { Property = 69 }, typeof(int?), null, 69 },
			new object[] { new DecimalModel { Property = 420.0m }, new DecimalModel { Property = 69.0m }, typeof(decimal), 420.0m, 69.0m },
			new object[] { new NullableDecimalModel { Property = 420.0m }, new NullableDecimalModel { Property = null }, typeof(decimal?), 420.0m, null },
			new object[] { new NullableDecimalModel { Property = null }, new NullableDecimalModel { Property = 69.0m }, typeof(decimal?), null, 69.0m },
			new object[] { new FloatModel { Property = 420.0f }, new FloatModel { Property = 69.0f }, typeof(float), 420.0f, 69.0f },
			new object[] { new NullableFloatModel { Property = 420.0f }, new NullableFloatModel { Property = null }, typeof(float?), 420.0f, null },
			new object[] { new NullableFloatModel { Property = null }, new NullableFloatModel { Property = 69.0f }, typeof(float?), null, 69.0f },
			new object[] { new DoubleModel { Property = 420.0d }, new DoubleModel { Property = 69.0d }, typeof(double), 420.0d, 69.0d },
			new object[] { new NullableDoubleModel { Property = 420.0d }, new NullableDoubleModel { Property = null }, typeof(double?), 420.0d, null },
			new object[] { new NullableDoubleModel { Property = null }, new NullableDoubleModel { Property = 69.0d }, typeof(double?), null, 69.0d },
			new object[] { new GuidModel { Property = Guid.Parse("c8766aee-9131-4e0f-abd3-d2b7731137c4") }, new GuidModel { Property = Guid.Parse("97f6350b-e0b1-4489-bbde-d6fe5570e36e") }, typeof(Guid), Guid.Parse("c8766aee-9131-4e0f-abd3-d2b7731137c4"), Guid.Parse("97f6350b-e0b1-4489-bbde-d6fe5570e36e") },
			new object[] { new NullableGuidModel { Property = Guid.Parse("c8766aee-9131-4e0f-abd3-d2b7731137c4") }, new NullableGuidModel { Property = null }, typeof(Guid?), Guid.Parse("c8766aee-9131-4e0f-abd3-d2b7731137c4"), null },
			new object[] { new NullableGuidModel { Property = null }, new NullableGuidModel { Property = Guid.Parse("97f6350b-e0b1-4489-bbde-d6fe5570e36e") }, typeof(Guid?), null, Guid.Parse("97f6350b-e0b1-4489-bbde-d6fe5570e36e") },
			new object[] { new EnumModel { Property = ModelEnum.One }, new EnumModel { Property = ModelEnum.Three }, typeof(ModelEnum), ModelEnum.One, ModelEnum.Three },
			new object[] { new EnumGuidModel { Property = ModelEnum.One }, new EnumGuidModel { Property = null }, typeof(ModelEnum?), ModelEnum.One, null },
			new object[] { new EnumGuidModel { Property = null }, new EnumGuidModel { Property = ModelEnum.Three }, typeof(ModelEnum?), null, ModelEnum.Three },
			new object[] { new ComplexModel { Property = Complex.Empty }, new ComplexModel { Property = null }, typeof(Complex), Complex.Empty, null },
			new object[] { new ComplexModel { Property = null }, new ComplexModel { Property = Complex.Empty }, typeof(Complex), null, Complex.Empty },
			new object[] { new ArrayModel { Property = Array.Empty<byte>() }, new ArrayModel { Property = null }, typeof(byte[]), Array.Empty<byte>(), null },
			new object[] { new ArrayModel { Property = null }, new ArrayModel { Property = Array.Empty<byte>() }, typeof(byte[]), null, Array.Empty<byte>() },
			new object[] { new ListModel { Property = EmptyList }, new ListModel { Property = null }, typeof(IList<string>), EmptyList, null },
			new object[] { new ListModel { Property = null }, new ListModel { Property = EmptyList }, typeof(IList<string>), null, EmptyList },
			new object[] { new CollectionModel { Property = EmptyCollection }, new CollectionModel { Property = null }, typeof(ICollection<string>), EmptyCollection, null },
			new object[] { new CollectionModel { Property = null }, new CollectionModel { Property = EmptyCollection }, typeof(ICollection<string>), null, EmptyCollection },
			new object[] { new EnumerableModel { Property = EmptyEnumerable }, new EnumerableModel { Property = null }, typeof(IEnumerable<string>), EmptyEnumerable, null },
			new object[] { new EnumerableModel { Property = null }, new EnumerableModel { Property = EmptyEnumerable }, typeof(IEnumerable<string>), null, EmptyEnumerable },
		};

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldCreateDeltaForSingleSimpleProperty(object first, object second, Type expectedType, object expectedOldValue, object expectedNewValue)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

			ObjectDelta result = first.CompareTo(second);
			Console.WriteLine(result);

			result.Should().NotBeNull();
			result.HasChanges.Should().BeTrue();
			result.PropertyDeltas.Should().HaveCount(1);
			result.PropertyDeltas[0].Name.Should().Be("Property");
			result.PropertyDeltas[0].Path.Should().Be("Property");
			result.PropertyDeltas[0].Type.Should().Be(expectedType);
			result.PropertyDeltas[0].OldValue.Should().Be(expectedOldValue);
			result.PropertyDeltas[0].NewValue.Should().Be(expectedNewValue);
		}
	}
}
