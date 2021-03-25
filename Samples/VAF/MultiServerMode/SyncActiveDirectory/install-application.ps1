param(

    [Parameter(Mandatory=$False)]
    [string[]] $InstallGroups=@()

)

# Load M-Files API.
$null = [System.Reflection.Assembly]::LoadWithPartialName("Interop.MFilesAPI")

# Application details
$appFilePath = "bin\Debug\SyncActiveDirectory.mfappx"
$appGuid = "11d6179b-ce0d-49a8-8638-dccbe2406ccf"

# Target vault
$vaultName = "Sample Vault"

# Connection details 
$authType = [MFilesAPI.MFAuthType]::MFAuthTypeLoggedOnWindowsUser
$userName = ""
$password = ""
$domain = ""
$spn = ""
$protocolSequence = "ncacn_ip_tcp"
$networkAddress = "localhost"
$endpoint = 2266
$encryptedConnection = $false
$localComputerName = ""

# Default to reporting an error if the script fails.
$ErrorActionPreference = 'Stop'

# Append the current path so we have the full location (required in some situations).
$currentDir = Get-Location
$appFilePath = Join-Path $currentDir $appFilePath

# If we are using install-application.user.json then parse the vault connections.
$doesInstallApplicationUserFileExist = $false
$vaultConnections = @()
try {
    Write-Output "  Parsing install-application.user.json (if available)..."
    $jsonInput = Get-Content -Path install-application.user.json | ConvertFrom-Json
    if($InstallGroups.Count -eq 0) {
        Write-Output "    No install groups given, proceeding with default."
        $InstallGroups += "default"
    }
    ForEach($jsonConnection in $jsonInput.VaultConnections) {
        $doesInstallApplicationUserFileExist = $true
    
        ForEach($ig in $InstallGroups) {
            # if no groups, include to default
            if(-not $jsonConnection.installGroups) {
                if($InstallGroups -contains "default") {
                    $vaultConnections += $jsonConnection
                    Write-Output "    Will deploy to $($jsonConnection.vaultName) on $($jsonConnection.networkAddress), as the declaration has no groups defined (included in default)."
                }
                break; # The connection has no groups, no further looping is needed
            }
            if($jsonConnection.InstallGroups -contains $ig) {
                $vaultConnections += $jsonConnection
                Write-Output "    Will deploy to $($jsonConnection.vaultName) on $($jsonConnection.networkAddress), as the declaration has group $($ig) defined."
                break;
            }
        }
    }
}
catch [System.IO.FileNotFoundException], [System.Management.Automation.ItemNotFoundException]
{
    # This is fine; the user is probably not using this approach for deployment.
    Write-Output "    install-application.user.json file not found; using data from install-application.ps1."
}
catch {
    # Write the exception out and throw so that we stop execution.
    Write-Error -Exception $error[0].Exception
    throw;
}
finally
{
    $error.clear();
}
# If we are not using an external JSON file
# then use the connection/authentication information defined at the top of the file.
if(-not $doesInstallApplicationUserFileExist) {
    $vaultConnections += [PSCustomObject]@{
        vaultName = $vaultName
        authType = $authType
        userName = $userName
        password = $password
        domain = $domain
        spn = $spn
        protocolSequence = $protocolSequence
        networkAddress = $networkAddress
        endpoint = $endpoint
        encryptedConnection = $encryptedConnection
        localComputerName = $localComputerName
    }
}
                                                                              
ForEach($vc in $vaultConnections) {
    # Connect to M-Files Server.
    Write-Host "  Connecting to '$($vc.vaultName)' on $($vc.networkAddress)..."
    $server = new-object MFilesAPI.MFilesServerApplicationClass
    $tzi = new-object MFilesAPI.TimeZoneInformationClass
    $tzi.LoadWithCurrentTimeZone()
    $null = $server.ConnectAdministrativeEx( $tzi, $vc.authType, $vc.userName, $vc.password, $vc.domain, $vc.spn, $vc.protocolSequence, $vc.networkAddress, $vc.endpoint, $vc.encryptedConnection, $vc.localComputerName )
    # Get the target vault.
    $vaultOnServer = $server.GetOnlineVaults().GetVaultByName( $vc.vaultName )
    # Login to vault.
    $vault = $vaultOnServer.LogIn()
    # Install application.
    Write-Host "    Installing application..."
    try {
        $vault.CustomApplicationManagementOperations.InstallCustomApplication( $appFilePath )
        # Restart vault.
        Write-Host "    Restarting vault..."
        $server.VaultManagementOperations.TakeVaultOffline( $vaultOnServer.GUID, $true )
        $server.VaultManagementOperations.BringVaultOnline( $vaultOnServer.GUID )
        # Short sleep to prevent SQL errors on logging in.
        Start-Sleep -Milliseconds 500
        # Login to vault again.
        $vault = $vaultOnServer.LogIn()
    } catch {
        # Already exists
        if($_.Exception -Match "0x80040031") {
            Write-Host "    This application version already exists on the vault, installation skipped"
        }
        else {
            throw
        }
    }
}