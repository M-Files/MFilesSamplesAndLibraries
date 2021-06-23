using COMAPI.ExtensionMethods;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace COMAPI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MFilesServerApplication serverApplication { get; }
			= new MFilesServerApplication();
		private PluginInfo oAuthPluginInfo { get; set; }

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Connect_Click(object sender, RoutedEventArgs e)
		{
			// Hide stuff from the UI that we don't need.
			this.webBrowser.Visibility = Visibility.Hidden;
			this.vaultContents.Visibility = Visibility.Hidden;
			this.vaultContents.Items.Clear();

			try
			{
				// Attempt to get the OAuth details.
				var pluginInfoCollection = this.serverApplication.GetAuthenticationPluginInformationEx
				(
					// We need to specify what to connect to, and how.
					ProtocolSequence: this.connectionDetails.ProtocolSequence,
					NetworkAddress: this.connectionDetails.NetworkAddress,
					Endpoint: this.connectionDetails.Endpoint,
					EncryptedConnection: this.connectionDetails.EncryptedConnection,

					// In some configurations you can provide the host name and no vault GUID and get back the 
					// authentication plugin details.  If this does not work, though, then you must
					// know the vault GUID and pass it as part of this call.
					HostName: this.connectionDetails.NetworkAddress,
					VaultGUID: this.connectionDetails.VaultGuid,

					// The authentication stuff can be empty/defaults.
					UserName: this.connectionDetails.UserName,
					Domain: this.connectionDetails.Domain,
					AccountType: MFLoginAccountType.MFLoginAccountTypeWindows,
					TargetPluginName: string.Empty
				);
				if (0 == pluginInfoCollection.Count)
				{
					MessageBox.Show("No authentication plugins configured");
					return;
				}
				this.oAuthPluginInfo = pluginInfoCollection
					.Cast<PluginInfo>()
					.FirstOrDefault(info => info.IsOAuthPlugin());
				if (null == this.oAuthPluginInfo)
				{
					MessageBox.Show("OAuth is not configured on the vault/server.");
					return;
				}

				// Navigate to the OAuth screen.
				var state = Guid.NewGuid().ToString("B");
				this.oAuthPluginInfo.Configuration["state"] = state;
				this.webBrowser.Navigate($"{this.oAuthPluginInfo.GenerateAuthorizationUri(state)}");

				// Show the web browser.
				this.webBrowser.Visibility = Visibility.Visible;

			}
			catch (Exception ex)
			{
				MessageBox.Show($"Exception obtaining authentication plugin data: {ex}");
			}
		}

		private async void webBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
		{
			// Sanity.
			if (null == this.oAuthPluginInfo)
				return;

			// If it's going somewhere else then fine.
			if (!e.Uri.ToString().StartsWith(this.oAuthPluginInfo.GetAppropriateRedirectUri()))
				return;

			// We need to handle it.
			e.Cancel = true;
			this.webBrowser.Visibility = Visibility.Hidden;

			// Parse the tokens.
			var tokens = await this.ProcessRedirectUri(e.Uri);

			// Connect to the vault.
			var authenticationData = new NamedValues();
			authenticationData["Token"] = tokens.AccessToken;
			this.serverApplication.ConnectWithAuthenticationDataEx6
			(
				new ConnectionData()
				{
					AllowUsingAuthenticationPlugins = true,
					ProtocolSequence = this.connectionDetails.ProtocolSequence,
					NetworkAddress = this.connectionDetails.NetworkAddress,
					Endpoint = this.connectionDetails.Endpoint,
					EncryptedConnection = this.connectionDetails.EncryptedConnection
				},
				this.oAuthPluginInfo,
				authenticationData
			);

			// Get a vault that the user can see (this should allow them to choose).
			var vaults = this.serverApplication.GetOnlineVaults();
			if (0 == vaults.Count)
				throw new InvalidOperationException("User cannot access any vaults");
			var vault = this.serverApplication.LogInToVault(vaults.Cast<VaultOnServer>().First().GUID);

			// Get everything in the root view.
			foreach (var item in vault.ViewOperations.GetFolderContents(new FolderDefs()).Cast<FolderContentItem>())
			{
				var treeViewItem = new TreeViewItem();
				switch (item.FolderContentItemType)
				{
					case MFFolderContentItemType.MFFolderContentItemTypeViewFolder:
						treeViewItem.Header = item.View.Name;
						break;
					default:
						continue; // This should also handle other types of content.
				}
				this.vaultContents.Items.Add(treeViewItem);
			}
			this.vaultContents.Visibility = Visibility.Visible;
		}

		private async Task<OAuth2TokenResponse> ProcessRedirectUri(Uri redirectUri)
		{
			// Does this represent an error?
			var queryParams = new UriBuilder(redirectUri).GetQueryParamsDictionary();
			if (queryParams.ContainsKey("error"))
			{
				throw new InvalidOperationException
				(
					$"Exception {queryParams["error"]} returned by authorisation endpoint."
				);
			}

			// Check that the state was correct (not tampered with).
			if (this.oAuthPluginInfo.Configuration["state"]?.ToString() != queryParams["state"])
			{
				throw new InvalidOperationException
				(
					"The state returned by the authorisation endpoint was not correct."
				);
			}

			// Retrieve the authorisation code from the URI.
			var code = queryParams.ContainsKey("code") ? queryParams["code"] : null;

			// Convert the authorisation code to tokens.

			// Create the request, adding the mandatory items.
			var tokenEndpoint = new Uri(this.oAuthPluginInfo.GetTokenEndpoint(), uriKind: UriKind.Absolute);
			var request = new RestSharp.RestRequest(tokenEndpoint.PathAndQuery, RestSharp.Method.POST);
			request.AddParameter("code", code);
			request.AddParameter("grant_type", "authorization_code");
			request.AddParameter("redirect_uri", this.oAuthPluginInfo.GetAppropriateRedirectUri());

			// Add the client id.  If there's a realm then use that here too.
			{
				var siteRealm = this.oAuthPluginInfo.GetSiteRealm();
				var clientId = this.oAuthPluginInfo.GetClientID();
				request.AddParameter
				(
					"client_id",
					string.IsNullOrWhiteSpace(siteRealm)
						? clientId // If no site realm is supplied, just pass the client ID.
						: $"{clientId}@{siteRealm}" // Otherwise pass client ID @ site realm.
				);
			}

			// Add the optional bits.
			request
				.AddParameterIfNotNullOrWhitespace("resource", this.oAuthPluginInfo.GetResource())
				.AddParameterIfNotNullOrWhitespace("scope", this.oAuthPluginInfo.GetScope())
				.AddParameterIfNotNullOrWhitespace("client_secret", this.oAuthPluginInfo.GetClientSecret());

			// Execute the HTTP request.
			var restClient = new RestSharp.RestClient(tokenEndpoint.GetLeftPart(UriPartial.Authority));
			var response = await restClient.ExecutePostAsync<OAuth2TokenResponse>(request);

			// Validate response.
			if (null == response.Data)
				throw new InvalidOperationException("OAuth token not received from endpoint. Response: " + response.Content);
			else if (response.Data.TokenType != "Bearer")
				throw new InvalidOperationException("Token type was not bearer. Response: " + response.Content);

			// Return the access token data.
			return response.Data;
		}
	}
}
