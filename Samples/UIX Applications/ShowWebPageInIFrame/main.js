// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI( shellUI )
{
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
		getShellFrameStartedHandler(shellFrame) );
}

function getShellFrameStartedHandler(shellFrame)
{
	/// <summary>Returns a function that handles the OnStarted event for an IShellFrame.</summary>

	return function()
	{
		// The shell frame is now started and can be used.

		// Create a new tab for the content to be shown in.
		// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~IShellPaneContainer~AddTab.html
		// Tab ids ref: https://www.m-files.com/UI_Extensibility_Framework/#SidePaneTabs.html
		var tab = shellFrame.RightPane.AddTab( "myDashboardId", "Search", "_last" );

		// Show our dashboard.
		tab.ShowDashboard("MyDashboard", {});

		// Show the tab.
		tab.visible = true;

		// Register to be notified when new shell listings are created.
		shellFrame.Events.Register(
			Event_NewShellListing,
			getNewShellListingHandler( shellFrame, tab ) );

		// Is there already a listing?  If so then we need to hook into it as well.
		if (null != shellFrame.Listing)
		{
			getNewShellListingHandler( shellFrame, tab )( shellFrame.Listing );
		}
	};
}

function getNewShellListingHandler(shellFrame, tab)
{
	/// <summary>Gets a function to handle the NewShellListing event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnNewShellListing">The event handler.</returns>

	// Return the handler function for NewShellListing event.
	return function(shellListing)
	{
		// Listen for selection change events on the listing.
		shellListing.Events.Register(
			Event_SelectionChanged,
			function(selectedItems)
			{
				// Sanity.
				if (false == shellListing.IsActive)
				{
					return false;
				}

				// Show our dashboard.
				tab.ShowDashboard("MyDashboard", selectedItems);

				// We succeeded; return true.
				return true;
			} );
	};
}