# M-Files Samples

Please note that [Custom External Object Type Data Source samples have moved to their own repository](https://github.com/M-Files/Samples.CustomExternalObjectTypeDataSources).

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
* `Simple Configuration` - An example of using the VAF 2.0 to create a simple configuration area in the M-Files 2018 (onwards) admin interface.
* `Complex Configuration` - An example of using the VAF 2.0 to create a more complex configuration area in the M-Files 2018 (onwards) admin interface. 
* `Xml Importer` - An example of a flexible Vault Application that can import data from XML files into M-Files objects.  This source code supports the training video available in the [M-Files Academy](https://m-files.csod.com/LMS/LoDetails/DetailsLo.aspx?loId=e600d2d9-677b-4a61-9483-d1b202b98004&back=%2fLMS%2fBrowseTraining%2fBrowseTraining.aspx#t=1).

## Processes

* `Chain Workflows` - An example to show how to move an object to a second workflow once it reaches the end of a first workflow.
* `Client to Server communication` - An example of how to execute server-side code on a client-side event (e.g. a command click).
