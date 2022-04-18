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
	public class CollectionTests
	{
		public class ArrayModel : GenericModel<string[]>
		{
			public static readonly ArrayModel Null = new ArrayModel();

			public static readonly ArrayModel Empty = new ArrayModel
			{
				Property = Array.Empty<string>()
			};

			public static readonly ArrayModel First = new ArrayModel
			{
				Property = new string[] { "Hello" }
			};

			public static readonly ArrayModel Second = new ArrayModel
			{
				Property = new string[] { "World" }
			};
		}

		public class ListModel : GenericModel<IList<string>>
		{
			public static readonly ListModel Null = new ListModel();

			public static readonly ListModel Empty = new ListModel
			{
				Property = new List<string>()
			};

			public static readonly ListModel First = new ListModel
			{
				Property = new List<string> { "Hello" }
			};

			public static readonly ListModel Second = new ListModel
			{
				Property = new List<string> { "World" }
			};
		}

		public class CollectionModel : GenericModel<ICollection<string>>
		{
			public static readonly CollectionModel Null = new CollectionModel();

			public static readonly CollectionModel Empty = new CollectionModel
			{
				Property = new List<string>()
			};

			public static readonly CollectionModel First = new CollectionModel
			{
				Property = new List<string> { "Hello" }
			};

			public static readonly CollectionModel Second = new CollectionModel
			{
				Property = new List<string> { "World" }
			};
		}

		public class EnumerableModel : GenericModel<IEnumerable<string>>
		{
			public static readonly EnumerableModel Null = new EnumerableModel();

			public static readonly EnumerableModel Empty = new EnumerableModel
			{
				Property = Enumerable.Empty<string>()
			};

			public static readonly EnumerableModel First = new EnumerableModel
			{
				Property = new string[] { "Hello" }
			};

			public static readonly EnumerableModel Second = new EnumerableModel
			{
				Property = new string[] { "World" }
			};
		}

		public static object[] NullTestData =
		{
			new object[] { ArrayModel.Empty, ArrayModel.Null, typeof(string[]), ArrayModel.Empty.Property, null, 1 },
			new object[] { ArrayModel.Null, ArrayModel.Empty, typeof(string[]), null, ArrayModel.Empty.Property, 1 },
			new object[] { ListModel.Empty, ListModel.Null, typeof(IList<string>), ListModel.Empty.Property, null, 1 },
			new object[] { ListModel.Null, ListModel.Empty, typeof(IList<string>), null, ListModel.Empty.Property, 1 },
			new object[] { CollectionModel.Empty, CollectionModel.Null, typeof(ICollection<string>), CollectionModel.Empty.Property, null, 1 },
			new object[] { CollectionModel.Null, CollectionModel.Empty, typeof(ICollection<string>), null, CollectionModel.Empty.Property, 1 },
			new object[] { EnumerableModel.Empty, EnumerableModel.Null, typeof(IEnumerable<string>), EnumerableModel.Empty.Property, null, 1 },
			new object[] { EnumerableModel.Null, EnumerableModel.Empty, typeof(IEnumerable<string>), null, EnumerableModel.Empty.Property, 1 },
		};

		public static object[] EmptyToElementTestData =
		{
			new object[] { ArrayModel.Empty, ArrayModel.First, typeof(string[]), "Property", ArrayModel.Empty.Property, ArrayModel.First.Property },
			new object[] { ListModel.Empty, ListModel.First, typeof(IList<string>), "Property", ListModel.Empty.Property, ListModel.First.Property },
			new object[] { CollectionModel.Empty, CollectionModel.First, typeof(ICollection<string>), "Property", CollectionModel.Empty.Property, CollectionModel.First.Property },
			new object[] { EnumerableModel.Empty, EnumerableModel.First, typeof(IEnumerable<string>), "Property", EnumerableModel.Empty.Property, EnumerableModel.First.Property },
		};

		public static object[] ChangeElementTestData =
		{
			new object[] { ArrayModel.First, ArrayModel.Second, typeof(string[]), "Property[0]", "Hello", "World" },
			new object[] { ListModel.First, ListModel.Second, typeof(IList<string>), "Property[0]", "Hello", "World" },
			new object[] { CollectionModel.First, CollectionModel.Second, typeof(ICollection<string>), "Property[0]", "Hello", "World" },
			new object[] { EnumerableModel.First, EnumerableModel.Second, typeof(IEnumerable<string>), "Property[0]", "Hello", "World" },
		};

		[Test]
		[TestCaseSource(nameof(ChangeElementTestData))]
		public void ShouldCreateDeltaForEnumerableElements_ChangeElement(
			object first,
			object second,
			Type expectedType,
			string expectedPath,
			object expectedOldValue,
			object expectedNewValue)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

			ObjectDelta result = first.CompareTo(second);
			Console.WriteLine(result);

			result.Should().NotBeNull();
			result.HasChanges.Should().BeTrue();
			result.PropertyDeltas.Should().HaveCount(2);

			result.PropertyDeltas.Should().Contain(x => x.Path == expectedPath);
			PropertyDelta propertyDelta = result.PropertyDeltas.First(x => x.Path == expectedPath);

			propertyDelta.Name.Should().Be(expectedPath.Replace("[0]", ""));
			propertyDelta.Path.Should().Be(expectedPath);
			propertyDelta.Type.Should().Be(expectedType);
			propertyDelta.OldValue.Should().Be(expectedOldValue);
			propertyDelta.NewValue.Should().Be(expectedNewValue);
		}

		[Test]
		[TestCaseSource(nameof(EmptyToElementTestData))]
		public void ShouldCreateDeltaForEnumerableElements_EmptyToElement(
			object first,
			object second,
			Type expectedType,
			string expectedPath,
			object expectedOldValue,
			object expectedNewValue)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

			ObjectDelta result = first.CompareTo(second);
			Console.WriteLine(result);

			result.Should().NotBeNull();
			result.HasChanges.Should().BeTrue();
			result.PropertyDeltas.Should().HaveCount(1);

			result.PropertyDeltas[0].Name.Should().Be(expectedPath);
			result.PropertyDeltas[0].Path.Should().Be(expectedPath);
			result.PropertyDeltas[0].Type.Should().Be(expectedType);
			result.PropertyDeltas[0].OldValue.Should().Be(expectedOldValue);
			result.PropertyDeltas[0].NewValue.Should().Be(expectedNewValue);
		}

		[Test]
		[TestCaseSource(nameof(NullTestData))]
		public void ShouldCreateDeltaForEnumerableProperty(
			object first,
			object second,
			Type expectedType,
			object expectedOldValue,
			object expectedNewValue,
			int expectedDeltaCount)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

			ObjectDelta result = first.CompareTo(second);
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
