# M-Files Samples

The following samples are available in this solution:

## M-Files API

* Searching
  * `Segmented Search` - An example of how to bypass the default search limitations of [SearchForObjectsByConditions](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations~SearchForObjectsByConditions.html) or [SearchForObjectsByConditionsEx](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations~SearchForObjectsByConditionsEx.html) by instead searching for "segments" of objects from the vault.
  * `Search by Display Id` - an example of how to search using the "display id" ("external id") when using [External Object Type Data Sources](http://www.m-files.com/user-guide/latest/eng/#Connection_to_external_database.html).

## M-Files Web Service

* `MFWSCheckOutStatus` - An example of retrieving the checkout status of a specific object.
* `MFWSDownloading` - An example of downloading files from the M-Files Web Service.
* `MFWSSearching` - An example of executing basic searches against the M-Files Web Service.
* `MFWSVaultStructure` - An example of retrieving vault structure from the M-Files Web Service.
* `MFWSViewNavigation` - An example of retrieving views and their contents from the M-Files Web Service.

## Vault Application Framework

Please note that the build actions have been disabled in these samples.  This has been done simply to stop them all being installed at build time.  To enable the build action, remove the "REM" command from the start of the build action.

* `Event Tracing` - An example to automatically log various vault events and VAF lifecycle events to the event log.  Useful to understand which events are executed in various scenarios.

## Processes

* `Chain Workflows` - An example to show how to move an object to a second workflow once it reaches the end of a first workflow.
* `Client to Server communication` - An example of how to execute server-side code on a client-side event (e.g. a command click).

