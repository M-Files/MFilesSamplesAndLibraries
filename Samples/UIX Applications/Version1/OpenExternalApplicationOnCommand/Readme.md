# Open External Application On Command

This application registers a [ShellUI module](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Modules/#shellui) that adds a [Command](https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~CreateCustomCommand.html) to both the task pane and the context menu.  This command, when clicked, opens `Notepad.exe` on the client machine.

**This application is only compatible with M-Files Desktop.**

## Application structure

* The application consists of two files:
  * `appdef.xml`, which [declares the application contents](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Application-Definition/).
  * `main.js`, a ShellUI module, which creates the command (button) and opens Notepad as appropriate.

## Approach

The application broadly takes the following approach:

1. When the [shell frame is started](https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_Started.html), a command is [created with the text "Open Application"](https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~CreateCustomCommand.html).
2. The command is added to the [context menu](http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~AddCustomCommandToMenu.html) and to the [task pane](http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ITaskPane~AddCustomCommandToGroup.html), and is set to [hidden by default](http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html).
3. When the selection changes in the active listing, the currently-selected items are stored and the [command is shown](http://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~ICommands~SetCommandState.html).  *The selected items are not used in the current implementation, but it may be useful to only show the command in certain situations, or to pass the selected item data to the external application, so is kept here for reference.*
4. When the command is executed (the button is clicked), [WScript.Shell](https://msdn.microsoft.com/en-us/library/aew9yb99(v=vs.84).aspx) is instantiated and `Notepad.exe` is [run](https://msdn.microsoft.com/en-us/library/d5fk67ky(v=vs.84).aspx).

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).
