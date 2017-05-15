# MFaaP.MFilesAPI (M-Files API Helper Library)

*Please note that these libraries are provided "as-is" and with no warranty, explicit or otherwise.  You should ensure that the functionality of these libraries meet your requirements, and thoroughly test them, prior to using in any production scenarios.*

## Overview

This library aims to improve the ease of using the [M-Files API](http://developer.m-files.com/APIs/COM-API/) from within Microsoft .NET environments.  The library contains a number of helper objects and extension methods.

* Helper objects and extension methods to aid connecting to (and disconnecting from) a server, when using the [MFServerApplication](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFilesServerApplication.html) mode.
* Extension methods to aid creation of [SearchCondition](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~SearchCondition.html) objects, and searching against a given vault.

## Connecting to a server (MFilesServerApplication)

There are two modes of interacting with the M-Files API: client or server:

* In the client mode, the API uses [locally-mapped vault connections](http://www.m-files.com/user-guide/latest/eng/#Document_vault_connections.html), can interact with the vault via the local file system, and can throw system dialogs such as the [new object metadata card window](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectOperations~ShowNewObjectWindow.html).

* In the server mode, the API needs to be explicitly provided with details on the server and the authentication scheme to use.  This library contains some helper objects to aid with this process.

<p class="note">The library does not currently support SAML/token-based authentication schemes such as those used with <a href="https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFilesServerApplication~ConnectWithAuthenticationDataEx3.html">ConnectWithAuthenticationDataEx3</a>.</p>

### A standard connection

A standard connection, using a named user, connecting to a remote M-Files server via HTTPS may look something like this:

```csharp
// Declare the vault guid.
var vaultGuid = Guid.Parse("{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}");

// Declare variables for our vault connection.
Vault vault = null;
MFilesServerApplication application = new MFilesServerApplication();

// Attempt to connect
try
{
    // Connect, passing it the details.
    serverApplication.Connect(
	    AuthType: MFAuthType.MFAuthTypeSpecificMFilesUser,
	    UserName: "Craig",
	    Password: "MyPassword",
	    ProtocolSequence: "ncacn_http",
	    NetworkAddress: "m-files.mycompany.com",
	    Endpoint: "4466");

    // Obtain a connection to the vault.
    vault = serverApplication.LogInToVault(vaultGuid.ToString("B"));
}
catch(Exception e)
{
    // TODO: Exception handling - connection/login failed.
}
```

Values such as the [ProtocolSequence](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFilesServerApplication~Connect.html) and the vault GUID need to be correctly formatted.

### A connection using the library

A connection using the library allows for fewer connection variables, and the code will automatically ensure that elements such as the vault GUID are correctly formatted.  The library will also automatically provide [current timezone and culture information](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFilesServerApplication~ConnectEx4.html) to the server as part of the connection. 

```csharp
// Declare the vault guid.
var vaultGuid = Guid.Parse("{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}");

// Declare variables for our vault connection.
Vault vault;
MFilesServerApplication application;

// Create the connection details.
var connectionDetails = new ConnectionDetails()
{
	AuthenticationDetails 
		= AuthenticationDetails.CreateForSpecificMFilesUser("Craig", "MyPassword"),
	ServerDetails 
		= ServerDetails.CreateForHttps("m-files.mycompany.com")
};

// Connect to the vault.
if(false == connectionDetails.TryConnectToVaultAdministrative(
	vaultGuid,
	out vault, out application))
{
    // TODO: connection/login failed.
}
```

## Searching

Creation of [SearchCondition](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~SearchCondition.html) can initially be an obscure concept, especially when creating search conditions to represent infrequently-used conditions such as a free-text search, or searching by an external object's display Id.

The following extension methods are available for [SearchConditions](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~SearchConditions.html) which create search condition objects and automatically add them to the collection:

* `AddNotDeletedSearchCondition` - adds a "not deleted" search condition to the given [SearchConditions collection](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~SearchConditions.html).

```csharp
// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Add a condition to filter out deleted items.
searchConditions.AddNotDeletedSearchCondition();

// Search (for all not-deleted items).
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);
```

* `AddObjectTypeSearchCondition` - adds a search condition restricting the results to a single object type.

```csharp
// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Add a condition for an object type Id.
searchConditions.AddObjectTypeSearchCondition(0);

// Search (for all documents).
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);
```

* `AddMinimumObjectIdSearchCondition` - used in combination with `AddObjectIdSegmentSearchCondition` to execute a [segmented search](https://github.com/M-Files/MFilesSamplesAndLibraries/tree/master/Samples/SegmentedSearch).

```csharp
// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Add a condition for a minimum object Id.
searchConditions.AddMinimumObjectIdSearchCondition(1000);

// Search (for all objects with an object Id greater than or equal to 1000).
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);
```

* `AddObjectIdSegmentSearchCondition` used in combination with `AddMinimumObjectIdSearchCondition` to execute a [segmented search](https://github.com/M-Files/MFilesSamplesAndLibraries/tree/master/Samples/SegmentedSearch).

```csharp
// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Add a condition for an object id in a range/segment.
searchConditions.AddObjectIdSegmentSearchCondition(0, 1000);

// Search (for all objects with an object Id between 0 and 999).
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);
```

* `AddDisplayIdSearchCondition` - adds a search condition restricting the results to an object with a specific external / display id.

```csharp
// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Add a condition for an object's external/display Id.
searchConditions.AddDisplayIdSearchCondition("CUST0001");

// Search (for all objects with an external/display Id of "CUST0001").
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);
```

* `AddFullTextSearchCondition` - adds a search condition executing a full-text search for a given text string.  Can be restricted to search in metadata, file data, or both (default). 

```csharp
// Create the basic search conditions collection.
var searchConditions = new SearchConditions();

// Add a full-text search condition (akin to the main search in the M-Files client).
// Note: this can be restricted to metadata or file data only, if required.
searchConditions.AddFullTextSearchCondition("hello world");

// Search (for all objects matching the full-text query).
var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
	MFSearchFlags.MFSearchFlagNone, SortResults: false);
```


