# ObjectDelta

A libary that creates a delta of two object instances.

The library is inspired by a blog post of [Daniel Wertheim](https://github.com/danielwertheim) about
producing a delta of two objects of the same type using the library [Structurizer](https://github.com/danielwertheim/structurizer).
Unfortunately [Structurizer](https://github.com/danielwertheim/structurizer) will not provide index information 
for properties with a ```null``` value, the property of a collection and for ```null``` collection items.
This information is needed to be able to record object changes from ```null``` to a value and vice versa. 
I decided to create a library based on [Structurizer](https://github.com/danielwertheim/structurizer) that fits 
my needs for the delta creation.

# Usage

```C#
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
```

The resulting ```ObjectDelta<Customer>``` provides a flat list containing the deltas for every property,
inclusing nested properties of complex types (like the ```Address```) and colleection items. 

The output of ```ToString()``` of the result of operation above:

```plain
LastName (Olmes => Holmes)
Address (ConsoleApp1.Address => ConsoleApp1.Address)
Address.Number (900 => 221b)
Tags (System.String[] => System.String[])
Tags[1] (addict => genius)
Tags[2] (null => addict)
```

## References

[Daniel Wertheim](https://github.com/danielwertheim)
	
- [Structurizer](https://github.com/danielwertheim/structurizer)
- [How to build a simple object graph delta comparer in C# using Structurizer](https://danielwertheim.se/how-to-build-a-simple-object-graph-delta-comparer-in-csharp-using-structurizer/)
