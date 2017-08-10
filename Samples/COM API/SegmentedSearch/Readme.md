# Segmented search

By default, the [SearchForObjectsByConditions](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations~SearchForObjectsByConditions.html) method on [VaultObjectSearchOperations](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations.html) will limit the number of items it returns to 500.  An extended [SearchForObjectsByConditionsEx](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations~SearchForObjectsByConditionsEx.html) method allows the caller to both specify a larger maximum count and longer timeout, but may still return limited results in some situations.  This example introduces an approach that can bypass this limitation and iterate over all content that matches the search conditions.

*No sample vault is included for this sample.*

## Firstly: the downsides

Searching over this volume of content can cause significant load on the M-Files server.  Whilst this code will happily iterate many hundreds of thousands of items, doing so may not be advisable in production scenarios.

Additionally, this approach takes significantly more time than executing a single search.  It may be useful to execute this code in scenarios where the user does not have to wait for the results.  In some ad-hoc testing against a large sample vault, searching this way took approximately twice the duration of searching using the standard methods.

## The approach

All objects within M-Files have an internal numeric "Object Id".  This object Id is normally shown in the top-left of the metadata card and is the Id which is used both internally and via the APIs to refer to individual objects.  In this approach we will "segment" the search call into multiple smaller requests, each only returning a small number of results.

Our code will look something like this:

* Assign a constant named `itemsPerSegment` to 1000.
* Assign a variable named `segment` to zero.
* Execute the following until we reach the end of the objects <sup>1</sup>:
  * Take a copy of the search conditions (`internalSearchConditions`).
  * Add an additional condition, forcing it to search for objects with ID between `segment * itemsPerSegment` and `(segment + 1) * itemsPerSegment`.
  * Execute the segment search, and return the results.

*Note that the number of items in each segment may not be 1000, even if there are no other search conditions restricting the results.  In situations where objects are permanently destroyed, the segment itself may only contain a small number of objects.  This does not mean that the last objects have been reached.*

[1] In order to find whether we have any more objects to return, we have to execute an additional search:

* Take a copy of the search conditions (`internalSearchConditions`).
* Add an additional condition, forcing it to search for items with a minimum ID greater than the current segment.
* Execute the search, only returning a maximum of one item; we only need to know if there is at least one item, not all of the items.

## The C# code

The code  (`Program.cs`) contains two implementations of the search code:

1. The `UseLibrary` method uses the M-Files API Helper Library to execute the various searches.  This library wraps some complexities of connecting/disconnecting from the vault, and creation of the SearchCondition objects.  The purpose of this code is to show the specific approach as cleanly as possible.
2. The `UseAPI` method uses the M-Files API directly.  This method shows the specific code required to use this approach directly against the API.  It contains more boiler-plate code but is useful both for learning, and for situations where the M-Files API Helper Library cannot be used.

:warning: Note that the code shown below retrieves all items from the vault in order to then simply  `.Count` the results.  When using M-Files 2015.3 and upwards, the [GetObjectCountInSearch](https://www.m-files.com/api/documentation/2015.3/index.html#MFilesAPI~VaultObjectSearchOperations~GetObjectCountInSearch.html) method is preferable, as this will simply return the count rather than all matching object versions.

### UseLibrary

This sample uses a number of helper methods from the M-Files API Helper Library.  These helper methods make connecting to the vault simpler, as well as removing the complexity of creating search conditions.

```csharp
// Declare variables for our vault connection.
Vault vault;
MFilesServerApplication application;

// The default connection (localhost, tcp, current Windows user) will suffice.
var connectionDetails = new ConnectionDetails();

// Connect to the vault using the helper method.
connectionDetails.ConnectToVaultAdministrative(
	Guid.Parse("{ ... }"),
	out vault, out application);

// Load the object types from the vault.
System.Console.WriteLine("Loading object types...");
var objectTypes = vault
	.ObjectTypeOperations
	.GetObjectTypes()
	.Cast<ObjType>()
	.ToList();
System.Console.WriteLine($"Iterating over {objectTypes.Count} object types...");

// Iterate over the object types to count the objects.
foreach (var objectType in objectTypes)
{
	// Create the basic search conditions collection.
	var searchConditions = new SearchConditions();

	// Add a condition for the object type we're interested in.
	searchConditions.AddObjectTypeIdSearchCondition(objectType.ID);

	// Count the items in this object type, including deleted ones.
	// This will execute the search in segments (i.e. items 0-1000, then 1001-2000),
	// but will "flatten" the results into a collection of ObjectVersion objects.
	var countIncludingDeleted = vault.ObjectSearchOperations
		.SearchForObjectsByConditionsSegmented_Flat(searchConditions)
		.Count();

	// Output the stats.
	System.Console.WriteLine($"\t{objectType.NamePlural}:");
	System.Console.WriteLine($"\t\tTotal: {countIncludingDeleted} (included deleted)");

}

System.Console.WriteLine($"Complete.");

// Ensure we're disconnected.
application.Disconnect(vault);
```

### UseAPI

This sample uses the M-Files API directly and does not use any helper methods from the M-Files API Helper Library.  This code builds the search conditions individually and shows how the logic is implemented within the helper functions.  Note that this code is approximately four times longer than the code above.

```csharp
// Connect to the server (localhost, tcp, current Windows user).
var application = new MFilesServerApplication();
application.ConnectAdministrative();

// Get a connection to the vault.
var vault = application.LogInToVault("{ ... }");

// Load the object types from the vault.
System.Console.WriteLine("Loading object types...");
var objectTypes = vault
	.ObjectTypeOperations
	.GetObjectTypes()
	.Cast<ObjType>()
	.ToList();
System.Console.WriteLine($"Iterating over {objectTypes.Count} object types...");

// Iterate over the object types to count the objects.
foreach (var objectType in objectTypes)
{
	// Create the basic search conditions collection.
	var searchConditions = new SearchConditions();

	// Add a condition for the object type we're interested in.
	{
		// Create the search condition (for object type id).
		SearchCondition condition = new SearchCondition
		{
			ConditionType = MFConditionType.MFConditionTypeEqual
		};
		condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectTypeID, null);
		condition.TypedValue.SetValue(MFDataType.MFDatatypeLookup, objectType.ID);

		// Add the condition at the index provided.
		searchConditions.Add(-1, condition);
	}

	// Create variables for the segment information.
	const int itemsPerSegment = 1000; // Maximum number of items in each segment.
	var segment = 0; // Start; this will increment as we go.
	var moreItems = true; // Whether there are more items to load.
	var countIncludingDeleted = 0; // The count of matching items.

	// Whilst there are items in the results, we need to loop.
	while (moreItems)
	{
		// Execute a search within the object id segment.
		{
			// Clone the search conditions (so we can add current-segment condition).
			var internalSearchConditions = searchConditions.Clone();

			// Add search condition:
			//   Id within the range: (segment - itemsPerSegment) to ((segment + 1) * itemsPerSegment)
			{
				// Create the search condition.
				SearchCondition condition = new SearchCondition
				{
					ConditionType = MFConditionType.MFConditionTypeEqual
				};
				condition.Expression.SetObjectIDSegmentExpression(itemsPerSegment);
				condition.TypedValue.SetValue(MFDataType.MFDatatypeInteger, segment);

				// Add the condition at the index provided.
				internalSearchConditions.Add(-1, condition);
			}

			// Execute the search and increment the count.
			countIncludingDeleted += vault.ObjectSearchOperations
				.SearchForObjectsByConditionsEx(internalSearchConditions, MFSearchFlags.MFSearchFlagDisableRelevancyRanking,
					SortResults: false, MaxResultCount: 0,
					SearchTimeoutInSeconds: 0).Count;

			// Move to the next segment.
			segment++;

		}

		// Are there any more items?
		{
			// Clone the search conditions (so we can add object id condition).
			var internalSearchConditions = searchConditions.Clone();

			// Add search condition:
			//   Id at least (segment * itemsPerSegment)
			{
				// Create the search condition.
				SearchCondition condition = new SearchCondition
				{
					ConditionType = MFConditionType.MFConditionTypeGreaterThanOrEqual
				};
				condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectID, null);
				condition.TypedValue.SetValue(MFDataType.MFDatatypeInteger, segment * itemsPerSegment);

				// Add the condition at the index provided.
				internalSearchConditions.Add(-1, condition);
			}

			// If we get one item then there's more results.
			moreItems = 1 == vault.ObjectSearchOperations.SearchForObjectsByConditionsEx(
				internalSearchConditions, // Our search conditions.
				MFSearchFlags.MFSearchFlagDisableRelevancyRanking,
				SortResults: false, // Don't bother attempting to sort them.
				MaxResultCount: 1, // We only need to know if there is at least one, nothing more.
				SearchTimeoutInSeconds: 0).Count;
		}
	}

	// Output the stats.
	System.Console.WriteLine($"\t{objectType.NamePlural}:");
	System.Console.WriteLine($"\t\tTotal: {countIncludingDeleted} (included deleted)");

}

System.Console.WriteLine($"Complete.");

// Disconnect.
vault.LogOutSilent();
application.Disconnect();
```
