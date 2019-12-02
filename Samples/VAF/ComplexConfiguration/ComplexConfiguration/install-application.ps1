exit

# Application details
$appFilePath = "bin\Debug\ComplexConfiguration.mfappx"
$appGuid = "d5ff5720-00ac-4a5d-b361-77472bf449a1"

# Target vault
$vaultName = "Sample Vault"

# Connection details 
$authType = 1 # 1 = MFAuthTypeLoggedOnWindowsUser
$userName = ""
$password = ""
$domain = ""
$spn = ""
$protocolSequence = "ncacn_ip_tcp"
$networkAddress = "localhost"
$endpoint = 2266
$encryptedConnection = $false
$localComputerName = ""

Write-Host "Connecting to Vault..."

# Load M-Files API
$null = [System.Reflection.Assembly]::LoadWithPartialName("Interop.MFilesAPI")

# Connect to M-Files Server
$server = new-object MFilesAPI.MFilesServerApplicationClass
$tzi = new-object MFilesAPI.TimeZoneInformationClass
$tzi.LoadWithCurrentTimeZone()
$null = $server.ConnectAdministrativeEx( $tzi, $authType, $userName, $password, $domain, $spn, $protocolSequence, $networkAddress, $endpoint, $encryptedConnection, $localComputerName )

# Get the target vault
$vaultOnServer = $server.GetOnlineVaults().GetVaultByName( $vaultName )

# Login to vault
$vault = $vaultOnServer.LogIn()

# Try to uninstall existing application
try
{
	Write-Host "Checking for previous installation of the application..."

	# Uninstall
	$vault.CustomApplicationManagementOperations.UninstallCustomApplication( $appGuid );
	
	Write-Host "Restarting after uninstall..."
	
	# Restart vault. The installation seems to fail, if the vault is not restarted after uninstall.
	$server.VaultManagementOperations.TakeVaultOffline( $vaultOnServer.GUID, $true )
	$server.VaultManagementOperations.BringVaultOnline( $vaultOnServer.GUID )
		
	# Login to vault again.
	$vault = $vaultOnServer.LogIn()
}
catch {}

Write-Host "Installing application..."

# Install application. The vault should not have the application installed at this point.
$vault.CustomApplicationManagementOperations.InstallCustomApplication( $appFilePath )

Write-Host "Restarting after install..."

# Restart vault
$server.VaultManagementOperations.TakeVaultOffline( $vaultOnServer.GUID, $true )
$server.VaultManagementOperations.BringVaultOnline( $vaultOnServer.GUID )
