# Built-in Command Demonstration

## Overview
This sample creates a basic User Interface Extensibility Framework application consisting of one ShellUI module which reacts when built-in commands are clicked. Also it shows how to execute a built in command.

This sample does not show how to create a local development folder or to deploy the code to the M-Files server. It is assumed that a local development folder already exists, and that is the location in which the development is occurring.

## Creating the application structure

### Creating the application definition file

Into this folder we will create an application definition file. This file must be named `appdef.xml`. The application will use version 5 of the client schema (as we are only targeting newer M-Files versions). The application will declare a single Shell UI module (with its code in `main.js`), and no dashboards.

`appdef.xml`
<Tabs>
<TabItem value="xml" label="XML">

```xml
<?xml version="1.0"?>
<application xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="http://www.m-files.com/schemas/appdef-client-v5.xsd">
    <guid>D14E6DA7-E7A6-498E-B709-C682C75DC887</guid>
    <name>Built-in Command Demonstration</name>
    <version>0.1</version>
    <description>Simple M-Files built-in command demonstration.</description>
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

### Creating the module

Next we will create a module file to contain our actual application logic. At this point we will just register to be notified of main lifecycle events:

- We will declare a default entry point for the ShellUI module.
- We will react to the `NewShellFrame` event and obtain a reference to the shell frame.
- We will react to the shell frame’s `Started` event (as using the shell frame before this point will result in an exception).

`main.js`
<Tabs>
<TabItem value="js" label="JavaScript">

```js
// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

function OnNewShellUI( shellUI ) {

	/// <summary>Executed by the UIX when a ShellUI module is started.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The shell UI object which was created.</param>

	// This is the start point of a ShellUI module.

	// Register to be notified when a new shell frame (MFiles.Event.NewShellFrame) is created.
	shellUI.Events.Register(
		MFiles.Event.NewShellFrame,
		handleNewShellFrame );
}

function handleNewShellFrame( shellFrame ) {

	/// <summary>Handles the OnNewNormalShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param>

	// The shell frame was created but it cannot be used yet.
	// The following line would throw an exception ("The object cannot be accessed, because it is not ready."):
	// shellFrame.ShowMessage("A shell frame was created");

	// Register to be notified when the shell frame is started.
	shellFrame.Events.Register(
		MFiles.Event.Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler( shellFrame )  {

	/// <summary>Returns a function which handles the OnStarted event for an IShellFrame.</summary>

	// The shell frame is now started and can be used.

	return async () => {};
}
```

</TabItem>
</Tabs>

## Reacting to built-in commands

Once the ShellFrame is available for use, our code can register to be notified when the BuiltinCommand event is raised. This event occurs whenever standard M-Files client functionality is used, such as clicking buttons to create new objects, or to log out.

To register, we provide a callback function which defines two parameters:

- `commandId` (number) - one of the built-in commands from the BuiltinCommand enumeration.
- `param` (number) - either:

    - If the commandId is `MFiles.BuiltinCommand.NewObject` then the ID of the object type being created (or -100 if not known), or
    - `-2` otherwise.

In this sample we will simply show the values in a message box:

`main.js`
<Tabs>
<TabItem value="js" label="JavaScript">

```js
// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

function OnNewShellUI( shellUI ) {

	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param> 

	// Note: We want to hook into the built-in commands being executed in any shell frame,
	// but we want our command to only be used in NORMAL shell frames (as the task pane etc. is not available in others).

	// Register to be notified when a new shell frame is created.
	// This will be used to hook into the built-in commands whereever they are executed.
	shellUI.Events.Register(
        MFiles.Event.NewShellFrame,
		handleNewShellFrame );	
}

function handleNewShellFrame( shellFrame ) {

	/// <summary>Handles the OnNewShellFrame event.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The new shell frame object.</param> 

	// Register to be notified of the started event.
	shellFrame.Events.Register(
		MFiles.Event.Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler( shellFrame ) {

	/// <summary>Gets a function to handle the Started event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>

	// Return the handler function for Started event.
	return async () => {

		// Register to be notified when a built-in command is executed.
		shellFrame.Commands.Events.Register(
			MFiles.Event.BuiltinCommand,
			async ( commandId, param ) => {

				/// <summary>Executed whenever a built-in command is clicked.</summary>
				/// <param name="commandId" type="BuiltinCommand">
				/// One of the built-in commands from the BuiltinCommand enumeration.
				/// </param> 
				/// <param name="param">
				/// If the <paramref name="commandId"/> is MFiles.BuiltinCommand.NewObject then contains the object type id of the object to create (or -100 if not specified).
				/// Otherwise, returns -2.
				/// </param>

				// Display every built-in command as message box.
				await shellFrame.ShowMessage( "Command ID: " + commandId + ", param: " + param );
			} );
	};
}
```

</TabItem>
</Tabs>

## Executing built-in command

Command can be executed using ExecuteCommand from ICommands interface. It has two parameters
- `commandId` - The command id. Can be a built-in command enumerated value or custom command id.
- `args` - Command argument or an arguments object, if the command requires arguments. Use null or empty value if the command does not require arguments.

In this example, it demonstrates how the builtin command `Download File` executed. It invovles four steps:
- Creating a new ICommand using CreateCustomCommand.
- Adding the command into the context menu using AddCustomCommandToMenu.
- Handling NewShellListing event where enable the custom command state based on the current selected items.
- Execute `Download File` command.

`main.js`
<Tabs>
<TabItem value="js" label="JavaScript">

```js
// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

function OnNewShellUI( shellUI ) {

	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param> 

	// Note: We want to hook into the built-in commands being executed in any shell frame,
	// but we want our command to only be used in NORMAL shell frames (as the task pane etc. is not available in others).

	// Register to be notified when a new shell frame is created.
	// This will be used to hook into the built-in commands whereever they are executed.
	shellUI.Events.Register(
        MFiles.Event.NewShellFrame,
		handleNewShellFrame );	
}

function handleNewShellFrame( shellFrame ) {

	/// <summary>Handles the OnNewShellFrame event.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The new shell frame object.</param> 

	// Register to be notified of the started event.
	shellFrame.Events.Register(
		MFiles.Event.Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler( shellFrame ) {

	/// <summary>Gets a function to handle the Started event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>

	// Return the handler function for Started event.
	return async () => {

		// Create a command (button).  Note that it is not yet visible.
		const commandDownloadId = await shellFrame.Commands.CreateCustomCommand( "Download selected file(s)" );

		// Add the commands to the context menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandDownloadId, MFiles.MenuLocation.MenuLocation_ContextMenu_Bottom, 1 );

		// Hide the command. We will show it when the selected items change.
		await shellFrame.Commands.SetCommandState( commandDownloadId, MFiles.CommandLocation.All, MFiles.CommandState.CommandState_Hidden );

		// Register to listen when new shell listings are created.
		shellFrame.Events.Register(
			MFiles.Event.NewShellListing,
			getNewShellListingHandler( shellFrame, commandDownloadId ) );

		// Is there already a listing?  If so then we need to hook into it as well.
		if( shellFrame.Listing ) {
			getNewShellListingHandler( shellFrame, commandDownloadId )( shellFrame.Listing );
		}

		// Register to be notified when a built-in command is executed.
		shellFrame.Commands.Events.Register(
			MFiles.Event.BuiltinCommand,
			async ( commandId, param ) => {

				/// <summary>Executed whenever a built-in command is clicked.</summary>
				/// <param name="commandId" type="BuiltinCommand">
				/// One of the built-in commands from the BuiltinCommand enumeration.
				/// </param> 
				/// <param name="param">
				/// If the <paramref name="commandId"/> is MFiles.BuiltinCommand.NewObject then contains the object type id of the object to create (or -100 if not specified).
				/// Otherwise, returns -2.
				/// </param>
				/// <returns>A boolean defining whether the action should continue (true) or be cancelled (false).</returns>

				// Display every built-in command as message box.
				await shellFrame.ShowMessage( "Command ID: " + commandId + ", param: " + param );
			} );

		// Register to be notified when a custom command is executed.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( commandId ) => {

				/// <summary>Executed whenever a custom command is clicked.</summary>
				/// <param name="commandId" type="CustomCommand">
				/// One of the built-in commands from the CustomCommand enumeration.
				/// </param> 
				if( commandId === commandDownloadId ) {
					await shellFrame.Commands.ExecuteCommand( MFiles.BuiltinCommand.DownloadFile, null );
				}
			} );
	};
}

function isObjectCanBeDownload( currentSelection ) {

	/// <summary>Gets the boolean flag whether the object can be download or not</summary>
	/// <param name="currentSelection" type="MFiles.ShellItems">The current selection.</param> 

	let canDownload = true;

	// Nothing selected so nothing to download.
	if( currentSelection.Count === 0 ) {
		return false;
	}

	// Skip the download option for folders/view.
	if( currentSelection.Folders.length === 0 ) {

		// Check all the objects in current selection in order to
		// set the commands state properly.
		if( currentSelection.ObjectVersions.length > 0 ) {

			// Object version info.
			const currentObjectVersions = currentSelection.ObjectVersions;

			for( const objIdx in currentObjectVersions ) {
				if( Object.prototype.hasOwnProperty.call( currentObjectVersions,
					objIdx ) ) {

					// True, if the selected object has files.
					canDownload = currentSelection.ObjectVersions[ objIdx ].version_info.files?.length > 0;

					// No need to show 'Download this file' option for objects which doesnt have files (e.g., customer, projects).
					if( !canDownload ) {
						break;
					}
				}
			}
		}

		// Single file.
		if( currentSelection.ObjectFiles.length > 0 ) {
			canDownload = true;
		}

	} else {
		canDownload = false;
	}

	return canDownload;
}

function getNewShellListingHandler( shellFrame, commandDownloadId ) {

	/// <summary>Gets a function to handle the NewShellListing event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnNewShellListing">The event handler.</returns>

	// Return the handler function for NewShellListing event.
	return async ( shellListing ) => {

		// Listen for selection change events on the listing.
		shellListing.Events.Register(
			MFiles.Event.SelectionChanged,
			async ( selectedItems ) => {

				// Has the user got any object versions selected?
				if( shellListing.IsActive === false ||
					!isObjectCanBeDownload( selectedItems ) ) {

					// Hide the menu item.
					await shellFrame.Commands. SetCommandState( commandDownloadId, MFiles.CommandLocation.All, MFiles.CommandState.CommandState_Hidden );
					return false;
				}

				// Show the menu item.
				await shellFrame.Commands.SetCommandState( commandDownloadId, MFiles.CommandLocation.All, MFiles.CommandState.CommandState_Active );

				// We succeeded; return true.
				return true;
			} );
	};
}
```

</TabItem>
</Tabs>

 ## Testing the application

 On creating new assignment object, the following message should be shown on-screen:

![ alt text ]( ./screenshots/BuiltInCommands_1.png "Builtin Command" )

On context menu over the doument object, `Download selected file(s)` command can be seen. On clicking it, the selected file will be downloaded using the built-in command `Download File`.

![ alt text ]( ./screenshots/BuiltInCommands_2.png "Download selected file(s)" )