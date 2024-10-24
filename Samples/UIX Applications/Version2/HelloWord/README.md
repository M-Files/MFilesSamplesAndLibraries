# Hello, World

## Overview
This sample creates a basic User Interface Extensibility Framework application consisting of one ShellUI module which shows a dialog box to the user when the shell frame is available.
The shell frame is a useful object as it allows us to interact with the shell listings and commands (such as buttons) within the user interface.

## Creating a local development folder
Firstly, let’s create a local development folder for the application.

![ alt text ]( ./screenshots/HelloWorld_2.png "Local development folder" )

## Creating the application definition file
Into this folder we will create an application definition file. This file must be named `appdef.xml`. The application will use version 5 of the client schema (as we are only targeting newer M-Files versions). The application will declare a single Shell UI module (with its code in `main.js`), and no dashboards.

`appdef.xml`
<Tabs>
<TabItem value="xml" label="XML">

```xml
<?xml version="1.0"?>
<application xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="http://www.m-files.com/schemas/appdef-client-v5.xsd">
    <guid>5EA29AF2-1EC9-4AB7-A0D1-FE1D586310D4</guid>
    <name>Hello, World</name>
    <version>0.1</version>
    <description>A basic application showing how to react to the shell frame being available.</description>
    <publisher>M-Files Corporation</publisher>
    <enabled-by-default>true</enabled-by-default>
    <modules>
        <module environment="shellui">
        <file>main.js</file>
        </module>
    </modules>
</application>
```

</TabItem>
</Tabs>

> Ensure that your application has a unique GUID by using a GUID generator.

## Creating the module
Next we will create a module file to contain our actual application logic. The logic will be simple:
- We will declare a default entry point for the ShellUI module.
- We will react to the `NewShellFrame` event and obtain a reference to the shell frame.
- We will react to the shell frame’s `Started` event (as using the shell frame before this point will result in an exception).
- We will display a message to the user that the shell frame is ready for use.

`main.js`
<Tabs>
<TabItem value="js" label="JavaScript">

```js
// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

function OnNewShellUI( shellUI )
{
	/// <summary>Executed by the UIX when a ShellUI module is started.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The shell UI object which was created.</param>

	// This is the start point of a ShellUI module.
	
	// Register to be notified when a new normal shell frame (MFiles.Event.NewShellFrame) is created.
	// We use MFiles.Event.NewShellFrame rather than MFiles.Event.NewShellFrame as this won't fire for history (etc.) dialogs.
	shellUI.Events.Register(
		MFiles.Event.NewShellFrame,
		handleNewShellFrame );
}

function handleNewShellFrame( shellFrame )
{
	/// <summary>Handles the OnNewShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param>

	// The shell frame was created but it cannot be used yet.
	// The following line would throw an exception ("The object cannot be accessed, because it is not ready."):
	// shellFrame.ShowMessage("A shell frame was created");

	// Register to be notified when the shell frame is started.
	// This time pass a reference to the function to call when the event is fired.
	shellFrame.Events.Register(
		MFiles.Event.Started,
		getShellFrameStartedHandler( shellFrame) );
}

function getShellFrameStartedHandler( shellFrame )
{
	/// <summary>Returns a function which handles the OnStarted event for an IShellFrame.</summary>

	return async () => {

		// The shell frame is now started and can be used.
		// Note: we need to use the global-scope variable.
		await shellFrame.ShowMessage( "A shell frame is available for use." );
	};
}
```

</TabItem>
</Tabs>

## Deploying the application
To deploy the application:
1. Zip the contents of the local development folder (e.g. `HelloWorld.zip`).
2. Open the M-Files Admin tool and connect to your M-Files server.
3. Right-click on the vault to install the application to.
4. Select `Applications`.
5. Click `Install...` and select the zip file.
6. Click `Open` and the application should be listed.

![ alt text ]( ./screenshots/HelloWorld_1.png "Applications" )

<mark> The zipped file can be renamed to have a .mfappx extension if you wish to differentiate it from other zip files.</mark>

 ## Testing the application
 Open M-Files web and navigate to the vault. The following message should be shown on-screen:

 ![ alt text ]( ./screenshots/HelloWorld_3.png "Shellframe Message" )