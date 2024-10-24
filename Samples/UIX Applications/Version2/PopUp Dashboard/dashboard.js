function OnNewDashboard( dashboard ) {

    /// <summary>Executed by the UIX when a dashboared is started.</summary>
	/// <param name="dashboard" type="MFiles.Dashboard">The dashboard object which was created.</param>

	// Get the viewshistory and sort items in descending order.
    let viewsHistory = dashboard.CustomData.viewsHistory || [];
    viewsHistory = viewsHistory.sort( ( a, b ) => b.time - a.time );

    // Register a handler to listen the started event.
    dashboard.Events.Register(
        MFiles.Event.Started,
        () => {

            // Get the element from the UI.
            const contentElement = document.getElementById( "content" );

            // Prepare the html content that to be updated.
            const htmlContent = `
                <div class="label">Your view history</div>
                ${viewsHistory.map( ( value ) => {
                    return`<div class="history-item">
                            <div class="viewid">${value.viewUrl } </div> 
                            <div class="viewpath">- ${value.viewPath } </div> 
                        </div>`; }
                ).join( "" ) }
            `;

            // Update the content.
            contentElement.innerHTML = htmlContent;
        }
)   ;
}
