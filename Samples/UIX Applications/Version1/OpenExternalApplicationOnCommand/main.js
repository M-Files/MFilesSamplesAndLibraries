// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI(shellUI)
{
	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param>

	// This is the start point of a ShellUI module.

	
	// Register to be notified when a new normal shell frame (Event_NewNormalShellFrame) is created.
	// We use Event_NewNormalShellFrame rather than Event_NewShellFrame as this won't fire for history (etc.) dialogs.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_NewNormalShellFrame.html
	shellUI.Events.Register(
		Event_NewNormalShellFrame,
		handleNewNormalShellFrame );
}

function handleNewNormalShellFrame(shellFrame)
{
	/// <summary>Handles the Event_NewNormalShellFrame event.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The new shell frame object.</param>

	// Register to be notified when the shell frame is started.
	// Note: the shell frame may not be usable yet.
	shellFrame.Events.Register(
		Event_Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler(shellFrame)
{
	/// <summary>Gets a function to handle the Started event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param>
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>

	return function()
	{
		var commandId = shellFrame.Commands.CreateCustomCommand( "Open Application" );

		// Add it to the context menu.
		// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~AddCustomCommandToMenu.html
		shellFrame.Commands.AddCustomCommandToMenu( commandId, MenuLocation_ContextMenu_Top, 0 );

		// Add it to the task pane.
		// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ITaskPane~AddCustomCommandToGroup.html
		shellFrame.TaskPane.AddCustomCommandToGroup( commandId, TaskPaneGroup_Main, 0 );

		// Register to be notified when someone clicks the button.
		shellFrame.Commands.Events.Register(
			Event_CustomCommand,
			function(command)
			{
				// We only care about our command.
				if (command != commandId)
				{
					return;
				}

				// Open the application.
				var shell = new ActiveXObject( "WScript.Shell" );
				shell.Run( "notepad.exe" );
			} );

		// Hide the command everywhere (affects both context menu and task pane) by default.
		// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html
		shellFrame.Commands.SetCommandState( commandId, CommandLocation_All, CommandState_Hidden );

		// Register to be notified when a new shell listing is created.
		// Note: we don't actually need this here, but often the application needs to be
		// passed details about the selected object(s), so useful to have this stub here.
		shellFrame.Events.Register( Event_NewShellListing, getNewShellListingHandler( shellFrame, commandId ) );
	};
}

function getNewShellListingHandler(shellFrame, commandId)
{
	/// <summary>Gets a function to handle the NewShellListing event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param>
	/// <returns type="MFiles.Events.OnNewShellListing">The event handler.</returns>

	// Return the handler function for NewShellListing event.
	return function(shellListing)
	{
		// Listen for selection change events on the listing
		shellListing.Events.Register(
			Event_SelectionChanged,
			function(selectedItems)
			{
				// We may get many events raised; only deal with the active listing.
				if (false == shellListing.IsActive)
				{
					return;
				}

				// Sanity.
				if (null == commandId)
				{
					return;
				}

				// There may be no point enabling our button if the user hasn't selected any objects we want to process.
				// For now, just assume they have (button is available always).
				var commandViable = true;

				// If the command is viable then enable the buttons.
				if (commandViable)
				{
					// Show the command everywhere (affects both context menu and task pane).
					// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html
					shellFrame.Commands.SetCommandState( commandId, CommandLocation_All, CommandState_Active );
				}
				else
				{
					// Hide the command everywhere (affects both context menu and task pane).
					// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html
					shellFrame.Commands.SetCommandState( commandId, CommandLocation_All, CommandState_Hidden );
				}
			} );
	};
}