# M-Files UIX Sample Applications

The following applications are available:

## [Hello, World (ShellUI)](HelloWorld)

Creates a basic UIX ShellUI application - consisting of an appdef.xml and a single module - that displays a message when the shell frame object becomes available for use.

## [Commands (ShellUI)](Commands)

Creates two commands that interact with each other.

## [Open External Application On Demand (ShellUI)](OpenExternalApplicationOnDemand)

Adds a command (button) to the task area and context menu, opening Notepad when the button is clicked.

## [Assign to me](AssignToMe)

Creates an "Assign to me" button which is only shown when at least one object is selected.  When the button is clicked, an assignment is created for the selected object(s), and assigned to the current user.  This application is compatible with M-Files Web Access as well as M-Files Desktop.

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).
