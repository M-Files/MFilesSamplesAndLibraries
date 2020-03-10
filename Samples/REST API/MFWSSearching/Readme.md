# Searching using the M-Files Web Service

The M-Files Web Service exposes an [endpoint](http://www.m-files.com/mfws/resources/objects.html) which can be used to execute searches against the current M-Files vault.  Searching against the vault involves providing one or more [search conditions](http://www.m-files.com/mfws/syntax.html#sect:search-encoding) via the querystring.

Note that, unlike the M-Files COM API, the M-Files Web Service will exclude deleted objects from the search results by default.

## Using System.Net.Http.HttpClient

The code below uses JSON.NET to handle the serialization and deserialization of objects to and from JSON.  This is not directly required but is used below for brevity and clarity.  Classes such as `ObjectVersion` can be found in the [sample code on the M-Files Web Service documentation](http://www.m-files.com/mfws/samples.html).

## Using the REST API wrapper

[The REST API wrapper](https://github.com/M-Files/Libraries.MFWSClient) provides a number of helper methods to create and execute searches, removing the requirement to manually construct search condition querystring elements, or encode content.

## Example search conditions

### Quick search

#### Using the native endpoint

To execute a quicksearch, include a querystring parameter named `q` and set its value to the term to search by.  This is equivalent to using the search bar at the top of the M-Files Desktop or Web interfaces.

```csharp
// Build the url to request (note to encode the query term).
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?q=" + System.Net.WebUtility.UrlEncode("mfws"));

// Build the request.
var httpClient = new HttpClient();

// Start the request.
var responseBody = await httpClient.GetStringAsync(url);

// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);
Console.WriteLine($"There were {results.Items.Count} results returned.");
```

#### Using the REST API wrapper

```csharp
// Connect to the online knowledgebase.
// Note that this doesn't require authentication.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Execute a quick search for the query term.
var results = client.ObjectSearchOperations.SearchForObjectsByString("mfws");
Console.WriteLine($"There were {results.Length} results returned.");
```

### Restricting by object type

#### Using the native endpoint

To restrict by object type, a querystring parameter named `o` must be included, and the value must be set to the [ID of the object type](http://developer.m-files.com/APIs/REST-API/Concepts/#obtaining-metadata-structure-element-ids).

This example restricts the search to only return documents (built-in object type ID of 0).

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?o=0");

// Build the request.
var httpClient = new HttpClient();

// Start the request.
var responseBody = httpClient.GetStringAsync(url);

// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);
Console.WriteLine($"There were {results.Items.Count} results returned.");
```

#### Using the REST API wrapper

Restricting objects by object type ID can be done by providing an instance of `ObjectTypeSearchCondition`.

This example restricts the search to only return documents (built-in object type ID of 0).

```csharp
// Connect to the online knowledgebase.
// Note that this doesn't require authentication.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Execute a quick search for the query term and object type search condition.
var results = client.ObjectSearchOperations.SearchForObjectsByConditions(
	new ObjectTypeSearchCondition(0) );
Console.WriteLine($"There were {results.Length} results returned.");
```

### Including deleted items

#### Using the native endpoint

To include deleted items, a querystring parameter named `d` must be included, with a value of `include`:

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?d=include");

// Build the request.
var httpClient = new HttpClient();

// Start the request.
var responseBody = await httpClient.GetStringAsync(url);

// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);
Console.WriteLine($"There were {results.Items.Count} results returned.");
```

#### Using the REST API wrapper

By default, all deleted items are *excluded* from the returned objects when using the REST API.  Including items can be done by providing an instance of `IncludeDeletedObjectsSearchCondition`:

```csharp
// Connect to the online knowledgebase.
// Note that this doesn't require authentication.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Execute a quick search for the query term and object type search condition.
var results = client.ObjectSearchOperations.SearchForObjectsByConditions(
	new IncludeDeletedObjectsSearchCondition() );
Console.WriteLine($"There were {results.Length} results returned.");
```

### Restricting by a property value

*Note that in all the examples below, we will only build the URL to request.  The rest of the code is assumed to be similar to the examples above.**

#### Using the native endpoint

To restrict search results by the value of a property, a querystring parameter must be constructed, the name of which depends on the property ID to query, and the value must be [correctly formatted](http://www.m-files.com/mfws/syntax.html).

##### Restricting by text properties

For example, to restrict the search results by the value of a property with ID `1002`, the querystring parameter must be named `p1002`.  Assuming this property is a text field which we wish to match `ESTT` exactly, the value of the querystring parameter would be `ESTT`.  The entire querystring paramter would be `p1002=ESTT`.

Note that the querystring parameter may need to be [URL encoded](https://en.wikipedia.org/wiki/Percent-encoding).

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1002=ESTT");
```

If we wished to restrict by a text property with ID where the field contains `ESTT`, we would alter the operator:

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1002*=ESTT");
```

If we wished to restrict by a text property with ID where the field matches a [wildcard search for `ESTT*`](http://www.m-files.com/user-guide/latest/eng/#Quick_search.html), we would alter the operator:

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1002**=ESTT");
```

##### Restricting by a boolean property

To restrict the search results by the value of a property with ID `1050`, the querystring parameter must be named `p1050`.  Assuming this property is a boolean which we want to be true, the entire querystring paramter would be `p1050=true`.

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1050=true");
```

Alternatively, we could search for only objects where the property is false:

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1050=false");
```

##### Restricting by a numeric property (integer or real)

To restrict the search results by the value of a property with ID `1100`, the querystring parameter must be named `p1100`.  Assuming this property is a boolean which we want to equal 123, the entire querystring paramter would be `p1100=123`.

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1100=123");
```

Alternatively, we could make use of a [different operator](#operators) to instead search for objects where the value is greater than 1000:

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1100>>=1000");
```

##### Restricting by a date/time or timestamp property

To restrict the search results by the value of a property with ID `1200`, the querystring parameter must be named `p1200`.  The property value must be formatted in ISO-format.  If we wished to match objects where the date property equals 1st June 2017, the querystring parameter would be `p1200=2017-06-01` :

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1200=2017-06-01");
```

Alternatively, we could make use of a [different operator](#operators) to instead search for objects where the date is newer than 1st June 2017:

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1200>>=2017-06-01");
```

##### Restricting by a lookup (single-select) property

To restrict the search results by the value of a property with ID `1500`, the querystring parameter must be named `p1500`.  The property value must be the ID of the lookup item.  For example, to restrict the results to only objects which referenced an object or value list item with ID 12345, the querystring parameter would be `p1500=12345`.

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1500=12345");
```

##### Restricting by a multi-select lookup property

To restrict the search results by the value of a property with ID `1500`, the querystring parameter must be named `p1500`.  The property value must be the ID of the lookup item.  For example, to restrict the results to only objects which referenced an object or value list item with ID 12345, the querystring parameter would be `p1500=12345`.  If there is only one value then this is the same as a single-select-lookup.

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1500=12345");
```

To restrict objects to only objects which reference objects or value list items with *either* IDs 1, 2, 3, or 4, the values must be comma-separated.  The full querystring parameter would be `p1500=1,2,3,4`:

```csharp
// Build the url to request.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?p1500=1,2,3,4");
```

#### Using the REST API wrapper

*The search conditions created below would be passed into MFWSVaultSearchOperations.SearchForObjectsByConditions.*

##### Restricting by text properties

For example, to restrict the search results by the value of a property with ID `1002`:

*Note that the library will handle ensuring items are correctly encoded.*

```csharp
// Create our search condition.
var condition = new TextPropertyValueSearchCondition(1002, "ESTT");
```

If we wished to restrict by a text property with ID where the field contains `ESTT`, we would alter the operator:

```csharp
// Create our search condition.
var condition = new TextPropertyValueSearchCondition(1002, "ESTT", SearchConditionOperators.Contains);
```

If we wished to restrict by a text property with ID where the field matches a [wildcard search for `ESTT*`](http://www.m-files.com/user-guide/latest/eng/#Quick_search.html), we would alter the operator:

```csharp
// Create our search condition.
var condition = new TextPropertyValueSearchCondition(1002, "ESTT", SearchConditionOperators.MatchesWildcard);
```

##### Restricting by a boolean property

To restrict the search results by the value of a boolean property with ID `1050`:

```csharp
// Create our search condition.
var condition = new BooleanPropertyValueSearchCondition(1050, true);
```

Alternatively, we could search for only objects where the property is false:

```csharp
// Create our search condition.
var condition = new BooleanPropertyValueSearchCondition(1050, false);
```

##### Restricting by a numeric property (integer or real)

To restrict the search results by the value of a numeric property with ID `1100`:

```csharp
// Create our search condition.
var condition = new NumericPropertyValueSearchCondition(1100, 123);
```

Alternatively, we could make use of a [different operator](#operators) to instead search for objects where the value is greater than 1000:

```csharp
// Create our search condition.
var condition = new NumericPropertyValueSearchCondition(1100, 1000, SearchConditionOperators.GreaterThan);
```

##### Restricting by a date/time or timestamp property

To restrict the search results by the value of a date property with ID `1200`:

```csharp
// Create our search condition.
var condition = new DatePropertyValueSearchCondition(1200, new DateTime(2017, 6, 1));
```

Alternatively, we could make use of a [different operator](#operators) to instead search for objects where the date is newer than 1st June 2017:

```csharp
// Create our search condition.
var condition = new DatePropertyValueSearchCondition(1200, new DateTime(2017, 6, 1), SearchConditionOperators.GreaterThan);
```

##### Restricting by a lookup (single-select) property

To restrict the search results by the value of a single-select lookup property with ID `1500` that reference an object or value list item with ID 12345:

```csharp
// Create our search condition.
var condition = new LookupPropertyValueSearchCondition(1500, 12345);
```

##### Restricting by a multi-select lookup property

To restrict the search results by the value of a single-select lookup property with ID `1500` that reference an object or value list item with ID 12345:

*If there is only one value then this is the same as a single-select-lookup.*

```csharp
// Create our search condition.
var condition = new MultiSelectLookupPropertyValueSearchCondition(1500, 12345);
```

To restrict objects to only objects which reference objects or value list items with *either* IDs 1, 2, 3, or 4:

```csharp
// Create our search condition.
var condition = new MultiSelectLookupPropertyValueSearchCondition(1500, new [] { 1, 2, 3, 4 });
```

## Combining search conditions

### Using the native endpoint

Executing a search using multiple conditions involves building up a more complex querystring to provide to the [objects endpoint](http://www.m-files.com/mfws/resources/objects.html).  Each condition is built and encoded as specified in the [search encoding table](http://www.m-files.com/mfws/syntax.html#sect:search-encoding) and the search conditions are appended to the querystring, separated by ampersands.

For example, a quicksearch for the string "mfws" may be encoded as `q=mfws`, and a constraint to restrict the results only to documents as `o=0`.  Combining these two conditions together would result in a request to `/objects?q=mfws&o=0`.  Additional constraints can be added until the desired expression is complete.

```csharp
// Build the url to request (note to encode the query term).
// Also note that "o=0" (object type Id equals zero) has been added to the querystring.
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?q=" + System.Net.WebUtility.UrlEncode("mfws") + "&o=0");

// Build the request.
var httpClient = new HttpClient();

// Start the request.
var responseBody = await httpClient.GetStringAsync(url);

// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);
Console.WriteLine($"There were {results.Items.Count} results returned.");
```

### Using the REST API wrapper

The REST API wrapper contains a number of classes which implement `MFaaP.MFWSClient.ISearchCondition`.  These can be passed to `MFWSVaultObjectSearchOperations.SearchForObjectsByConditions`, and it will construct and execute the appropriate web request.

More information on these classes is given above in the [Example search conditions](#example-search-conditions) chapter.

```csharp
// Connect to the online knowledgebase.
// Note that this doesn't require authentication.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Execute a quick search for the query term and object type search condition.
var results = await client.ObjectSearchOperations.SearchForObjectsByConditions(
	new QuickSearchCondition("mfws"),
	new ObjectTypeSearchCondition(0) );
Console.WriteLine($"There were {results.Length} results returned.");
```

## Operators

*These operators are used when searching by a property value, and are detailed [in the online documentation](http://www.m-files.com/mfws/syntax.html).*

### Available operators

| Operator | Description | Example |
| - | - | - |
| = | The property value must equal the value provided. | p1050=ESTT |
| <<= | The property value must be less than the value provided. | p1050<<=1000 |
| <= | The property value must be less than or equal to the value provided. | p1050<=1000 |
| >>= | The property value must be greater than the value provided. | p1050>>=1000 |
| >= | The property value must be greater than or equal to the value provided. | p1050>=1000 |
| **= | The property value must match the wildcard search value provided. | p1050**=ESTT* |
| *= | The property value must contain the value provided. | p1050*=ESTT |
| ^= | The property value must start with the value provided. | p1050^=ESTT |

### Op-mod

The op-mod negates the operator provided, and is used in front of it.  For example: 

| Operator | Description | Example |
| - | - | - |
| != | The property value must *not* equal the value provided. | p1050!=ESTT |
