# Alter Context Menu Depending on SelectedObject

This application registers a [ShellUI module](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Modules/#shellui) that declares a command which is only available when a single object is selected and the context menu (right-click menu) opened.

This approach can be used to filter commands to only show - for example - if objects of a certain type are selected when the context menu is triggered.

## Application structure

* The application consists of two files:
  * `appdef.xml`, which [declares the application contents](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Application-Definition/).
  * `main.js`, a ShellUI module, which creates a command (menu item) and attaches it to the context menu.

## Approach

The application broadly takes the following approach:

1. When the [shell frame is started](https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_Started.html), the code creates a command and adds it to the context menu.
2. When the `IShellListing.OnShowContextMenu` event is fired, the system checks how many items are selected and shows or hides the command accordingly:
   * If there is only one item selected then the command is visible.
   * If there are zero items selected, or more than one item selected, the command is hidden.

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).
