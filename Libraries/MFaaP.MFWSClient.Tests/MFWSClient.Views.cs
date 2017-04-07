using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	public partial class MFWSClient
	{

		#region GetRootViewContents

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetRootViewContents"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetRootViewContents_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetRootViewContents();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetRootViewContents"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetRootViewContents_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetRootViewContents();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region GetViewContents

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetViewContents"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContents_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetViewContents();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetViewContents"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContents_CorrectResource_WithView()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetViewContents(new FolderContentItem()
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
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v15/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetViewContents"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContents_CorrectResource_WithView_OneGrouping()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetViewContents(new FolderContentItem()
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
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/L4/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetViewContents"/>
		/// requests the correct resource address (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContents_CorrectResource_WithView_TwoGroupings()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetViewContents(new FolderContentItem()
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
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/views/v216/L4/L19/items", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetViewContents"/>
		/// uses the correct Http method (no path).
		/// </summary>
		[TestMethod]
		public async Task GetViewContents_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetViewContents(new FolderContentItem()
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
			mock.Verify(c => c.ExecuteTaskAsync<FolderContentItems>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

	}
}
