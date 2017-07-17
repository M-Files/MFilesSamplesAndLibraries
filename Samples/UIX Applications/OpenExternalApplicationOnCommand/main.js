// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.
//		 Authored by: Craig Hawker / M-Files

"use strict";

// Hold a reference to our command button (populated in getShellFrameStartedHandler).
var g_MyCommandId = null;

// Hold a reference to the selected items (populated in getNewShellListingHandler).
var g_SelectedItems = null;

function OnNewShellUI(shellUI) {
	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param>

	// Register to listen new shell frame creation event.
	try {
		shellUI.Events.Register( Event_NewShellFrame, newShellFrameHandler );
	}
	catch (e) {} // Suppress any "not ready" exceptions.
}

function newShellFrameHandler(shellFrame) {
	/// <summary>Handles the OnNewShellFrame event.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The new shell frame object.</param>

	try {
		// Register to be notified when the shell frame is started.
		// Note: the shell frame may not be usable yet.
		shellFrame.Events.Register( Event_Started, getShellFrameStartedHandler( shellFrame ) );

		// Register to be notified when a new shell listing is created.
		// Note: we don't actually need this here, but often the application needs to be
		// passed details about the selected object(s), so useful to have this stub here.
		shellFrame.Events.Register( Event_NewShellListing, getNewShellListingHandler( shellFrame ) );
	}
	catch (e) {} // Suppress any "not ready" exceptions.
}

function getShellFrameStartedHandler(shellFrame) {
	/// <summary>Gets a function to handle the Started event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param>
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>
	return function() {

		g_MyCommandId = shellFrame.Commands.CreateCustomCommand( "Open Application" );

		// Add it to the context menu.
		// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~AddCustomCommandToMenu.html
		shellFrame.Commands.AddCustomCommandToMenu( g_MyCommandId, MenuLocation_ContextMenu_Top, 0 );

		try {
			// Add it to the task pane.
			// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ITaskPane~AddCustomCommandToGroup.html
			shellFrame.TaskPane.AddCustomCommandToGroup( g_MyCommandId, TaskPaneGroup_Main, 0 );
		}
		catch (e) {
			// This excepts inside the history view, as the task pane cannot be accessed.
		}

		// Register to be notified when someone clicks the button.
		shellFrame.Commands.Events.Register( Event_CustomCommand,
			function(command) {

				// We only care about our command.
				if (command != g_MyCommandId) {
					return;
				}

				// Open the application.
				var shell = new ActiveXObject( "WScript.Shell" );
				shell.Run( "notepad.exe" );

			} );

		// Hide the command everywhere (affects both context menu and task pane) by default.
		// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html
		shellFrame.Commands.SetCommandState( g_MyCommandId, CommandLocation_All, CommandState_Hidden );

	};
}

function getNewShellListingHandler(shellFrame) {
	/// <summary>Gets a function to handle the NewShellListing event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param>
	/// <returns type="MFiles.Events.OnNewShellListing">The event handler.</returns>

	// Return the handler function for NewShellListing event.
	return function(shellListing) {

		// Listen for selection change events on the listing
		shellListing.Events.Register( Event_SelectionChanged,
			function(selectedItems) {

				// We may get many events raised; only deal with the active listing.
				if (false == shellListing.IsActive) {
					return;
				}

				// Store the selected items.
				g_SelectedItems = selectedItems;

				// Sanity.
				if (null == g_MyCommandId) {
					return;
				}

				// There may be no point enabling our button if the user hasn't selected any objects we want to process.
				// For now, just assume they have (button is available always).
				var commandViable = true;

				// If the command is viable then enable the buttons.
				if (commandViable) {

					// Show the command everywhere (affects both context menu and task pane).
					// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html
					shellFrame.Commands.SetCommandState( g_MyCommandId, CommandLocation_All, CommandState_Active );

				}
				else {

					// Hide the command everywhere (affects both context menu and task pane).
					// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html
					shellFrame.Commands.SetCommandState( g_MyCommandId, CommandLocation_All, CommandState_Hidden );

				}

			} );
	};
}