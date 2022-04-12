namespace ObjectDelta.UnitTests.Model
{
	public class Customer
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public Address Address { get; set; }

		public string[] Tags { get; set; }
	}
}
