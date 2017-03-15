# M-Files Libraries

*Please note that these libraries are provided "as-is" and with no warranty, explicit or otherwise.  You should ensure that the functionality of these libraries meet your requirements, and thoroughly test them, prior to using in any production scenarios.*

The following helper libraries are provided as work-in-progress, and may not be fully complete.

## MFaaP.MFilesAPI (M-Files API Helper Library)



## MFaaP.MFWSClient (C# M-Files Web Service Wrapper)

This library aims to provide an easy-to-use C# wrapper for the [M-Files Web Service](http://www.m-files.com/MFWS/), which is part of the M-Files Web Access.  The user guide contains more information on [setting up M-Files Web Access](http://www.m-files.com/user-guide/latest/eng/#Configure_M-Files_Web_Access.html).

It currently provides the following functionality:

* Authentication, both using credentials and using Windows Single Sign On (`MFWSClient.Authentication.cs`)
* Object creation (`MFWSClient.CreatingObjects.cs`)
* File upload (`MFWSClient.CreatingObjects.cs`)
* Vault extension method execution (`MFWSClient.ExtensionMethods.cs`)
* Basic searching (`MFWSClient.Searching.cs`)

### Basic usage

This functionality is exposed by the `MFWSClient` object, which takes the URL of the M-Files Web Service in the constructor:

```csharp
// Instantiate a new MFWS client.
var client = new MFWSClient("http://m-files.mycompany.com");
```

### Authentication

Currently two methods of authentication are supported: authentication using credentials, and Windows Single Sign On.

Note that the M-Files Web Service will provide authentication tokens even if the credentials are incorrect.  This is by design.

### Authenticating using credentials

```csharp
// Instantiate a new MFWS client.
var client = new MFWSClient("http://m-files.mycompany.com");

// Authentiate to a vault with GUID {C840BE1A-5B47-4AC0-8EF7-835C166C8E24} (clear credentials).
client.AuthenticateUsingCredentials(Guid.Parse("{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}"), "MyUsername", "MyPassword")
```

### Authenticating using Windows Single Sign On

If using Windows Single Sign On, the application will use the current Windows identity that it is running under.  Note that [using Windows Single Sign On requires additional configuration](https://partners.cloudvault.m-files.com/Default.aspx?#CE7643CB-C9BB-4536-8187-707DB78EAF2A/object/75F59ED5-CC7F-4A0A-90D5-0F582D26E884/latest).

```csharp
// Instantiate a new MFWS client.
var client = new MFWSClient("http://m-files.mycompany.com");

// Authentiate to a vault with GUID {C840BE1A-5B47-4AC0-8EF7-835C166C8E24} (clear credentials).
client.AuthenticateUsingSingleSignOn(Guid.Parse("{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}"))
```

### Basic searching

Simple searching can be done using the `QuickSearch` method.  Note that the search will only return items which you have access to, so ensure that you are authenticated (if required) prior to executing the method.

```csharp
// Connect to the online knowledgebase.
// Note that this doesn't require authentication.
var client = new MFWSClient("http://kb.cloudvault.m-files.com");

// Execute a simple search for the word "mfws".
var results = client.QuickSearch("mfws");

// Iterate over the results and output them.
System.Console.WriteLine($"There were {results.Length} results returned.");
foreach (var objectVersion in results)
{
	System.Console.WriteLine($"\t{objectVersion.Title}");
	System.Console.WriteLine($"\t\tType: {objectVersion.ObjVer.Type}, ID: {objectVersion.ObjVer.ID}");
}
```