namespace ConsoleApp1
{
	using System;
	using ObjectDelta;

	internal static class Program
	{
		public static void Main(string[] args)
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
				Tags = new string[] { "detective", "addict" }
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
				Tags = new string[] { "detective", "genius", "addict" }
			};

			ObjectDelta<Customer> result = ObjectComparer.Compare(first, second);
			Console.WriteLine(result);
		}
	}
}
