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
