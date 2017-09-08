// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI(shellUI)
{
	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param> 

	// Register to be notified when a new shell frame is created.
	shellUI.Events.Register(
		Event_NewShellFrame,
		handleNewShellFrame );
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