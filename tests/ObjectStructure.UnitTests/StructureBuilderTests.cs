namespace ObjectStructure.UnitTests
{
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class StructureBuilderTests
	{
		[Test]
		public void Test()
		{
			IStructureBuilder builder = new StructureBuilder();
			Structure structure = builder.CreateStructure(new Customer
			{
				Firstname = "Tester"
			});

			structure.Should().NotBeNull();
			structure.Indices.Should().HaveCount(1);
		}
	}

	public class Customer
	{
		public string Firstname { get; set; }
	}
}
