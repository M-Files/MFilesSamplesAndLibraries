# Retrieving an object's checkout status

This sample shows how to retrieve an object's [checkout status](http://www.m-files.com/mfws/resources/objects/type/objectid/version/checkedout.html) using both the REST API wrapper for .NET, and also using the native HttpClient object.

## Using the native HttpClient

The code below uses JSON.NET to handle the serialization and deserialization of objects to and from JSON.  This is not directly required but is used below for brevity and clarity.  Classes such as `PrimitiveType` and enumerations such as `MFCheckOutStatus` can be found in the [sample code on the M-Files Web Service documentation](http://www.m-files.com/mfws/samples.html).

```csharp
// Create a HttpClient to use for our requests.
var httpClient = new System.Net.Http.HttpClient();

// Create an ObjID for the object to reference.
var objId = new ObjID()
{
    ID = 5,
    Type = 0
};

// Retrieve the checkout status.
url = 
	new Uri($"http://kb.cloudvault.m-files.com/REST/objects/{objId.Type}/{objId.ID}/latest/checkedout");
responseBody = await httpClient.GetStringAsync(url);

// Attempt to parse it.
var checkoutStatus = JsonConvert.DeserializeObject<PrimitiveType<MFCheckOutStatus>>(responseBody);
```


## Using the REST API wrapper

[The REST API wrapper](https://github.com/M-Files/Libraries.MFWSClient) exposes a method (GetCheckoutStatus) on the MFWSVaultObjectOperations class.  This can be executed as below:

```csharp
// Create an MFWSClient for the knowledgebase.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Create an ObjID for the object to reference.
var objId = new ObjID()
{
    ID = 5,
    Type = 0
};

// Retrieve the checkout status.
var checkoutStatus = client.ObjectOperations.GetCheckoutStatus(objId);
```


