namespace ObjectDelta.UnitTests
{
	using System;
	using NUnit.Framework;
	using ObjectDelta.UnitTests.Model;

	[TestFixture]
	public class ComplexSampleTests
	{
		//[Test]
		//public void ShouldCreateCorrectDeltas()
		//{
		//	Customer first = new Customer
		//	{
		//		FirstName = "Daniel",
		//		LastName = "Andersson",
		//		Address = new Address
		//		{
		//			Street = "Some street 1",
		//			Zip = "11111",
		//			City = "City1",
		//			Country = "Sweden"
		//		},
		//		Tags = new string[] { "Test1", "Test3" }
		//	};

		//	Customer second = new Customer
		//	{
		//		FirstName = "Daniel",
		//		LastName = "Olsson",
		//		Address = new Address
		//		{
		//			Street = "Some street 1",
		//			Zip = "22222",
		//			City = "City2",
		//			Country = "Sweden"
		//		},
		//		Tags = new string[] { "Test1", "Test2", "Test3" }
		//	};

		//	ObjectComparer comparer = new ObjectComparer();
		//	ObjectDelta<Customer> result = comparer.Compare(first, second);

		//	//result.PropertyDeltas.Should().HaveCount(11);
		//	//result.PropertyDeltas[0].Name.Should().Be("LastName");
		//	//result.PropertyDeltas[0].OldValue.Should().Be("Andersson");
		//	//result.PropertyDeltas[0].NewValue.Should().Be("Olsson");

		//	//result.PropertyDeltas[1].Name.Should().Be("Address.Zip");
		//	//result.PropertyDeltas[1].OldValue.Should().Be("11111");
		//	//result.PropertyDeltas[1].NewValue.Should().Be("22222");

		//	//result.PropertyDeltas[2].Name.Should().Be("Address.City");
		//	//result.PropertyDeltas[2].OldValue.Should().Be("City1");
		//	//result.PropertyDeltas[2].NewValue.Should().Be("City2");

		//	//result.PropertyDeltas[3].Name.Should().Be("Tags");
		//	//result.PropertyDeltas[3].Path.Should().Be("Tags[1]");
		//	//result.PropertyDeltas[3].OldValue.Should().Be("Test3");
		//	//result.PropertyDeltas[3].NewValue.Should().Be("Test2");

		//	Console.WriteLine(result);
		//}

		[Test]
		public void ShouldPrintToString()
		{
			Customer first = new Customer
			{
				FirstName = "Sherlock",
				LastName = "Olmes",
				Address = new Address
				{
					Street = "Baker Street",
					Number = "900",
					Zip = "NW1",
					City = "London",
					Country = "England"
				},
				Tags = new string[] { "detective", "smart" }
			};

			Customer second = new Customer
			{
				FirstName = "Sherlock",
				LastName = "Holmes",
				Address = new Address
				{
					Street = "Baker Street",
					Number = "221b",
					Zip = "NW1",
					City = "London",
					Country = "England"
				},
				Tags = new string[] { "detective", "addict", "smart" }
			};

			ObjectDelta<Customer> result = ObjectComparer.Compare(first, second);
			Console.WriteLine(result);
		}
	}
}
