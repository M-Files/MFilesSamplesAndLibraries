# M-Files View URL Generator

This vault application creates a context menu item for creating a view URL for a selected view or a virtual folder.

*Please note: This is a consultancy template solution. This is not an official M-Files product. The solution can be used to provide certain functionality not available with the M-Files offering without customization. This solution does not have any official support. It is always used at your own risk and should be tested thoroughly for the use case before implementing in a production environment.*

## Usage

1. Select a view or a virtual folder
2. Select Get M-Files View URL from the context menu
3. Copy the view URL

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).

