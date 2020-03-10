# Enumerating the vault structure

This sample shows how to retrieve various elements of the vault structure using the REST API wrapper for .NET.

## Using the REST API wrapper

[The REST API](https://github.com/M-Files/Libraries.MFWSClient) wrapper exposes various methods which mimic the [M-Files API](https://www.m-files.com/api/documentation/latest/index.html) to retrieve the vault structure.

```csharp
// Connect to the online knowledgebase.
// Note that this doesn't require authentication.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Get the object types.
var objectTypes = client.ObjectTypeOperations.GetObjectTypes();
Console.WriteLine($"There are {objectTypes.Count} object types in the vault:");
foreach (var item in objectTypes)
{
	// Output basic content.
	System.Console.WriteLine($"\t{item.Name} ({item.NamePlural})");
	System.Console.WriteLine($"\t\tID: {item.ID}");
}

// Get the value lists.
var valueLists = client.ValueListOperations.GetValueLists();
Console.WriteLine($"There are {valueLists.Count} value lists in the vault:");
foreach (var item in valueLists)
{
	// Output basic content.
	System.Console.WriteLine($"\t{item.Name}");
	System.Console.WriteLine("\t\tID: {item.ID}");

	// Retrieve the items.
	var valueListItems = client.ValueListItemOperations.GetValueListItems(item.ID);
	System.Console.WriteLine($"\t\tItems ({valueListItems.Items.Count}):");

	// Output the items.
	foreach (var valueListItem in valueListItems.Items)
	{
		System.Console.WriteLine($"\t\t\t{valueListItem.Name}, ID: {valueListItem.ID}");
	}

}
```


