using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultViewOperations
	{

		#region Path encoding for external view folders.
		
		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetRootFolderContents"/>
		/// correctly encodes paths for external view folders.
		/// </summary>
		[TestMethod]
		public async Task GetExternalViewFolderAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/umyrepository%3A12%2B3456/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ExternalViewFolder,
				ExternalView = new ExternalView()
				{
					ExternalRepositoryName = "myrepository",
					ID = "12 3456" // NOTE: This will be double-encoded (" " to "+", then to "%2B").
				}
			});

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetRootViewContents

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetRootFolderContents"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task GetRootViewContentsAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetRootFolderContentsAsync();

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetRootFolderContents"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void GetRootViewContents()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/items");

			// Execute.
			runner.MFWSClient.ViewOperations.GetRootFolderContents();

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetFolderContents

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync();

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void GetViewContents()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/items");

			// Execute.
			runner.MFWSClient.ViewOperations.GetFolderContents();

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with a view).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_WithView()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v15/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 15,
					Name = "Favourites"
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with a view).
		/// </summary>
		[TestMethod]
		public void GetViewContents_WithView()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v15/items");

			// Execute.
			runner.MFWSClient.ViewOperations.GetFolderContents(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 15,
					Name = "Favourites"
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with view and lookup grouping).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_WithView_OneGrouping()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v216/L4/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 216,
					Name = "By Technology"
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Lookup,
					Lookup = new Lookup()
					{
						Item = 4
					}
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with view and lookup grouping).
		/// </summary>
		[TestMethod]
		public void GetViewContents_WithView_OneGrouping()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v216/L4/items");

			// Execute.
			runner.MFWSClient.ViewOperations.GetFolderContents(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 216,
					Name = "By Technology"
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Lookup,
					Lookup = new Lookup()
					{
						Item = 4
					}
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with view and two lookup groupings).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_WithView_TwoGroupings()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v216/L4/L19/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 216,
					Name = "By Technology"
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Lookup,
					Lookup = new Lookup()
					{
						Item = 4
					}
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Lookup,
					Lookup = new Lookup()
					{
						Item = 19
					}
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with view and two lookup groupings).
		/// </summary>
		[TestMethod]
		public void GetViewContents_WithView_TwoGroupings()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v216/L4/L19/items");

			// Execute.
			runner.MFWSClient.ViewOperations.GetFolderContents(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 216,
					Name = "By Technology"
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Lookup,
					Lookup = new Lookup()
					{
						Item = 4
					}
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Lookup,
					Lookup = new Lookup()
					{
						Item = 19
					}
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with view and text grouping, testing url encoding of property data).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_WithView_OneGrouping_UrlEncoded()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v216/TIntelligent+Metadata+Layer/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 216,
					Name = "By Technology"
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "Intelligent Metadata Layer"
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (with view and text grouping, testing url encoding of property data).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_WithView_OneGrouping_UrlEncoded_WithSlashes()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v216/TOne%2FTwo+Options/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 216,
					Name = "By Technology"
				}
			}, new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.PropertyFolder,
				PropertyFolder = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "One/Two Options"
				}
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (view path must be formatted correctly if the path does not end with a slash).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_ViewPathMustEndWithSlash()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v123/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync("v123");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address and HTTP method (view path must be formatted correctly if the path starts with a slash).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_ViewPathMustNotStartWithSlash()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<FolderContentItems>(Method.GET, $"/REST/views/v123/items");

			// Execute.
			await runner.MFWSClient.ViewOperations.GetFolderContentsAsync("/v123/");

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
