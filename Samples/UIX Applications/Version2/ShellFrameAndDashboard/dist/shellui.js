// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

function OnNewShellUI(shellUI) {

	/// <summary>Executed by the UIX when a ShellUI module is started.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The shell UI object which was created.</param>

	// This is the start point of a ShellUI module.

	// Register to be notified when a new shell frame (MFiles.Event.NewShellFrame) is created.
	shellUI.Events.Register(
		MFiles.Event.NewShellFrame,
		onNewNormalShellFrame
	);
}

function onNewNormalShellFrame(shellFrame) {

	// Add tab to right pane, when the shell frame is started.
	shellFrame.Events.Register(MFiles.Event.Started, onStarted);

	// NOTE: to be on the safe side, handle the callback in "async" function and await all the
	// return values, because when the postMessage API is used, all return values will be async.
	async function onStarted() {

		const dashboardCommand = await shellFrame.Commands.CreateCustomCommand("Show my dashboard");
		await shellFrame.Commands.AddCustomCommandToMenu(dashboardCommand, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1);
		await shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			(command) => {
				// Execute only our custom command.
				if (command === dashboardCommand) {
					shellFrame.ShowDashboard("MyDashboard");
				}
		});
	}
}

