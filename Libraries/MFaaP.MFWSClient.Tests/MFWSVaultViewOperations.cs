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

		#region GetRootViewContents

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetRootFolderContents"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetRootViewContentsAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetRootFolderContentsAsync();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetRootFolderContents"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetRootViewContents_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ViewOperations.GetRootFolderContents();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetRootFolderContents"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetRootViewContentsAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetRootFolderContentsAsync();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetRootFolderContents"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void GetRootViewContents_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ViewOperations.GetRootFolderContents();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region GetFolderContents

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public void GetViewContents_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ViewOperations.GetFolderContents();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_WithView()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 15,
					Name = "Favourites"
				}
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v15/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public void GetViewContents_CorrectResource_WithView()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ViewOperations.GetFolderContents(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 15,
					Name = "Favourites"
				}
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v15/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_WithView_OneGrouping()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
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

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/L4/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public void GetViewContents_CorrectResource_WithView_OneGrouping()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ViewOperations.GetFolderContents(new FolderContentItem()
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

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/L4/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_WithView_TwoGroupings()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
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

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/L4/L19/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public void GetViewContents_CorrectResource_WithView_TwoGroupings()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ViewOperations.GetFolderContents(new FolderContentItem()
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

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/L4/L19/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// uses the correct Http method (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 15,
					Name = "Favourites"
				}
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// uses the correct Http method (no path).
		/// </summary>
		[TestMethod]
		public void GetViewContents_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ViewOperations.GetFolderContents(new FolderContentItem()
			{
				FolderContentItemType = MFFolderContentItemType.ViewFolder,
				View = new View()
				{
					ID = 15,
					Name = "Favourites"
				}
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_WithView_OneGrouping_UrlEncoded()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
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

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/TIntelligent+Metadata+Layer/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_WithView_OneGrouping_UrlEncoded_WithSlashes()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync(new FolderContentItem()
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

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/TOne%2FTwo+Options/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_ViewPathMustEndWithSlash()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync("v123");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v123/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultViewOperations.GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContentsAsync_CorrectResource_ViewPathMustNotStartWithSlash()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<FolderContentItems>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new FolderContentItems());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ViewOperations.GetFolderContentsAsync("/v123/");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v123/items", resourceAddress);
		}

		#endregion

	}
}
