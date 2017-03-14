# Chain Workflows

This sample shows how to move an object from one workflow to another once it reaches a specific state.  This technique can be used to split complex workflows into smaller, more managable processes for staff.

This sample consists of three broad items:

1. A backup of a sample M-Files vault (compatible with M-Files 2015.3 and upwards), that can be used to demonstrate the supplied code (`Vault Backup.mfb`).
2. An implementation using VBScript (`VBScript.txt`), to be run as a state action script.
3. A C# implementation using the Vault Application Framework (`ChainWorkflows.csproj`).
