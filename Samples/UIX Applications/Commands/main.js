// The current shell frame.
var g_shellFrame = null;

// The command Id of the first command.
var g_commandOneId = null;

// The command Id of the second command.
var g_commandTwoId = null;
 
function OnNewShellUI( shellUI )
{
	/// <summary>Executed by the UIX when a ShellUI module is started.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The shell UI object which was created.</param>
 
	// This is the start point of a ShellUI module.
 
	// Register to be notified when a new shell frame (Event_NewShellFrame) is created.
	shellUI.Events.Register(
		Event_NewShellFrame,
		handleNewShellFrame );
}
 
function handleNewShellFrame(shellFrame)
{
	/// <summary>Handles the OnNewShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param>
 
	// The shell frame was created but it cannot be used yet.
	// The following line would throw an exception ("The object cannot be accessed, because it is not ready."):
	// shellFrame.ShowMessage("A shell frame was created");
 
	// Update global scope variable to point to new shell frame.
	g_shellFrame = shellFrame;
 
	// Register to be notified when the shell frame is started.
	// This time pass a reference to the function to call when the event is fired.
	shellFrame.Events.Register(
		Event_Started,
		handleShellFrameStarted );
}
 
function handleShellFrameStarted()
{
	/// <summary>Handles the OnStarted event for an IShellFrame.</summary>
 
	// The shell frame is now started and can be used.
	// Note: we need to use the global-scope variable.

	// Create a command (button).  Note that it is not yet visible.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~CreateCustomCommand.html
	g_commandOneId = g_shellFrame.Commands.CreateCustomCommand("My First Command");

	// Create a command (button).  Note that it is not yet visible.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~CreateCustomCommand.html
	g_commandTwoId = g_shellFrame.Commands.CreateCustomCommand("My Second Command");

	// Hide the second command.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html
	g_shellFrame.Commands.SetCommandState(g_commandTwoId, CommandLocation_All, CommandState_Hidden);

	// Set the icon for the command.
	g_shellFrame.Commands.SetIconFromPath(g_commandOneId, "icons/uparrow.ico");

	// Add the first and second commands to the task area.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ITaskPane~AddCustomCommandToGroup.html
	g_shellFrame.TaskPane.AddCustomCommandToGroup(g_commandOneId, TaskPaneGroup_Main, 1);
	g_shellFrame.TaskPane.AddCustomCommandToGroup(g_commandTwoId, TaskPaneGroup_Main, 1);

	// Add the first and second commands to the context menu.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~AddCustomCommandToMenu.html
	g_shellFrame.Commands.AddCustomCommandToMenu(g_commandOneId, MenuLocation_ContextMenu_Top, 1);
	g_shellFrame.Commands.AddCustomCommandToMenu(g_commandTwoId, MenuLocation_ContextMenu_Top, 1);

	// Register to be notified when a custom command is clicked.
	// Note: this will fire for ALL custom commands, so we need to filter out others.
	g_shellFrame.Commands.Events.Register(
		Event_CustomCommand,
		function(commandId)
		{
			// Branch depending on the Id of the command that was clicked.
			switch(commandId)
			{
				case g_commandOneId:
					// Our first command was clicked.
					handleFirstCommandClicked();
					break;
				case g_commandTwoId:
					// Our second command was clicked.
					handleSecondCommandClicked();
					break;
			}
		});

}
function handleFirstCommandClicked()
{
	/// <summary>Handles the click of the first command.</summary>
	
	// Show the second command.
	g_shellFrame.Commands.SetCommandState(g_commandTwoId, CommandLocation_All, CommandState_Active);
}
function handleSecondCommandClicked()
{
	/// <summary>Handles the click of the first command.</summary>
	
	// Disable the second command.
	g_shellFrame.Commands.SetCommandState(g_commandTwoId, CommandLocation_All, CommandState_Inactive);
}