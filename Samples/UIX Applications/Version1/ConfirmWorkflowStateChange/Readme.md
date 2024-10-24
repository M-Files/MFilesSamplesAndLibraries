# Confirm Workflow State Change

This application registers a [VaultUI module](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Modules/#vaultui) that reacts when properties are changed on an object, allowing code to execute before the changes are sent to the server.

**This application is only compatible with M-Files Desktop.**

## Application structure

* The application consists of two files:
  * `appdef.xml`, which [declares the application contents](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Application-Definition/).
  * `vaultui.js`, a VaultUI module, which contains code which reacts to the `OnSetPropertiesOfObjectVersion` of the `VaultEntry`, and prompts the user for confirmation of the state change.

## Approach

The application broadly takes the following approach:

1. When the [VaultEntry is created](https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_NewVaultEntry.html), a handler is registered to react to the `OnSetPropertiesOfObjectVersion` event.
2. When the `OnSetPropertiesOfObjectVersion` event occurs, the properties on the object are checked to see whether a state transition ([MFBuiltInPropertyDefStateTransition](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFBuiltInPropertyDef.html)) has been defined.
3. If it has, the user is prompted to confirm their actions.  

## Testing and deploying

### Testing

The simplest method to test an application is to copy the files into a [local development folder](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Local-Development-Folder/).  This allows you to alter the files and simply log out and into the vault to see the changes.

### Deploying

To deploy the application, you must zip the application files and deploy them using the M-Files Admin tool.  This process is detailed [on the M-Files Developer Portal](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Development-Practices/Deployment/).

#### Pre-approval

When a client connects a vault, new User Interface Extensibility Framework applications will be downloaded and the user will be prompted to install them.  [This can be avoided by pre-approving it using registry keys](http://developer.m-files.com/Frameworks/User-Interface-Extensibility-Framework/Pre-Approval/).
