# M-Files Libraries

*Please note that these libraries are provided "as-is" and with no warranty, explicit or otherwise.  You should ensure that the functionality of these libraries meet your requirements, and thoroughly test them, prior to using in any production scenarios.*

The following helper libraries are provided as work-in-progress, and may not be fully complete.

## MFaaP.MFilesAPI (M-Files API Helper Library)

This library provides helper and extension methods to more easily work with the M-Files API.

It currently provides the following functionality:

* Connection/Disconnection
* Searching
  * Creation of "Not Deleted" search condition
  * Creation of "Object Type" search condition
  * Creation of "Display Id" search condition
  * Execution of a segmented search to enumerate all objects in a large vault

Further details are available within the [project folder](MFaaP.MFilesAPI).

## MFaaP.MFWSClient (C# M-Files Web Service Wrapper)

This library aims to provide an easy-to-use C# wrapper for the [M-Files Web Service](http://www.m-files.com/MFWS/), which is part of the M-Files Web Access.  The user guide contains more information on [setting up M-Files Web Access](http://www.m-files.com/user-guide/latest/eng/#Configure_M-Files_Web_Access.html).

It currently provides the following functionality:

* Authentication, both using credentials and using Windows Single Sign On
* Object creation
* File upload
* Vault extension method execution
* Searching
* Retrieving view contents

Further details are available within the [project folder](MFaaP.MFWSClient).