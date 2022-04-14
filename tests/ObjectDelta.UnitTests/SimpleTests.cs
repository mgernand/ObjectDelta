//namespace ObjectDelta.UnitTests
//{
//	using System;
//	using System.Collections.Generic;
//	using System.Globalization;
//	using System.Threading;
//	using FluentAssertions;
//	using NUnit.Framework;
//	using ObjectDelta.UnitTests.Model;

//	[TestFixture]
//	public class SimpleTests
//	{
//		private static readonly ComplexTestClass EmptyComplexValue = new ComplexTestClass();
//		private static readonly IList<string> EmptyList = new List<string>();

//		public static object[] TestData =
//		{
//			new object[]
//			{
//				new TestClass { StringProperty = "Hello" },
//				new TestClass { StringProperty = "World" },
//				nameof(TestClass.StringProperty),
//				nameof(TestClass.StringProperty),
//				typeof(string),
//				"Hello",
//				"World"
//			},
//			new object[]
//			{
//				new TestClass { StringProperty = "Hello" },
//				new TestClass { StringProperty = null },
//				nameof(TestClass.StringProperty),
//				nameof(TestClass.StringProperty),
//				typeof(string),
//				"Hello",
//				null
//			},
//			new object[]
//			{
//				new TestClass { StringProperty = "Hello" },
//				new TestClass { StringProperty = string.Empty },
//				nameof(TestClass.StringProperty),
//				nameof(TestClass.StringProperty),
//				typeof(string),
//				"Hello",
//				string.Empty
//			},
//			new object[]
//			{
//				new TestClass { StringProperty = null },
//				new TestClass { StringProperty = "World" },
//				nameof(TestClass.StringProperty),
//				nameof(TestClass.StringProperty),
//				typeof(string),
//				null,
//				"World"
//			},
//			new object[]
//			{
//				new TestClass { StringProperty = null },
//				new TestClass { StringProperty = string.Empty },
//				nameof(TestClass.StringProperty),
//				nameof(TestClass.StringProperty),
//				typeof(string),
//				null,
//				string.Empty
//			},
//			new object[]
//			{
//				new TestClass { IntegerProperty = 42069 },
//				new TestClass { IntegerProperty = 666 },
//				nameof(TestClass.IntegerProperty),
//				nameof(TestClass.IntegerProperty),
//				typeof(int),
//				42069,
//				666
//			},
//			new object[]
//			{
//				new TestClass { NullableIntegerProperty = 42069 },
//				new TestClass { NullableIntegerProperty = null },
//				nameof(TestClass.NullableIntegerProperty),
//				nameof(TestClass.NullableIntegerProperty),
//				typeof(int),
//				42069,
//				null
//			},
//			new object[]
//			{
//				new TestClass { NullableIntegerProperty = null },
//				new TestClass { NullableIntegerProperty = 666 },
//				nameof(TestClass.NullableIntegerProperty),
//				nameof(TestClass.NullableIntegerProperty),
//				typeof(int),
//				null,
//				666
//			},
//			new object[]
//			{
//				new TestClass { DecimalProperty = 420.69m },
//				new TestClass { DecimalProperty = 6.66m },
//				nameof(TestClass.DecimalProperty),
//				nameof(TestClass.DecimalProperty),
//				typeof(decimal),
//				420.69m,
//				6.66m
//			},
//			new object[]
//			{
//				new TestClass { DoubleProperty = 420.69d },
//				new TestClass { DoubleProperty = 6.66d },
//				nameof(TestClass.DoubleProperty),
//				nameof(TestClass.DoubleProperty),
//				typeof(double),
//				420.69d,
//				6.66d
//			},
//			new object[]
//			{
//				new TestClass { FloatProperty = 420.69f },
//				new TestClass { FloatProperty = 6.66f },
//				nameof(TestClass.FloatProperty),
//				nameof(TestClass.FloatProperty),
//				typeof(float),
//				420.69f,
//				6.66f
//			},
//			new object[]
//			{
//				new TestClass { GuidProperty = Guid.Parse("c8766aee-9131-4e0f-abd3-d2b7731137c4") },
//				new TestClass { GuidProperty = Guid.Parse("97f6350b-e0b1-4489-bbde-d6fe5570e36e") },
//				nameof(TestClass.GuidProperty),
//				nameof(TestClass.GuidProperty),
//				typeof(Guid),
//				Guid.Parse("c8766aee-9131-4e0f-abd3-d2b7731137c4"),
//				Guid.Parse("97f6350b-e0b1-4489-bbde-d6fe5570e36e")
//			},
//			new object[]
//			{
//				new TestClass { EnumProperty = TestEnum.One },
//				new TestClass { EnumProperty = TestEnum.Three },
//				nameof(TestClass.EnumProperty),
//				nameof(TestClass.EnumProperty),
//				typeof(TestEnum),
//				TestEnum.One,
//				TestEnum.Three
//			},
//			new object[]
//			{
//				new TestClass { ComplexProperty = null },
//				new TestClass { ComplexProperty = EmptyComplexValue },
//				nameof(TestClass.ComplexProperty),
//				nameof(TestClass.ComplexProperty),
//				typeof(TestEnum),
//				null,
//				EmptyComplexValue
//			},
//			new object[]
//			{
//				new TestClass { ComplexProperty = EmptyComplexValue },
//				new TestClass { ComplexProperty = null },
//				nameof(TestClass.ComplexProperty),
//				nameof(TestClass.ComplexProperty),
//				typeof(TestEnum),
//				EmptyComplexValue,
//				null
//			},
//			new object[]
//			{
//				new TestClass { ListProperty = null },
//				new TestClass { ListProperty = EmptyList },
//				nameof(TestClass.ListProperty),
//				nameof(TestClass.ListProperty),
//				typeof(TestEnum),
//				null,
//				EmptyList
//			},
//			new object[]
//			{
//				new TestClass { ListProperty = EmptyList },
//				new TestClass { ListProperty = null },
//				nameof(TestClass.ListProperty),
//				nameof(TestClass.ListProperty),
//				typeof(TestEnum),
//				EmptyList,
//				null
//			},
//		};

//		[Test]
//		[TestCaseSource(nameof(TestData))]
//		public void ShouldCreateDeltaForSingleSimpleProperty(TestClass first, TestClass second,
//			string expectedName, string expectedPath, Type expectedType,
//			object expectedOldValue, object expectedNewValue)
//		{
//			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
//			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

//			ObjectDelta<TestClass> result = first.CompareTo(second);
//			Console.WriteLine(result);

//			result.Should().NotBeNull();
//			result.HasChanges.Should().BeTrue();
//			result.PropertyDeltas.Should().HaveCount(1);
//			result.PropertyDeltas[0].Name.Should().Be(expectedName);
//			result.PropertyDeltas[0].Path.Should().Be(expectedPath);
//			result.PropertyDeltas[0].DataType.Should().Be(expectedType);
//			result.PropertyDeltas[0].OldValue.Should().Be(expectedOldValue);
//			result.PropertyDeltas[0].NewValue.Should().Be(expectedNewValue);
//		}

//		[Test]
//		public void Test()
//		{
//			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
//			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

//			TestClass first = new TestClass { StringProperty = null };
//			TestClass second = new TestClass { StringProperty = "World" };

//			ObjectDelta<TestClass> result = first.CompareTo(second);
//			Console.WriteLine(result);

//			result.Should().NotBeNull();
//			result.HasChanges.Should().BeTrue();
//			result.PropertyDeltas.Should().HaveCount(1);
//			result.PropertyDeltas[0].Name.Should().Be("StringProperty");
//			result.PropertyDeltas[0].Path.Should().Be("StringProperty");
//			result.PropertyDeltas[0].DataType.Should().Be(typeof(string));
//			result.PropertyDeltas[0].OldValue.Should().Be(null);
//			result.PropertyDeltas[0].NewValue.Should().Be("World");
//		}
//	}
//}


