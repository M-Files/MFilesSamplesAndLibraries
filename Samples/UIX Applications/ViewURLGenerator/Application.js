// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI(shellUI) {
	/// <summary>Executed by the UIX when a ShellUI module is started.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The shell UI object which was created.</param>

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

	// This will be a reference to the ID of the "Get M-Files View URL" command.
	var cmd;

	// Register to be notified when the shell frame is started.
	shellFrame.Events.Register(
		Event_Started,
		function () {

			// Create our custom command, initalized as hidden
			cmd = shellFrame.Commands.CreateCustomCommand("Get M-Files View URL...");
			shellFrame.Commands.SetCommandState(cmd, CommandLocation_ContextMenu, CommandState_Hidden);
			shellFrame.Commands.AddCustomCommandToMenu(cmd, MenuLocation_ContextMenu_Misc2_Top, 1);

			// Register to be notified when any custom command is clicked.
			shellFrame.Commands.Events.Register(
				Event_CustomCommand,
				function (commandId) {
					// Handle our "Create Link..." command
					if (commandId == cmd) {
						shellFrame.ShowPopupDashboard("dashboard", true, {});
					}
				});

		});

	// Register to be notified when a new shell listing is created so that we can react when items are selected.
	shellFrame.Events.Register(
		Event_NewShellListing,
		function (shellListing) {

			// Register to be notified when the context menu is shown on the listing.
			shellListing.Events.Register(
				Event_ShowContextMenu,
				function(selectedItems)
				{
					// exactly 1 view or property folder is selected
					if (selectedItems.Count === 1 &&
						selectedItems.Folders.Count === 1 &&
						(selectedItems.Folders.Item( 1 )
							.FolderDefType ==
							MFFolderDefTypeViewFolder ||
							selectedItems.Folders.Item( 1 )
							.FolderDefType ==
							MFFolderDefTypePropertyFolder))
					{
						// show our view/folder link command
						shellFrame.Commands.SetCommandState( cmd, CommandLocation_ContextMenu, CommandState_Active );
					}
					else
					{
						// hide our view/folder link command
						shellFrame.Commands.SetCommandState( cmd, CommandLocation_ContextMenu, CommandState_Hidden );
					}
				} );
		});
}

