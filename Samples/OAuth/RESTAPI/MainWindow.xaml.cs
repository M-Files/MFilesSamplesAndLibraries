using MFaaP.MFWSClient;
using RESTAPI.ExtensionMethods;
using Common.ExtensionMethods;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
using Common;
using System.Net;

namespace RESTAPI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// The RestSharp client to use to make HTTP requests.
		/// </summary>
		private RestClient client { get; set; }

		/// <summary>
		/// Details about the authentication plugin used.
		/// Note: this structure comes from the MFWSStructs.cs file and broadly mimics the COM API structures.
		/// See: https://github.com/M-Files/Libraries.MFWSClient/blob/da22e931a34f13fe3cb35c692ea9fe7645fc0c20/MFaaP.MFWSClient/MFWSStructs.cs
		/// </summary>
		private PluginInfoConfiguration oAuthPluginInfo { get; set; }


		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Connects to the vault.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Connect_Click(object sender, RoutedEventArgs e)
		{
			// Hide stuff from the UI that we don't need.
			this.webBrowser.Visibility = Visibility.Hidden;
			this.vaultContents.Visibility = Visibility.Hidden;
			this.vaultContents.Items.Clear();

			// Attempt to parse the network address.
			if (false == Uri.TryCreate(this.connectionDetails.NetworkAddress, UriKind.Absolute, out Uri baseUri))
			{
				MessageBox.Show($"Cannot parse {this.connectionDetails.NetworkAddress} as a valid network address.");
				return;
			}

			try
			{
				// Set up the RestSharp client.
				// Note: the base url should be of the form "https://m-files.mycompany.com".
				this.client = new RestClient(baseUri);

				// Attempt to get the OAuth details.
				List<PluginInfoConfiguration> pluginInfoCollection = null;
				{
					// Get all the plugin details (there may be multiple).
					var response = this.client.Execute<List<PluginInfoConfiguration>>(new RestRequest("/REST/server/authenticationprotocols.aspx", Method.GET));
					pluginInfoCollection = response.Data;

					// Save the response cookies, for MSM compatibility.
					this.client.CookieContainer = this.client.CookieContainer ?? new System.Net.CookieContainer();
					if (null != response.Cookies)
						foreach (var cookie in response.Cookies)
							this.client.CookieContainer.Add(baseUri, new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
				}
				if (0 == pluginInfoCollection.Count)
				{
					MessageBox.Show("No authentication plugins configured");
					return;
				}

				// Try and get the OAuth-specific plugin.
				this.oAuthPluginInfo = pluginInfoCollection
					.FirstOrDefault(info => info.IsOAuthPlugin());
				if (null == this.oAuthPluginInfo)
				{
					MessageBox.Show("OAuth is not configured on the vault/server.");
					return;
				}

				// Navigate to the authorisation screen.
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

		/// <summary>
		/// Reacts when the web browser navigates due to interaction with the
		/// provider.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void webBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
		{
			// Sanity.
			if (null == this.oAuthPluginInfo)
				return;

			// We only want to react if it is being redirected to thte redirect Uri.
			if (!e.Uri.ToString().StartsWith(this.oAuthPluginInfo.GetAppropriateRedirectUri()))
				return;

			// We need to handle it.
			e.Cancel = true;
			this.webBrowser.Visibility = Visibility.Hidden;

			// Parse the tokens.
			var tokens = await this.ProcessRedirectUri(e.Uri);

			// Add the auth token to the default headers.
			// Note: we need to add both the Authorization and X-Vault headers or it won't work.
			this.client.AddDefaultHeader("Authorization", "Bearer " + tokens.AccessToken);
			this.client.AddDefaultHeader("X-Vault", this.oAuthPluginInfo.VaultGuid);

			// Show and populate the (root) tree view.
			this.vaultContents.Visibility = Visibility.Visible;
			var items = this.vaultContents.Items;
			var rootItem = new TreeViewItem() { Header = "Vault contents" };
			items.Add(rootItem);

			this.ExpandTreeViewItem(rootItem);
		}

		private void ExpandTreeViewItem(TreeViewItem parent, string folder = null)
		{
			// Sanity.
			if (null == parent)
				return;
			folder = folder ?? "";

			// Remove anything including the "loading" item.
			parent.Items.Clear();

			this.Dispatcher.BeginInvoke(new Action(() =>
			{
				if (false == folder.EndsWith("/"))
					folder = folder + "/";

				// Get the items within the selected view.
				var response = this.client.Get<FolderContentItems>(new RestRequest($"/REST/views{folder}items"));

				// Get everything in the passed location.
				foreach (var item in response.Data.Items)
				{
					// Get the folder defs to use for this node.
					var tag = folder;

					// Create the tree view item depending on the type of item we've got.
					// Note: a lot of this is taken from https://github.com/M-Files/Libraries.MFWSClient/blob/da22e931a34f13fe3cb35c692ea9fe7645fc0c20/MFaaP.MFWSClient/ExtensionMethods/FolderContentItemExtensionMethods.cs#L73.
					var treeViewItem = new TreeViewItem();
					switch (item.FolderContentItemType)
					{
						// Render views.
						case MFFolderContentItemType.ViewFolder:
							{
								// Set up the folder def to go "into" this view.
								tag += "v" + item.View.ID;

								// Set the header to the view name.
								treeViewItem.Header = item.View.Name;
							}
							break;
						// Render property groups.
						case MFFolderContentItemType.PropertyFolder:
							{
								// Set up the folder def to go "into" this grouping.
								string prefix = null;
								string suffix = item.PropertyFolder.Value?.ToString();
								switch (item.PropertyFolder.DataType)
								{
									case MFDataType.Text:
										prefix = "T";
										break;
									case MFDataType.MultiLineText:
										prefix = "M";
										break;
									case MFDataType.Integer:
										prefix = "I";
										break;
									case MFDataType.Integer64:
										prefix = "J";
										break;
									case MFDataType.Floating:
										prefix = "R";
										break;
									case MFDataType.Date:
										prefix = "D";
										break;
									case MFDataType.Time:
										prefix = "C";
										break;
									case MFDataType.FILETIME:
										prefix = "E";
										break;
									case MFDataType.Lookup:
										prefix = "L";
										suffix = (item.PropertyFolder.Lookup?.Item ?? 0).ToString();
										break;
									case MFDataType.MultiSelectLookup:
										prefix = "S";
										suffix = String.Join(",", item.PropertyFolder.Lookups?.Select(l => l.Item) ?? new int[0]);
										break;
									case MFDataType.Uninitialized:
										prefix = "-";
										break;
									case MFDataType.ACL:
										prefix = "A";
										break;
									case MFDataType.Boolean:
										prefix = "B";
										break;
								}

								// Return the formatted value.
								tag += $"{prefix}{WebUtility.UrlEncode(suffix)}";

								// Set the header to the grouping name.
								treeViewItem.Header = item.PropertyFolder.DisplayValue;
							}
							break;
						default:
							// We should also handle other types of content, but this will do for now.
							continue;
					}
					if (null == treeViewItem.Header)
						continue;

					// Set up the tag.
					treeViewItem.Tag = tag;

					// Add the item to the list.
					treeViewItem.Items.Add(new TreeViewItem() { Header = "Loading...", IsEnabled = false });
					treeViewItem.Expanded += TreeViewItem_Expanded;
					parent.Items.Add(treeViewItem);
				}
			}));
		}

		private void ExpandTreeViewItem(TreeViewItem treeViewItem)
		{
			if (null == treeViewItem)
				return;
			this.ExpandTreeViewItem(treeViewItem, treeViewItem.Tag as string);
		}

		private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
		{
			this.ExpandTreeViewItem(sender as TreeViewItem);
			e.Handled = true;
		}

		/// <summary>
		/// Extracts data from the <paramref name="redirectUri"/> and uses it to retrieve access
		/// and refresh tokens from the provider.
		/// </summary>
		/// <param name="redirectUri">The Uri redirected to by the provider.</param>
		/// <returns>The token response, if available.</returns>
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
