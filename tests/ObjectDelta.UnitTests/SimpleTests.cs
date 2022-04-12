namespace ObjectDelta.UnitTests
{
	using FluentAssertions;
	using NUnit.Framework;
	using ObjectDelta.UnitTests.Model;

	[TestFixture]
	public class SimpleTests
	{
		[Test]
		public void ShouldCreateDeltaForStringProperty()
		{
			TestClass first = new TestClass { String = "Text" };
			TestClass second = new TestClass { String = "Hello" };

			ObjectDelta<TestClass> result = first.CompareTo(second);

			result.Should().NotBeNull();
			result.HasChanges.Should().BeTrue();
			result.PropertyDeltas.Should().HaveCount(1);
			result.PropertyDeltas[0].Name.Should().Be("String");
			result.PropertyDeltas[0].Path.Should().Be("String");
			result.PropertyDeltas[0].DataType.Should().Be(typeof(string));
			result.PropertyDeltas[0].OldValue.Should().Be("Text");
			result.PropertyDeltas[0].NewValue.Should().Be("Hello");
		}
	}
}
