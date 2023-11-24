# ObjectDelta

A libary that creates a delta of two object instances.

The library is inspired by a blog post of [Daniel Wertheim](https://github.com/danielwertheim) about
producing a delta of two objects of the same type using the library [Structurizer](https://github.com/danielwertheim/structurizer).
Unfortunately [Structurizer](https://github.com/danielwertheim/structurizer) will not provide index information 
for properties with a ```null``` value, the property of a collection and for ```null``` collection items.
This information is needed to be able to record object changes from ```null``` to a value and vice versa. 
I decided to create a library based on [Structurizer](https://github.com/danielwertheim/structurizer) that fits 
my needs for the delta creation.

## Usage

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

## Structure

This creates a flat structure for types and a flat key-value structure for instances.

### Usage

If you just ant to create the schema of a type just pass it to the ```IStructureBuilder```.

```C#
IStructureBuilder builder = new StructureBuilder();
Structure structure = builder.CreateStructure<Customer>();

Console.WriteLine(structure.Schema);
```

This will result in a flat list of the property structure of the type ```Customer```.

```plain
FirstName
LastName
Address
Address.Street
Address.Number
Address.Zip
Address.City
Address.Country
Tags
```

To create the schema and accessors for the values of an instance pass the instance to the ```IStructureBuilder```.

```C#
Customer customer = new Customer
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
	Tags = new string[] { "detective", "addict" }
};

IStructureBuilder builder = new StructureBuilder();
Structure structure = builder.CreateStructure(customer);

Console.WriteLine(structure.Indices);
```

This will result in a flat list of the property values in the given instance of type ```Customer```.

```plain
FirstName = "Sherlock"
LastName = "Holmes"
Address = "ConsoleApp1.Address"
Address.Street = "Baker Street"
Address.Number = "221b"
Address.Zip = "NW1"
Address.City = "London"
Address.Country = "England"
Tags = "System.String[]"
Tags[0] = "detective"
Tags[1] = "addict"
```

## Acknowledgement

Most of the terminology and code structure of the library comes from the libary [Structurizer](https://github.com/danielwertheim/structurizer)
by [Daniel Wertheim](https://github.com/danielwertheim). This is basically a fork of his work which I bent to my needs, f.e. the need
to support ```null``` values in index values and some other fixes and tweaks. I completely remove the configuration part and added the
ability to just get the schema of a type. The library this work is based on is licensed under the MIT license and so it this library.

## References

[Daniel Wertheim](https://github.com/danielwertheim)
	
- [Structurizer](https://github.com/danielwertheim/structurizer)
- [How to build a simple object graph delta comparer in C# using Structurizer](https://danielwertheim.se/how-to-build-a-simple-object-graph-delta-comparer-in-csharp-using-structurizer/)
