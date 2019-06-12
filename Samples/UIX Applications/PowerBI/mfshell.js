// Called when a new shell UI object is created.
function OnNewShellUI( shellUI )
{	
	// New commands in task pane. 
	var showCommand1 = null;
	var hideCommand = null;
	
	// Persistent browser sessions.
	var session1 = null;
	var group = null;
	var initialized = false;
	var tab = null;
	var vault = null;
	var metadataTab = null;

	return {

		// ShellUI.OnStop
		OnStop: function() {
			
			// Destroy persistent browser contents when shellUI is shutting down.
			//
			shellUI.DestroyPersistentContent( session1 );
		},
	
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

						showCommand1 = shellFrame.Commands.CreateCustomCommand( "View Report" );
						shellFrame.Commands.SetIconFromPath( showCommand1, "png/Report2.ico" );
						hideCommand = shellFrame.Commands.CreateCustomCommand( "Hide Report" );
						shellFrame.Commands.SetIconFromPath( hideCommand, "png/disabled.ico" );
						shellFrame.Taskpane.AddCustomCommandToGroup( showCommand1, group, 11 );
						shellFrame.Taskpane.AddCustomCommandToGroup( hideCommand, group, 12 );
						shellFrame.Commands.AddCustomCommandToMenu( showCommand1, MenuLocation_ContextMenu_Top, 11 );
						shellFrame.Commands.AddCustomCommandToMenu( hideCommand, MenuLocation_ContextMenu_Top, 12 );
						shellFrame.Commands.SetCommandState( showCommand1, CommandLocation_All, CommandState_Hidden );
						shellFrame.Commands.SetCommandState( hideCommand, CommandLocation_All, CommandState_Hidden );
					}
				},
				
				OnNewShellListing: function( shellListing )
				{
					return {          					
						OnSelectionChanged: function( selectedItems )
						{	
							//shellFrame.RightPane.ShowDefaultContent();
							if ( tab == null)
							{
								
							}
							else
							{
								try
								{
									tab.Remove();
									tab = null;
								}
								catch(err)
								{
									
								}
							}
									
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
											shellFrame.Commands.SetCommandState( showCommand1, CommandLocation_All, CommandState_Active );
											shellFrame.Commands.SetCommandState( hideCommand, CommandLocation_All, CommandState_Active );
										}
									}
									else
									{
										//shellFrame.RightPane.ShowDefaultContent();
										if ( tab == null)
										{
											
										}
										else
										{
											try
											{
												tab.Remove();
												tab = null;
											}
											catch(err)
											{
											
											}
										}
										
										if( shellFrame.TaskPane.Available )
										{
											shellFrame.Commands.SetCommandState( showCommand1, CommandLocation_All, CommandState_Hidden );
											shellFrame.Commands.SetCommandState( hideCommand, CommandLocation_All, CommandState_Hidden );
										}
										
									}
								}
								else
								{
									if( shellFrame.TaskPane.Available )
									{
										shellFrame.Commands.SetCommandState( showCommand1, CommandLocation_All, CommandState_Hidden );
										shellFrame.Commands.SetCommandState( hideCommand, CommandLocation_All, CommandState_Hidden );
									}
									
								}
							}
							else
							{
								if( shellFrame.TaskPane.Available )
								{
									shellFrame.Commands.SetCommandState( showCommand1, CommandLocation_All, CommandState_Hidden );
									shellFrame.Commands.SetCommandState( hideCommand, CommandLocation_All, CommandState_Hidden );
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
							if ( commandId == showCommand1 ) {	
								tab = null;								
									
								// Attach browser content to right pane.
								var selectedObjectVersion = shellFrame.ActiveListing.CurrentSelection.ObjectVersions[0];
								var objver = selectedObjectVersion.ObjVer;
								//vault = shellFrame.ShellUI.Vault;
								var urlid = shellFrame.ShellUI.Vault.PropertyDefOperations.GetPropertyDefIDByAlias( "M-Files.PowerBI.URL" );
								var tsid = shellFrame.ShellUI.Vault.ObjectPropertyOperations.GetProperty(objver, urlid).Value.Value + "?chromeless=1";
								
								if ( session1 == null) {
									session1 = shellFrame.shellUI.CreatePersistentBrowserContent( tsid, {
									defaultvisibility: true,
									persistentid: 10
									} );
								}
								else {
									shellFrame.shellUI.SetPersistentBrowserContent( tsid, session1 );
								}
								
								if ( tab == null)
								{
									tab = shellFrame.RightPane.AddTab( "mfiles.powerbi.tab", "Reports", "_last" );
								}
								// Make the tab visible.
								tab.ShowPersistentContent( session1 );
								tab.Visible = true;	
								tab.select();

								// Resize to adjust it to frame
								var paneSize = shellFrame.RightPane.Size;
								shellFrame.RightPane.Size = paneSize+1;
							} 							
							else if ( commandId == hideCommand ) {
								// Detach browser content from right pane.	
								if ( tab == null)
								{
									
								}
								else
								{
									tab.Remove();
									tab = null;
									//tab.Visible = false;
								}
							}
						}						 
					};
					
				}  // end of OnNewCommands		
				
			};  // end of return

        }  // end of OnNewShellFrame
		
    };  // end of return						 

}  // end of OnNewShellUI  

