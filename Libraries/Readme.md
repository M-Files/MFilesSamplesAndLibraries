# M-Files Libraries

*Please note that these libraries are provided "as-is" and with no warranty, explicit or otherwise.  You should ensure that the functionality of these libraries meet your requirements, and thoroughly test them, prior to using in any production scenarios.*

The following helper libraries are provided as work-in-progress, and may not be fully complete.

## MFaaP.MFilesAPI (M-Files API Helper Library)



## MFaaP.MFWSClient (C# M-Files Web Service Wrapper)

This library aims to provide an easy-to-use C# wrapper for the [M-Files Web Service](http://www.m-files.com/MFWS/), which is part of the M-Files Web Access.  The user guide contains more information on [setting up M-Files Web Access](http://www.m-files.com/user-guide/latest/eng/#Configure_M-Files_Web_Access.html).

It currently provides the following functionality:

* Authentication, both using credentials and using Windows Single Sign On (`MFWSClient.Authentication.cs`)
* Object creation (`MFWSClient.CreatingObjects.cs`)
* File upload (`MFWSClient.CreatingObjects.cs`)
* Vault extension method execution (`MFWSClient.ExtensionMethods.cs`)
* Searching (`MFWSClient.Searching.cs`)

Further details are available within the Readme.md file in the project folder.