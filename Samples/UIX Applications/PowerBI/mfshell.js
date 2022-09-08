// Called when a new shell UI object is created.
function OnNewShellUI( shellUI )
{	
	// New commands in task pane. 
	var showCommand = null;
	var group = null;

	return {
	
		// Called when a new shell frame object is created.
        OnNewShellFrame: function( shellFrame )
		{
			
			return {
				
				// ShellFrame.OnStarted.
				OnStarted: function( ) {
				
					// Create new commands to task pane if it exists.
					if( shellFrame.TaskPane.Available ) {
					
						// Create new command group and commands to show and hide persistent browser content
						// and add the commands to task pane.
						group = shellFrame.Taskpane.CreateGroup( "Report", 0 );

						showCommand = shellFrame.Commands.CreateCustomCommand( "View Report" );
						shellFrame.Commands.SetIconFromPath( showCommand, "png/Report2.ico" );
						shellFrame.Taskpane.AddCustomCommandToGroup( showCommand, group, 11 );
						shellFrame.Commands.AddCustomCommandToMenu( showCommand, MenuLocation_ContextMenu_Top, 11 );
						shellFrame.Commands.SetCommandState( showCommand, CommandLocation_All, CommandState_Hidden );
					}
				},
				
				OnNewShellListing: function( shellListing )
				{
					return {          					
						OnShowContextMenu: function(selectedItems)
						{	
							// Get selected items.
							if (selectedItems.count > 0)
							{
								var objectVersions = selectedItems.ObjectVersions;
								if( objectVersions.Count == 1 ) 
								{
									vault = shellFrame.ShellUI.Vault;
									var repid = shellFrame.ShellUI.Vault.ObjectTypeOperations.GetObjectTypeIDByAlias( "M-Files.PowerBI.Report" );
									if( objectVersions(1).ObjVer.Type == repid )
									{								
										if( shellFrame.TaskPane.Available )
										{
											shellFrame.Commands.SetCommandState( showCommand, CommandLocation_All, CommandState_Active );
										}
									}
									else
									{	
										if( shellFrame.TaskPane.Available )
										{
											shellFrame.Commands.SetCommandState( showCommand, CommandLocation_All, CommandState_Hidden );
										}
										
									}
								}
								else
								{
									if( shellFrame.TaskPane.Available )
									{
										shellFrame.Commands.SetCommandState( showCommand, CommandLocation_All, CommandState_Hidden );
									}
									
								}
							}
							else
							{
								if( shellFrame.TaskPane.Available )
								{
									shellFrame.Commands.SetCommandState( showCommand, CommandLocation_All, CommandState_Hidden );
								}
								
							}
						}
					};
				},

				// ShellFrame.OnNewCommands.
				OnNewCommands: function( commands ) {
					return {          
					
						// Handle custom commands.
						OnCustomCommand: function( commandId ) {					
							// User has clicked command to show browser content.
							if ( commandId == showCommand ) {	
								tab = null;								
									
								// Attach browser content to right pane.
								var selectedObjectVersion = shellFrame.ActiveListing.CurrentSelection.ObjectVersions[0];
								var objver = selectedObjectVersion.ObjVer;
								//vault = shellFrame.ShellUI.Vault;
								var urlid = shellFrame.ShellUI.Vault.PropertyDefOperations.GetPropertyDefIDByAlias( "M-Files.PowerBI.URL" );
								var tsid = shellFrame.ShellUI.Vault.ObjectPropertyOperations.GetProperty(objver, urlid).Value.Value;
								MFiles.ExecuteURL(tsid);
							} 
						}						 
					};
					
				}  // end of OnNewCommands		
				
			};  // end of return

        }  // end of OnNewShellFrame
		
    };  // end of return						 

}  // end of OnNewShellUI  

