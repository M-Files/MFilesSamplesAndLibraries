# Persistent Web Page

This application registers a [ShellUI module](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Modules/#shellui) that creates a persistent web page and shows the content within a tab on the right-hand-side of the interface.  The navigation within the tab is persisted even as the user interacts with M-Files (e.g. by navigating into a view).

More information is available within the [M-Files Developer Portal](http://developer.m-files.com/Samples-And-Libraries/Samples/User-Interface-Extensibility-Framework/Display-Persistent-Web-Page-In-Tab/).

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).