// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI(shellUI) {
	// This is the start point of a ShellUI module.

	// Register to be notified when a new normal shell frame (Event_NewNormalShellFrame) is created.
	// We use Event_NewNormalShellFrame rather than Event_NewShellFrame as this won't fire for history (etc.) dialogs.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_NewNormalShellFrame.html
	shellUI.Events.Register(
		Event_NewNormalShellFrame,
		handleNewNormalShellFrame);
}

function handleNewNormalShellFrame(shellFrame) {
	/// <summary>Handles the OnNewNormalShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param> 

	// The shell frame was created but it cannot be used yet.
	// The following line would throw an exception ("The object cannot be accessed, because it is not ready."):
	// shellFrame.ShowMessage("A shell frame was created");

	// Register to be notified when the shell frame is started.
	// This time pass a reference to the function to call when the event is fired.
	shellFrame.Events.Register(
		Event_Started,
		getShellFrameStartedHandler(shellFrame));
}

function getShellFrameStartedHandler(shellFrame) {
	/// <summary>Returns a function that handles the OnStarted event for an IShellFrame.</summary>

	return function () {
		// The shell frame is now started and can be used.

		// Create the command.
		var myCommandId = shellFrame.Commands.CreateCustomCommand("My command");
		shellFrame.Commands.AddCustomCommandToMenu(myCommandId, MenuLocation_ContextMenu_Top, 1);

		// Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			Event_CustomCommand,
			function (commandId) {
				shellFrame.ShowMessage("The command was clicked");

				return true;
			});

		// Register to be notified when new shell listings are created.
		shellFrame.Events.Register(
			Event_NewShellListing,
			getNewShellListingHandler(shellFrame, myCommandId));

		// Is there already a listing?  If so then we need to hook into it as well.
		if (null != shellFrame.Listing) {
			getNewShellListingHandler(shellFrame, myCommandId)(shellFrame.Listing);
		}
	};
}

function getNewShellListingHandler(shellFrame, commandId) {
	/// <summary>Gets a function to handle the NewShellListing event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnNewShellListing">The event handler.</returns>

	// Return the handler function for NewShellListing event.
	return function (shellListing) {
		// Listen for selection change events on the listing.
		shellListing.Events.Register(
			Event_ShowContextMenu,
			function (selectedItems) {
				// Sanity.
				if (false == shellListing.IsActive) {
					return false;
				}

				// Was there only one item selected (and is it an object version)?
				var isOneObjectSelected = selectedItems.Count == 1 && selectedItems.ObjectVersions.Count == 1;

				// Show the context menu item only if there is 1 object selected.
				// Additionally, this could check the type of the object (e.g. so the context menu is only shown on specific object types).
				shellFrame.Commands.SetCommandState(
					commandId,
					CommandLocation_All,
					isOneObjectSelected ? CommandState_Active : CommandState_Hidden
				);

				// We succeeded; return true.
				return true;
			});
	};
}