# Segmented search

By default, the [SearchForObjectsByConditions](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations~SearchForObjectsByConditions.html) method on [VaultObjectSearchOperations](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations.html) will limit the number of items it returns to 500.  An extended [SearchForObjectsByConditionsEx](https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectSearchOperations~SearchForObjectsByConditionsEx.html) method allows the caller to both specify a larger maximum count and longer timeout, but may still return limited results in some situations.  This example introduces an approach that can bypass this limitation and iterate over all content that matches the search conditions.

## Firstly: the downsides

Searching over this volume of content can cause significant load on the M-Files server.  Whilst this code will happily iterate many hundreds of thousands of items, doing so may not be advisable in production scenarios.

Additionally, this approach takes significantly more time than executing a single search.  It may be useful to execute this code in scenarios where the user does not have to wait for the results.  In some ad-hoc testing against a large sample vault, searching this way took approximately twice the duration of searching using the standard methods.

## The approach

All objects within M-Files have an internal numeric "Object Id".  This object Id is normally shown in the top-left of the metadata card and is the Id which is used both internally and via the APIs to refer to individual objects.  In this approach we will "segment" the search call into multiple smaller requests, each only returning a small number of results.

Our code will look something like this:

* Assign a constant named `itemsPerSegment` to 1000.
* Assign a variable named `segment` to zero.
* Execute the following until we reach the end of the objects <sup>1</sup>:
  * Take a copy of the search conditions (`internalSearchConditions`).
  * Add an additional condition, forcing it to search for objects with ID between `segment * itemsPerSegment` and `(segment + 1) * itemsPerSegment`.
  * Execute the segment search, and return the results.

*Note that the number of items in each segment may not be 1000, even if there are no other search conditions restricting the results.  In situations where objects are permanently destroyed, the segment itself may only contain a small number of objects.  This does not mean that the last objects have been reached.*

[1] In order to find whether we have any more objects to return, we have to execute an additional search:

* Take a copy of the search conditions (`internalSearchConditions`).
* Add an additional condition, forcing it to search for items with a minimum ID greater than the current segment.
* Execute the search, only returning a maximum of one item; we only need to know if there is at least one item, not all of the items.

## The code

The code  (`Program.cs`) contains two implementations of the search code:

1. The `UseLibrary` method uses the M-Files API Helper Library to execute the various searches.  This library wraps some complexities of connecting/disconnecting from the vault, and creation of the SearchCondition objects.  The purpose of this code is to show the specific approach as cleanly as possible.
2. The `UseAPI` method uses the M-Files API directly.  This method shows the specific code required to use this approach directly against the API.  It contains more boiler-plate code but is useful both for learning, and for situations where the M-Files API Helper Library cannot be used.

