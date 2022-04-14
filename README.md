# ObjectDelta

A libary that creates a delta of two object instaces.

The library is heavily inspired by a blog post of [Daniel Wertheim](https://github.com/danielwertheim)  about
producing a delta of two objects of the same type using his nice library [Structurizer](https://github.com/danielwertheim/structurizer).
Unfortunately [Structurizer](https://github.com/danielwertheim/structurizer) will not provide index information 
for properties with a ```null``` value, the property of a collection and for ```null``` collection items.
This information is needed to be able to record object changes from ```null``` to a value and vice versa. 
I decided to hide the indexing behind another abstraction and to implement it using a modified version of
the [Structurizer](https://github.com/danielwertheim/structurizer) library. If [Structurizer](https://github.com/danielwertheim/structurizer)
supports this in the future I will switch to the NuGet packaged version.

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
	Tags = new string[] { "detective", "smart", "addict" }
};

ObjectComparer comparer = new ObjectComparer();
ObjectDelta<Customer> result = comparer.Compare(first, second);
```

The resulting ```ObjectDelta<Customer>``` provides a flat list containing the deltas for every property,
inclusing nested properties of complex types (like the ```Address```) and colleection items. 

The output of ```ToString()``` of the result of operation above:

```plain
[
	Name=LastName, Path=LastName, OldValue=Olmes, NewValue=Holmes, DataType=String, DataTypeCode=String, IsNumeric=False,
	Name=Address.Number, Path=Address.Number, OldValue=900, NewValue=221b, DataType=String, DataTypeCode=String, IsNumeric=False,
	Name=Tags, Path=Tags[1], OldValue=smart, NewValue=addict, DataType=String[], DataTypeCode=String, IsNumeric=False
]
```

## References

[Daniel Wertheim](https://github.com/danielwertheim)
	
- [Structurizer](https://github.com/danielwertheim/structurizer)
- [How to build a simple object graph delta comparer in C# using Structurizer](https://danielwertheim.se/how-to-build-a-simple-object-graph-delta-comparer-in-csharp-using-structurizer/)
