# Show Web Page in iframe

This application registers a [ShellUI module](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Modules/#shellui) that reacts when users select items in the listings and searches for the object title on Microsoft Bing.

**This application is only compatible with M-Files Desktop.**

## Application structure

* The application consists of three files:
  * `appdef.xml`, which [declares the application contents](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Application-Definition/).
  * `main.js`, a ShellUI module, which creates the command (button) and opens Notepad as appropriate.
  * `dashboard.html`, the dashboard shown in the tab.  The dashboard contains an iframe which is shown full-screen, and renders the website within it.

## Approach

The application broadly takes the following approach:

1. When the [shell frame is started](https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_Started.html), the code registers a new tab.
2. When the selected items are changed in the active shell listing, the currently-selected items are passed to the dashboard.
3. The dashboard iterates over the selected items, builds up a search string, and alters the `src` attribute of the `iframe` to execute the search.

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).
