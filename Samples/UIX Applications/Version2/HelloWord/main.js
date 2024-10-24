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
