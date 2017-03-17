# Search by display / external id

Whilst some [object types](http://www.m-files.com/user-guide/latest/eng/#Object_types.html) modelled within an M-Files vault will be created directly within M-Files, it is also possible for objects to be automatically retrieved from [external systems](http://www.m-files.com/user-guide/latest/eng/#Object_types.html).  This allows other business systems (e.g. a CRM) to manage business objects (e.g. Customers or Contacts), but for the content to be available within M-Files for reference.

In these situations, the object ID shown on the [metadata card](http://www.m-files.com/user-guide/latest/eng/#metadata_card.html) will be the ID from the external system, not the ID within M-Files.  As such, it may be useful to search the M-Files vault to find the [ObjID](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~ObjID.html) (the internal ID) of the item within M-Files so that it can be used for other operations within the API.

## The approach

In order to search for an item containing a specific external/display Id, a `SearchCondition` must be created as below:

```csharp
SearchCondition condition = new SearchCondition
{
	ConditionType = MFConditionType.MFConditionTypeEqual,
};
condition.Expression.DataStatusValueType = MFStatusType.MFStatusTypeExtID;
condition.TypedValue.SetValue(MFDataType.MFDatatypeText, "MyDisplayId");
```

## The C# code

The code  (`Program.cs`) contains two implementations of the search code:

1. The `UseLibrary` method uses the M-Files API Helper Library to execute the various searches.  This library wraps some complexities of connecting/disconnecting from the vault, and creation of the SearchCondition objects.  The purpose of this code is to show the specific approach as cleanly as possible.
2. The `UseAPI` method uses the M-Files API directly.  This method shows the specific code required to use this approach directly against the API.  It contains more boiler-plate code but is useful both for learning, and for situations where the M-Files API Helper Library cannot be used.

### UseLibrary

This sample uses a number of helper methods from the M-Files API Helper Library.  These helper methods make connecting to the vault simpler, as well as removing the complexity of creating search conditions.

The M-Files API Helper Library contains an extension method for the `SearchConditions` class named `AddDisplayIdSearchCondition`

```csharp
// Declare variables for our vault connection.
Vault vault;
MFilesServerApplication application;

// The default connection (localhost, tcp, current Windows user) will suffice.
var connectionDetails = new ConnectionDetails();

// Connect to the vault.
connectionDetails.ConnectToVaultAdministrative(
	Program.sampleVaultGuid,
	out vault, out application);

// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Add a condition for the display Id.
searchConditions.AddDisplayIdSearchCondition("MyDisplayId");

// Search.
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);

// Output the number of items matching (should be one in each object type, at a maximum).
System.Console.WriteLine($"There were {results.Count} objects with the display Id of {Program.customerDisplayId}:");

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
var vault = application.LogInToVault(Program.sampleVaultGuid.ToString("B"));

// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Create the search condition.
SearchCondition condition = new SearchCondition
{
	ConditionType = MFConditionType.MFConditionTypeEqual,
};
condition.Expression.DataStatusValueType = MFStatusType.MFStatusTypeExtID;
condition.TypedValue.SetValue(MFDataType.MFDatatypeText, Program.customerDisplayId);

// Add the condition to the collection.
searchConditions.Add(-1, condition);

// Search.
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);

// Output the number of items matching (should be one in each object type, at a maximum).
System.Console.WriteLine($"There were {results.Count} objects with the display Id of {Program.customerDisplayId}:");

System.Console.WriteLine($"Complete.");

// Disconnect.
vault.LogOutSilent();
application.Disconnect();
```
