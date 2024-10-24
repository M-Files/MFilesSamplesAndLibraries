// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI(shellUI)
{
	/// <summary>Executed by the UIX when a ShellUI module is started.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The shell UI object which was created.</param>

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
	/// <summary>Handles the OnNewNormalShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param>

	// The shell frame was created but it cannot be used yet.
	// The following line would throw an exception ("The object cannot be accessed, because it is not ready."):
	// shellFrame.ShowMessage("A shell frame was created");

	// Register to be notified when the shell frame is started.
	// This time pass a reference to the function to call when the event is fired.
	shellFrame.Events.Register(
		Event_Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler(shellFrame)
{
	/// <summary>Returns a function which handles the OnStarted event for an IShellFrame.</summary>

	return function()
	{
		// The shell frame is now started and can be used.
		shellFrame.ShowMessage( "A shell frame is available for use." );
	}
}