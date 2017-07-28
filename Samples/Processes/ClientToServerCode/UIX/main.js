// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.
//		 Created by: Craig Hawker / M-Files

"use strict";

// The name of the vault extension method that should be executed when the "VBScript" command is executed.
var VBScriptVaultExtensionName = "VaultExtensionMethod_VBScript";

// The name of the vault extension method that should be executed when the "VAF" command is executed.
var VAFVaultExtensionName = "VaultExtensionMethod_VAF";

function OnNewShellUI(shellUI) {
	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param> 

	// Register to listen new shell frame creation event.
	shellUI.Events.Register(
		Event_NewShellFrame,
		newShellFrameHandler );
}

function newShellFrameHandler(shellFrame)
{
	/// <summary>Handles the OnNewShellFrame event.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The new shell frame object.</param> 

	// Register to listen to the started event.
	shellFrame.Events.Register(
		Event_Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler(shellFrame) {
	/// <summary>Gets a function to handle the Started event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>

	// Return the handler function for ShellFrame's Started event.
	return function ()
	{
		// Create a command to execute the VBScript version.
		var vbScriptCommand = shellFrame.Commands.CreateCustomCommand( "What is the time (VBScript)?" );

		// Create a command to execute the VAF version.
		var vafCommand = shellFrame.Commands.CreateCustomCommand( "What is the time (VAF)?" );

		// Add the commands to the task pane.
		// ref: http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ITaskPane~AddCustomCommandToGroup.html
		shellFrame.TaskPane.AddCustomCommandToGroup( vbScriptCommand, TaskPaneGroup_Main, 0 );
		shellFrame.TaskPane.AddCustomCommandToGroup( vafCommand, TaskPaneGroup_Main, 0 );

		// Register to respond to commands being clicked.
		shellFrame.Commands.Events.Register(
			Event_CustomCommand,
			function(command)
			{
				switch (command)
				{
					case vbScriptCommand: // It was the VBScript command.

						// Call the extension method passing the string "hello" as the parameter.
						// Note: this is called async for compatibility with M-Files Web Access.
						// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultExtensionMethodOperations~ExecuteVaultExtensionMethod.html
						shellFrame.ShellUI.Vault.Async.ExtensionMethodOperations.ExecuteVaultExtensionMethod(
							VBScriptVaultExtensionName, // The name of the extension method to execute.
							"hello", // The input (string) to pass it.
							function(response)
							{
								// The output (string) will be in response; show it.
								shellFrame.ShowMessage( response );
							} );
						break;

					case vafCommand: // It was the VAF command.

						// Call the extension method passing the string "world" as the parameter.
						// Note: this is called async for compatibility with M-Files Web Access.
						// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultExtensionMethodOperations~ExecuteVaultExtensionMethod.html
						shellFrame.ShellUI.Vault.Async.ExtensionMethodOperations.ExecuteVaultExtensionMethod(
							VAFVaultExtensionName, // The name of the extension method to execute.
							"world", // The input (string) to pass it.
							function(response)
							{
								// The output (string) will be in response; show it.
								shellFrame.ShowMessage( response );
							} );

						break;

					default : // It wasn't one of our commands.
						break;
				}

			} );
	};
}