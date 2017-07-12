# Generating a pre-approval registry key for a User Interface Extensibility Framework application

Whenever a User Interface Extensibility (UIX) Framework application is installed onto the server, upon next connection each user will be prompted to accept or decline the application.

![Accepting a UI Extensibility Framework application](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/allow-deny.png)

## Suppressing the dialog using registry keys

This dialog can be suppressed by creating specific registry keys on each client machine. The User Interface Extensibility Framework documentation details this briefly in [section 3.1.5](https://www.m-files.com/UI_Extensibility_Framework/DevGuide.htm#_Toc442344256) of the developer guide.  [More detailed information can be found on the Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).

## Using an programmatic approach

As manually creating registry keys for each application can be awkward and painstaking, an automated/programmatic approach can simplify the process.  This sample application shows how the API can be used to generate these registry keys.

This application connects to an M-Files server, lists the UIX applications which are installed, and allows the user to generate a `.reg` file containing the required registry keys for pre-approving the application.

The primary section of code can be found within the `GenerateRegistryFile` method within `MainWindowViewModel`:

```csharp
/// <summary>
/// Generates the registry file.
/// </summary>
/// <param name="application">The custom application that should be within the registry file.</param>
public void GenerateRegistryFile(CustomApplication application)
{
	// Sanity.
	if (null == application)
		throw new ArgumentNullException(nameof(application));

	// Let them choose a file.
	if (this.saveFileDialog.ShowDialog(this.Window) != true)
		return;

	// Does it exist?
	var fileInfo = new System.IO.FileInfo(this.saveFileDialog.FileName);
	if(fileInfo.Exists)
		fileInfo.Delete();

	// Create it.
	using (var textWriter = fileInfo.CreateText())
	{
		textWriter.WriteLine("Windows Registry Editor Version 5.00");
		textWriter.WriteLine();
		textWriter.WriteLine($@"[HKEY_LOCAL_MACHINE\SOFTWARE\Motive\M-Files\{this.SelectedVault.GetServerVersionOfVault().Display}\Client\MFClient\ApplicationAccess\{this.SelectedVault.GetGUID()}]");
		textWriter.WriteLine($"\"{application.ID}\"=\"{application.ChecksumHash}\"");
	}
}
```

*This method is passed a [CustomApplication](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~CustomApplication.html) which is loaded from [VaultCustomApplicationManagementOperations.GetCustomApplications](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultCustomApplicationManagementOperations~GetCustomApplications.html).*