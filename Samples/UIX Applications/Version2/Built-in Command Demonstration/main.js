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