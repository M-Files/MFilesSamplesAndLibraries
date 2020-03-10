# Downloading files

This sample shows how to download an object's [files](http://www.m-files.com/mfws/resources/objects/type/objectid/version/files.html) using both the REST API wrapper for .NET, and also using the native HttpClient object.

## Using the native HttpClient

The code below uses JSON.NET to handle the serialization and deserialization of objects to and from JSON.  This is not directly required but is used below for brevity and clarity.  Classes such as `ObjectVersion` can be found in the [sample code on the M-Files Web Service documentation](http://www.m-files.com/mfws/samples.html).

```csharp
// Build the url to request (note to encode the query term).
var url =
	new Uri("http://kb.cloudvault.m-files.com/REST/objects?q=" + System.Net.WebUtility.UrlEncode("mfws"));

// Build the request.
var httpClient = new System.Net.Http.HttpClient();

// Start the request.
string responseBody = await httpClient.GetStringAsync(url);

// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);

// Iterate over the results and output them.
Console.WriteLine($"There were {results.Items.Count} results returned.");
foreach (var objectVersion in results.Items)
{
	Console.WriteLine($"\t{objectVersion.Title}");
	Console.WriteLine($"\t\tType: {objectVersion.ObjVer.Type}, ID: {objectVersion.ObjVer.ID}");

	// Create a folder for the files to go in.
	var folderPath =
		new System.IO.DirectoryInfo(System.IO.Path.Combine(@"C:\temp\downloads", objectVersion.ObjVer.ID.ToString()));
	if (false == folderPath.Exists)
		folderPath.Create();

	// Download the files.
	foreach (var file in objectVersion.Files)
	{
		// Generate a unique file name.
		var fileName = System.IO.Path.Combine(folderPath.FullName, file.ID + "." + file.Extension);

		// Download the file data.
		var data = await
			httpClient.GetByteArrayAsync(
				$"http://kb.cloudvault.m-files.com/REST/objects/{objectVersion.ObjVer.Type}/{objectVersion.ObjVer.ID}/{objectVersion.ObjVer.Version}/files/{file.ID}/content");

		// Open a stream to the file.
		using (var stream = System.IO.File.OpenWrite(fileName))
		{
			// Save the content to disk.
			stream.Write(data, 0, data.Length);
		}

		// Log.
		Console.WriteLine($"\t\t\tFile: {file.Name} output to {fileName}");

	}
}
```

## Using the REST API wrapper

[The REST API wrapper](https://github.com/M-Files/Libraries.MFWSClient) exposes a method (GetCheckoutStatus) on the MFWSVaultObjectOperations class.  This can be executed as below:

```csharp
// Connect to the online knowledgebase.
// Note that this doesn't require authentication.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Execute a quick search for the query term.
var results = await client.ObjectSearchOperations.SearchForObjectsByString("mfws");

// Iterate over the results and output them.
Console.WriteLine($"There were {results.Length} results returned.");
foreach (var objectVersion in results)
{
	Console.WriteLine($"\t{objectVersion.Title}");
	Console.WriteLine($"\t\tType: {objectVersion.ObjVer.Type}, ID: {objectVersion.ObjVer.ID}");

	// Create a folder for the files to go in.
	var folderPath =
		new System.IO.DirectoryInfo(System.IO.Path.Combine(@"C:\temp\downloads", objectVersion.ObjVer.ID.ToString()));
	if (false == folderPath.Exists)
		folderPath.Create();

	// Download the files.
	foreach (var file in objectVersion.Files)
	{
		// Generate a unique file name.
		var fileName = System.IO.Path.Combine(folderPath.FullName, file.ID + "." + file.Extension);

		// Download the file data.
		await client.ObjectFileOperations.DownloadFile(objectVersion.ObjVer.Type,
			objectVersion.ObjVer.ID,
			objectVersion.Files[0].ID,
			fileName,
			objectVersion.ObjVer.Version);

		Console.WriteLine($"\t\t\tFile: {file.Name} output to {fileName}");
	}

}
```


