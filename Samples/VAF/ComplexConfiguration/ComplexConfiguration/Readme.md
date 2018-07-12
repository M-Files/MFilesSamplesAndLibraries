# Vault Application Framework 2.0 (Complex) integration to the M-Files Admin Configuration section

This sample shows a number of ways of exposing Vault Application Framework configuration to the M-Files Admin area.

Specifically:

* Configuration editors:
  * Allowing users to choose colours.
  * Allowing users to select dates, times, and timestamps.
  * Allowing users to select [vault structural elements](https://developer.m-files.com/Frameworks/Vault-Application-Framework/Attributes/Configuration/#mfidentifier) (e.g. property definitions or classes).
  * Allowing users to select numeric values (integers and floating-point values).
  * Allowing users to enter "placeholder text", [which can have values replaced into it at runtime](https://developer.m-files.com/Frameworks/Vault-Application-Framework/Helpers/ObjVerEx/#expandplaceholdertext).
  * Allowing users to select values from preconfigured lists (e.g. enumerations).
  * Allowing users to choose their own search conditions, similarly to the M-Files Desktop client.
  * Allowing users to enter text values, with regular expression validation.
* Lists:
  * Allowing users to manage (add to, update, and remove from) lists of simple data types (e.g. strings).
  * Allowing users to manage (add to, update, and remove from) lists of [vault structural elements](https://developer.m-files.com/Frameworks/Vault-Application-Framework/Attributes/Configuration/#mfidentifier).
  * Allowing users to manage (add to, update, and remove from) lists of [POCOs (Plain Old CLR Objects)](https://en.wikipedia.org/wiki/Plain_old_CLR_object).
* Altering the visible configuration options based on selected values:
  * Showing configuration options based on other values.
  * Hiding configuration options based on other values.