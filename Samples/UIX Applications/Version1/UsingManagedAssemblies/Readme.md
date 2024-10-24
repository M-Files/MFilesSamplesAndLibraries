# Using managed assemblies

This application registers a [ShellUI module](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Modules/#shellui) that interacts with the supplied managed assembly, showing a message when the shell frame changes.

**This application is only compatible with M-Files Desktop.**

More information on using Managed Assemblies is available on the [UIX reference site](https://www.m-files.com/UI_Extensibility_Framework/index.html#UsingManagedAssembliesWithUIExtensibilityApplications.html).  Specifically, managed assemblies can only be used if:

* They target .NET 4.0 or higher.
* Complex types passed from JavaScript to C# (e.g. M-Files API objects) must be declared as `dynamic` in C#.
* The managed assembly must not reference the M-Files API.
* The managed assembly must be available in the UIX application package and are distributed as part of the UIX installation.

## Application structure

* The application consists of four files:
  * `appdef.xml`, which [declares the application contents](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Application-Definition/).
  * `main.js`, a ShellUI module, which instantiates the CLR objects and calls methods on them.
  * `MyClassLibrary.dll` (and optionally `MyClassLibrary.pdb`, containing debugging information), which is the managed assembly that the UIX application will use.

## Approach

The application broadly takes the following approach:

1. When the [shell frame is started](https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_Started.html), the code instantiates an instance of the managed assembly.
2. A method on the managed assembly is called, passing in information about the current environment (the outer window handle and the Vault), and a message to show.

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).
