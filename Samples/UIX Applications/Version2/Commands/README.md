# Commands

## Overview
This sample creates a basic User Interface Extensibility Framework application consisting of one ShellUI module which adds buttons to the main menu and context menu that are shown/hidden depending upon the user’s actions.

## Creating the application structure

### Creating the application definition file

Into this folder we will create an application definition file. This file must be named `appdef.xml`. The application will use version 5 of the client schema (as we are only targeting newer M-Files versions). The application will declare a single Shell UI module (with its code in `main.js`), and no dashboards.

`appdef.xml`
<Tabs>
<TabItem value="xml" label="XML">

```xml
<?xml version="1.0"?>
<application xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="http://www.m-files.com/schemas/appdef-client-v5.xsd">
    <guid>A50FF521-9167-4366-9CDE-6CB5C3BDDAFF</guid>
    <name>Commands</name>
    <version>0.1</version>
    <description>A basic application showing how to work with commands.</description>
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

## Creating a button in the main menu

Adding a button into the main menu involves two steps:

1. Creating a new `ICommand` using CreateCustomCommand.
2. Adding the command into the task area using AddCustomCommandToMenu.

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

	return async () => {

		// Create a command (button).  Note that it is not yet visible.
		const commandOneId = await shellFrame.Commands.CreateCustomCommand( "My First Command" );

		// Add the first command to the main menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );
	};
}
```

</TabItem>
</Tabs>

Logging into the M-Files vault should now show a button in the main menu (three dot menu near user icon) with the text `My First Command`:

![ alt text ]( ./screenshots/Commands_1.png "First command" )

### Reacting when the command is clicked

Reacting to a command being clicked involves three steps:

- Register to be notified of the CustomCommand event.
- Ensure that the command that was clicked was the one we want to handle.
- Execute the required code.

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

	return async () => {

		// Create a command (button).  Note that it is not yet visible.
		const commandOneId = await shellFrame.Commands.CreateCustomCommand( "My First Command" );

		// Add the first command to the main menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );

        // Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( commandId ) => {

				// Branch depending on the Id of the command that was clicked.
				switch (commandId) {
					case commandOneId:

						// Our first command was clicked.
						await shellFrame.ShowMessage( "My first command clicked." );
						break;
				}
			} );

	};
}
```

</TabItem>
</Tabs>

![ alt text ]( ./screenshots/Commands_2.png "First command clicked" )

## Adding a command to both the main menu and the context menu

To add a command created with CreateCustomCommand to the context menu, call AddCustomCommandToMenu. In the example below, it is the same command object added to both main menu and context menu, so our code to react when the button is clicked will be fired for the context menu item and the main menu item.

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

	return async () => {

		// Create a command (button).  Note that it is not yet visible.
		const commandOneId = await shellFrame.Commands.CreateCustomCommand( "My First Command" );

        // Add the first command to the context menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_ContextMenu_Bottom, 1 );

		// Add the first command to the main menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );

        // Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( commandId ) => {

				// Branch depending on the Id of the command that was clicked.
				switch (commandId) {
					case commandOneId:

						// Our first command was clicked.
						await shellFrame.ShowMessage( "My first command clicked." );
						break;
				}
			} );

	};
}
```

</TabItem>
</Tabs>

![ alt text ]( ./screenshots/Commands_3.png "Context menu" )

## Adding child command to the main menu
Adding a child button into the main menu involves two steps:

1. Creating a new `ICommand` using CreateCustomCommand.
2. Adding the command into main menu using CreateSubMenuItem by giving the parent command id.

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

	return async () => {

		// Create a command (button).  Note that it is not yet visible.
		const commandOneId = await shellFrame.Commands.CreateCustomCommand( "My First Command" );

		// Create a command (button).  Note that it is not yet visible.
		const commandTwoId = await shellFrame.Commands.CreateCustomCommand( "My Second Command" );

		// Add the first command to the context menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_ContextMenu_Bottom, 1 );

		// Add the first and second commands to the main menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );
		const parentMenuItemId = await shellFrame.Commands.AddCustomCommandToMenu( commandTwoId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );

		// Create child commands (buttons).
		const commandChildOneId = await shellFrame.Commands.CreateCustomCommand( "My First Child Command" );
		const commandChildTwoId = await shellFrame.Commands.CreateCustomCommand( "My Second Child Command" );

		// Add created child comamnds to the parent.
		await shellFrame.Commands.CreateSubMenuItem( parentMenuItemId, commandChildOneId, 1 );
		await shellFrame.Commands.CreateSubMenuItem( parentMenuItemId, commandChildTwoId, 1 );

		// Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( commandId ) => {

				// Branch depending on the Id of the command that was clicked.
				switch (commandId) {
					case commandOneId:

						// Our first command was clicked.
						await shellFrame.ShowMessage( "My first command clicked." );
						break;

					case commandChildOneId:

						// Our child command was clicked.
						await shellFrame.ShowMessage( "My child command clicked." );
						break;
				}
			} );
	};
}
```

</TabItem>
</Tabs>

![ alt text ]( ./screenshots/Commands_4.png "Child Command" )

## Showing and hiding buttons

The visibility of commands can be controlled by calling `SetCommandState`. To show this, we will toggle the first command visibility on clicking the child command.

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

	return async () => {

		// Create a command (button).  Note that it is not yet visible.
		const commandOneId = await shellFrame.Commands.CreateCustomCommand( "My First Command" );

		// Create a command (button).  Note that it is not yet visible.
		const commandTwoId = await shellFrame.Commands.CreateCustomCommand( "My Second Command" );

		// Add the first command to the context menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_ContextMenu_Bottom, 1 );

		// Add the first and second commands to the main menu.
		await shellFrame.Commands.AddCustomCommandToMenu( commandOneId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );
		const parentMenuItemId = await shellFrame.Commands.AddCustomCommandToMenu( commandTwoId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );

		// Create child commands (buttons).
		const commandChildOneId = await shellFrame.Commands.CreateCustomCommand( "My First Child Command" );
		const commandChildTwoId = await shellFrame.Commands.CreateCustomCommand( "My Second Child Command" );

		// Add created child comamnds to the parent.
		await shellFrame.Commands.CreateSubMenuItem( parentMenuItemId, commandChildOneId, 1 );
		const childMenuItem = await shellFrame.Commands.CreateSubMenuItem( parentMenuItemId, commandChildTwoId, 1 );

		// Create nested child command and add to the parent.
		const toggleCommand = await shellFrame.Commands.CreateCustomCommand( "Toggle First Command Visibility" );
		await shellFrame.Commands.CreateSubMenuItem( childMenuItem, toggleCommand, 1 );

		// Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( commandId ) => {

				// Branch depending on the Id of the command that was clicked.
				switch (commandId) {
					case commandOneId:

						// Our first command was clicked.
						await shellFrame.ShowMessage( "My first command clicked." );
						break;

					case commandChildOneId:

						// Our child command was clicked.
						await shellFrame.ShowMessage( "My child command clicked." );
						break;

					case toggleCommand:

						// Get the first command state and toggle it.
						let firstCommandState = await shellFrame.Commands.GetCommandState( commandOneId, MFiles.CommandLocation.MainMenu, false, false );
						firstCommandState = firstCommandState === MFiles.CommandState.CommandState_Hidden
							? MFiles.CommandState.CommandState_Active
							: MFiles.CommandState.CommandState_Hidden;

						// Toggle the command state and show message.
						await shellFrame.Commands.SetCommandState( commandOneId, MFiles.CommandLocation.All, firstCommandState );

                        // Show the message.
						const message = firstCommandState === MFiles.CommandState.CommandState_Active
							? "First command visibility enabled"
							: "First command visibility disabled"
						await shellFrame.ShowMessage( message );
						break;
				}
			} );
	};
}
```

</TabItem>
</Tabs>

 ## Testing the application
 
![ alt text ]( ./screenshots/Commands_5.png "Hide Show Commands" )
