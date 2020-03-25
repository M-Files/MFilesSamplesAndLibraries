# M-Files Samples and Libraries

*Please read the attached [LICENSE](LICENSE.md) file for detailed information.*

This solution contains a series of libraries and samples to help developers build applications using [M-Files](http://www.m-files.com).  These libraries and samples are designed to sit alongside our dedicated [M-Files Developer Portal](http://developer.m-files.com), where further information on the APIs and Frameworks that M-Files expose can be found.

Whilst the code aims to be as compatible as possible, please note that this is currently tested using the following configuration:

* The latest public M-Files build
* Visual Studio 2019

**We expect people interacting with this repository to follow our [code of conduct](CODE_OF_CONDUCT.md), and have published [contribution guidelines](CONTRIBUTING.md).**

## Please also note that there are additional public repositories available under the [M-Files Organisation](https://github.com/M-Files/):

* [Sample Custom External Object Type Data Sources](https://github.com/M-Files/Samples.CustomExternalObjectTypeDataSources) - provides sample applications that allow M-Files to retrieve non-document data (e.g. Customers or Projects) from external systems.  The M-Files Developer Portal has more information on [External Object Type Data Sources](https://developer.m-files.com/Built-In/External-Object-Type-Data-Source/).
* [M-Files COM API Extension Methods (Community)](https://github.com/M-Files/COMAPI.Extensions.Community) - contains a set of open-source, community-driven .NET extension methods that extend the functionality/usability of the M-Files COM API object model.
* [Vault Application Framework Extension Methods (Community)](https://github.com/M-Files/VAF.Extensions.Community) - contains a set of open-source, community-driven .NET extension methods that extend the functionality/usability of the Vault Application Framework.  Note: this library references the COM API extension methods.
* [M-Files Web Service .NET Wrapper Library](https://github.com/M-Files/Libraries.MFWSClient) - contains a sample .NET library that wraps the M-Files Web Service, allowing developers to interact with an object model similar to the M-Files COM API, but using the M-Files Web Service.

# Visual Studio solutions

Two Visual Studio solution files (`.sln`) are available within this repository: `MFSamplesAndLibraries` and `MFWSSamplesAndLibraries`.

* `MFSamplesAndLibraries` is a solution containing all samples and libraries, and can be used to easily open and investigate the entire codebase.  This requires that you have the latest M-Files build installed on your development machine.
* `MFWSSamplesAndLibraries` contains only the libraries and samples used to access the M-Files Web Service.  This solution does not require the M-Files client to be installed.

# Libraries

More information is available within the Readme file in the Libraries folder.

# Samples

More information is available within the Readme file in the Samples folder.