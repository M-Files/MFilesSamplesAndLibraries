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

    // Register to be notified when the view location changed.
    shellFrame.Events.Register(
        MFiles.Event.ViewLocationChanged,
        getViewLocationChangedHandler( shellFrame ) );
}

function getShellFrameStartedHandler( shellFrame ) {

	/// <summary>Returns a function which handles the OnStarted event for an IShellFrame.</summary>

	// The shell frame is now started and can be used.

	return async () => {

		// Create a command (button).  Note that it is not yet visible.
		const showViewHistoryCmd = await shellFrame.Commands.CreateCustomCommand( "Show view history" );

		// Add the first command to the main menu.
		await shellFrame.Commands.AddCustomCommandToMenu( showViewHistoryCmd, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );

        // Set command visility.
        await shellFrame.Commands.SetCommandState(
            showViewHistoryCmd,
            MFiles.CommandLocation.MainMenu,
            MFiles.CommandState.CommandState_Active );

		// Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( command ) => {

				// Execute only our custom command.
				if( command === showViewHistoryCmd ) {

					// Get view history from the storage.
					const viewHistoryString  = await MFiles.ReadFromWebStorage( "viewsHistory" );
					let viewsHistory = [];
					if( viewHistoryString ) {
						viewsHistory = JSON.parse( viewHistoryString );
					}

                    // Show popup dashboard with custom data.
                    shellFrame.ShowPopupDashboard( "MySample",
                        {
							viewsHistory: viewsHistory
						},
						"Views History" );
                    }
		} );
	};
}

function getViewLocationChangedHandler( shellFrame ) {

	/// <summary>Returns a function which handles the OnViewLocationChange event.</summary>

	return async () => {

		// Get the view history from storage.
		const viewHistoryString  = await MFiles.ReadFromWebStorage( "viewsHistory" );
		let viewHistory = [];

		// Parse the view history from the string.
		if( viewHistoryString ) {
			viewHistory = JSON.parse( viewHistoryString );
		}

		// Prepare the current view object.
		viewHistory.push( {
			time: Date.now(),
			viewUrl: shellFrame.CurrentUrl,
			viewPath:  shellFrame.CurrentPath || "All"
		} );

		// Write viewhistory to web storage.
		await MFiles.WriteToWebStorage( "viewsHistory", JSON.stringify( viewHistory ) );
	};
}
