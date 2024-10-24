// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI(shellUI)
{
	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param> 

	// Note: We want to hook into the built-in commands being executed in any shell frame,
	// but we want our command to only be used in NORMAL shell frames (as the task pane etc. is not available in others).

	// Register to be notified when a new shell frame is created.
	// This will be used to hook into the built-in commands whereever they are executed.
	shellUI.Events.Register(
		Event_NewShellFrame,
		handleNewShellFrame );	

	// Register to be notified when a new normal shell frame (Event_NewNormalShellFrame) is created.
	// We use Event_NewNormalShellFrame rather than Event_NewShellFrame as this won't fire for history (etc.) dialogs.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_NewNormalShellFrame.html
	// This will be used to add the button that executes a built-in command to the task pane.
	shellUI.Events.Register(
		Event_NewNormalShellFrame,
		handleNewNormalShellFrame );
}

function handleNewShellFrame(shellFrame)
{
	/// <summary>Handles the OnNewShellFrame event.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The new shell frame object.</param> 

	// Register to be notified of the started event.
	shellFrame.Events.Register(
		Event_Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler(shellFrame)
{
	/// <summary>Gets a function to handle the Started event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>

	// Return the handler function for Started event.
	return function()
	{
		// Register to be notified when a built-in command is executed.
		shellFrame.Commands.Events.Register(
			Event_BuiltinCommand,
			function(commandId, param)
			{
				/// <summary>Executed whenever a built-in command is clicked.</summary>
				/// <param name="commandId" type="BuiltinCommand">
				/// One of the built-in commands from the BuiltinCommand enumeration.
				/// ref: https://www.m-files.com/UI_Extensibility_Framework/#MFClientScript~BuiltinCommand.html
				/// </param> 
				/// <param name="param">
				/// If the <paramref name="commandId"/> is BuiltinCommand_NewObject then contains the object type id of the object to create (or -100 if not specified).
				/// Otherwise, returns -2.
				/// </param>
				/// <returns>A boolean defining whether the action should continue (true) or be cancelled (false).</returns>

				// Display every built-in command as message box.
				shellFrame.ShowMessage( "Command ID: " + commandId + ", param: " + param );

				// UI ext app should return true when nothing is processed and want to continue default command behaviour.
				// Although default value is true when nothing is returned.
				// Return false to cancel the standard command execution.
				return true;
			} );
	};
}

function handleNewNormalShellFrame(shellFrame)
{
	/// <summary>Handles the OnNewNormalShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param>

	// The shell frame was created but it cannot be used yet.
	// The following line would throw an exception ("The object cannot be accessed, because it is not ready."):
	// shellFrame.ShowMessage("A shell frame was created");

	// Register to be notified when the shell frame is started.
	shellFrame.Events.Register(
		Event_Started,
		getNormalShellFrameStartedHandler(shellFrame) );
}


function getNormalShellFrameStartedHandler(shellFrame)
{
	/// <summary>Gets a function to handle the Started event for the normal shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>

	// Return the handler function for Started event.
	return function()
	{
		// Create a command (button).  Note that it is not yet visible.
		// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~CreateCustomCommand.html
		var documentFromScannerCommand = shellFrame.Commands.CreateCustomCommand( "Add document from scanner" );

		// Add the command to the task area.
		// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ITaskPane~AddCustomCommandToGroup.html
		shellFrame.TaskPane.AddCustomCommandToGroup( documentFromScannerCommand, TaskPaneGroup_Main, 1 );

		// Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			Event_CustomCommand,
			function(commandId)
			{
				// Ignore other commands.
				if (commandId != documentFromScannerCommand)
					return;

				// Execute a built-in command.
				// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~ExecuteCommand.html
				// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~BuiltinCommand.html
				shellFrame.Commands.ExecuteCommand( BuiltinCommand_AddDocumentFromScanner, null );
			} );
	};
}