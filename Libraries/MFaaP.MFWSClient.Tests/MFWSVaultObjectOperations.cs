using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFilesAPI.Tests.ExtensionMethods;
using MFaaP.MFWSClient.Tests.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectOperations
	{

		#region RenameObject

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RenameObjectAsync(MFaaP.MFWSClient.ObjVer,string,System.Threading.CancellationToken)()"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task RenameObjectAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/1/2/latest/title");

			// Set the expected body.
			var newObjectName = new PrimitiveType<string>()
			{
				Value = "renamed object"
			};
			runner.SetExpectedRequestBody(newObjectName);

			// Execute.
			await runner.MFWSClient.ObjectOperations.RenameObjectAsync(new ObjID()
			{
				Type = 1,
				ID = 2
			}, newObjectName.Value);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RenameObjectAsync(MFaaP.MFWSClient.ObjVer,string,System.Threading.CancellationToken)()"/>
		/// when using unmanaged object information
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task RenameObjectAsync_Unmanaged()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/uhello%3Aworld/uagain/title");

			// Set the expected body.
			var newObjectName = new PrimitiveType<string>()
			{
				Value = "renamed object"
			};
			runner.SetExpectedRequestBody(newObjectName);

			// Execute.
			await runner.MFWSClient.ObjectOperations.RenameObjectAsync(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryName = "hello",
				ExternalRepositoryObjectID = "world",
				ExternalRepositoryObjectVersionID = "again"
			}, newObjectName.Value);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RenameObjectAsync(MFaaP.MFWSClient.ObjVer,string,System.Threading.CancellationToken)()"/>
		/// when using unmanaged object information
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task RenameObjectAsync_Unmanaged_Latest()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/uhello%3Aworld/latest/title");

			// Set the expected body.
			var newObjectName = new PrimitiveType<string>()
			{
				Value = "renamed object"
			};
			runner.SetExpectedRequestBody(newObjectName);

			// Execute.
			await runner.MFWSClient.ObjectOperations.RenameObjectAsync(new ObjID()
			{
				Type = 0,
				ExternalRepositoryName = "hello",
				ExternalRepositoryObjectID = "world"
			}, newObjectName.Value);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RenameObject(MFaaP.MFWSClient.ObjVer,string,System.Threading.CancellationToken)()"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void RenameObject()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/1/2/latest/title");

			// Set the expected body.
			var newObjectName = new PrimitiveType<string>()
			{
				Value = "renamed object"
			};
			runner.SetExpectedRequestBody(newObjectName);

			// Execute.
			runner.MFWSClient.ObjectOperations.RenameObject(new ObjID()
			{
				Type = 1,
				ID = 2
			}, newObjectName.Value);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetLatestObjectVersionAndProperties

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetLatestObjectVersionAndPropertiesAsync(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetLatestObjectVersionAndPropertiesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.GET, "/REST/objects/0/123/latest.aspx?include=properties");

			// Execute.
			await runner.MFWSClient.ObjectOperations.GetLatestObjectVersionAndPropertiesAsync(0, 123);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetLatestObjectVersionAndProperties(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetLatestObjectVersionAndProperties()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.GET, "/REST/objects/0/123/latest.aspx?include=properties");

			// Execute.
			runner.MFWSClient.ObjectOperations.GetLatestObjectVersionAndProperties(0, 123);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetLatestObjectVersionAndPropertiesAsync(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// using an external object's details
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetLatestObjectVersionAndPropertiesAsync_External()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.GET, "/REST/objects/0/uhello%2Bworld%3A123%2525123/latest.aspx?include=properties");

			// Execute.
			await runner.MFWSClient.ObjectOperations.GetLatestObjectVersionAndPropertiesAsync(new ObjID()
			{
				Type = 0,
				ExternalRepositoryName = "hello world", // This will be double-encoded.
				ExternalRepositoryObjectID = "123%123" // This will be double-encoded.
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetLatestObjectVersionAndProperties(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// using an external object's details
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetLatestObjectVersionAndProperties_External()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.GET, "/REST/objects/0/uhello%2Bworld%3A123%2525123/latest.aspx?include=properties");

			// Execute.
			runner.MFWSClient.ObjectOperations.GetLatestObjectVersionAndProperties(new ObjID()
			{
				Type = 0,
				ExternalRepositoryName = "hello world", // This will be double-encoded.
				ExternalRepositoryObjectID = "123%123" // This will be double-encoded.
			});

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Creating new objects

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.CreateNewObject"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task CreateNewObjectAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.CreateNewObjectAsync(0, new ObjectCreationInfo());

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.CreateNewObject"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void CreateNewObject_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.CreateNewObject(0, new ObjectCreationInfo());

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.CreateNewObject"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task CreateNewObjectAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.CreateNewObjectAsync(0, new ObjectCreationInfo());

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.CreateNewObject"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void CreateNewObject_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.CreateNewObject(0, new ObjectCreationInfo());

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		#endregion

		#region GetCheckoutStatus

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatusAsync(ObjVer,System.Threading.CancellationToken)"/>
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public async Task GetCheckoutStatusAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<PrimitiveType<MFCheckOutStatus>>(Method.GET, $"/REST/objects/0/1/2/checkedout");

			// Execute.
			await runner.MFWSClient.ObjectOperations.GetCheckoutStatusAsync(new ObjVer()
			{
				Type = 0,
				ID = 1,
				Version = 2
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatus(ObjVer,System.Threading.CancellationToken)"/>
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void GetCheckoutStatus()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<PrimitiveType<MFCheckOutStatus>>(Method.GET, $"/REST/objects/0/1/2/checkedout");

			// Execute.
			runner.MFWSClient.ObjectOperations.GetCheckoutStatus(new ObjVer()
			{
				Type = 0,
				ID = 1,
				Version = 2
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatusAsync(ObjVer,System.Threading.CancellationToken)"/>
		/// when using unmanaged object details
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public async Task GetCheckoutStatusAsync_External()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<PrimitiveType<MFCheckOutStatus>>(Method.GET, $"/REST/objects/0/umy+repository:hello+world/latest/checkedout");

			// Execute.
			await runner.MFWSClient.ObjectOperations.GetCheckoutStatusAsync(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryObjectID = "hello world",
				ExternalRepositoryName = "my repository"
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatus(ObjVer,System.Threading.CancellationToken)"/>
		/// when using unmanaged object details
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void GetCheckoutStatus_External()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<PrimitiveType<MFCheckOutStatus>>(Method.GET, $"/REST/objects/0/umy+repository:hello+world/latest/checkedout");

			// Execute.
			runner.MFWSClient.ObjectOperations.GetCheckoutStatus(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryObjectID = "hello world",
				ExternalRepositoryName = "my repository"
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatusAsync(ObjVer,System.Threading.CancellationToken)"/>
		/// when using unmanaged object details
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public async Task GetCheckoutStatusAsync_External_WithVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<PrimitiveType<MFCheckOutStatus>>(Method.GET, $"/REST/objects/0/umy+repository:hello+world/myversionid/checkedout");

			// Execute.
			await runner.MFWSClient.ObjectOperations.GetCheckoutStatusAsync(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryObjectID = "hello world",
				ExternalRepositoryName = "my repository",
				ExternalRepositoryObjectVersionID = "myversionid"
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatus(ObjVer,System.Threading.CancellationToken)"/>
		/// when using unmanaged object details
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void GetCheckoutStatus_External_WithVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<PrimitiveType<MFCheckOutStatus>>(Method.GET, $"/REST/objects/0/umy+repository:hello+world/myversionid/checkedout");

			// Execute.
			runner.MFWSClient.ObjectOperations.GetCheckoutStatus(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryObjectID = "hello world",
				ExternalRepositoryName = "my repository",
				ExternalRepositoryObjectVersionID = "myversionid"
			});

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Undo checkout

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.UndoCheckout"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void UndoCheckout_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.UndoCheckout(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2?force=false", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.UndoCheckout"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void UndoCheckout_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;
			string methodParameter = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
					methodParameter = r.Parameters.GetMethodQuerystringParameter();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.UndoCheckout(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.DELETE.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.ForceUndoCheckout"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void ForceUndoCheckout_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.ForceUndoCheckout(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2?force=true", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.ForceUndoCheckout"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void ForceUndoCheckout_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;
			string methodParameter = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
					methodParameter = r.Parameters.GetMethodQuerystringParameter();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.ForceUndoCheckout(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.DELETE.ToString(), methodParameter);
		}

		#endregion

		#region Undelete object

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.UndeleteObject(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void UndeleteObject_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.UndeleteObject(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/deleted", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.UndeleteObject(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void UndeleteObject_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;
			string methodParameter = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
					methodParameter = r.Parameters.GetMethodQuerystringParameter();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.UndeleteObject(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.PUT.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.UndeleteObject(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct request body.
		/// </summary>
		[TestMethod]
		public void UndeleteObject_CorrectRequestBody()
		{
			/* Arrange */

			// The request body.
			PrimitiveType<bool> body = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the body requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					body = r.DeserializeBody<PrimitiveType<bool>>();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.UndeleteObject(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.IsNotNull(body);
			Assert.AreEqual(false, body.Value);
		}

		#endregion

		#region Delete object

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.DeleteObject(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void DeleteObject_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.DeleteObject(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/deleted", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.DeleteObject(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void DeleteObject_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;
			string methodParameter = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
					methodParameter = r.Parameters.GetMethodQuerystringParameter();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.DeleteObject(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.PUT.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.DeleteObject(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct request body.
		/// </summary>
		[TestMethod]
		public void DeleteObject_CorrectRequestBody()
		{
			/* Arrange */

			// The request body.
			PrimitiveType<bool> body = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the body requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					body = r.DeserializeBody<PrimitiveType<bool>>();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.DeleteObject(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.IsNotNull(body);
			Assert.AreEqual(true, body.Value);
		}

		#endregion

		#region Destroy object

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.DestroyObject(MFaaP.MFWSClient.ObjID,bool,int,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void DestroyObject_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.DestroyObject(0, 1, true, -1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/latest?allVersions=true", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.DestroyObject(MFaaP.MFWSClient.ObjID,bool,int,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void DestroyObject_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;
			string methodParameter = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
					methodParameter = r.Parameters.GetMethodQuerystringParameter();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.DestroyObject(0, 1, true, -1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.DELETE.ToString(), methodParameter);
		}

		#endregion

		#region SetCheckoutStatus

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatusAsync(ObjVer,MFaaP.MFWSClient.MFCheckOutStatus,System.Threading.CancellationToken)"/>
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatusAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/1/2/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			await runner.MFWSClient.ObjectOperations.SetCheckoutStatusAsync(new ObjVer()
			{
				Type = 0,
				ID = 1,
				Version = 2
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(ObjVer,MFaaP.MFWSClient.MFCheckOutStatus,System.Threading.CancellationToken)"/>
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/1/2/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			runner.MFWSClient.ObjectOperations.SetCheckoutStatus(new ObjVer()
			{
				Type = 0,
				ID = 1,
				Version = 2
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatusAsync(ObjVer,MFaaP.MFWSClient.MFCheckOutStatus,System.Threading.CancellationToken)"/>
		/// when using an unmanaged object
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatusAsync_External()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/urepository%2Bname%3Amy%2Bobject/uversion%2B1/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			await runner.MFWSClient.ObjectOperations.SetCheckoutStatusAsync(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryName = "repository name",
				ExternalRepositoryObjectID = "my object",
				ExternalRepositoryObjectVersionID = "version 1"
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(ObjVer,MFaaP.MFWSClient.MFCheckOutStatus,System.Threading.CancellationToken)"/>
		/// when using an unmanaged object
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_External()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/urepository%2Bname%3Amy%2Bobject/uversion%2B1/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			runner.MFWSClient.ObjectOperations.SetCheckoutStatus(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryName = "repository name",
				ExternalRepositoryObjectID = "my object",
				ExternalRepositoryObjectVersionID = "version 1"
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(ObjVer,MFaaP.MFWSClient.MFCheckOutStatus,System.Threading.CancellationToken)"/>
		/// when using an unmanaged object
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_External_Latest()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/urepository%2Bname%3Amy%2Bobject/latest/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			runner.MFWSClient.ObjectOperations.SetCheckoutStatus(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryName = "repository name",
				ExternalRepositoryObjectID = "my object"
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatusAsync(ObjID,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// when providing the latest version
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatusAsync_LatestVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/1/latest/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			await runner.MFWSClient.ObjectOperations.SetCheckoutStatusAsync(new ObjID()
			{
				Type = 0,
				ID = 1
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(ObjID,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// when providing the latest version
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_LatestVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/1/latest/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			runner.MFWSClient.ObjectOperations.SetCheckoutStatus(new ObjID()
			{
				Type = 0,
				ID = 1
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(ObjID,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// when providing the latest version
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CheckIn()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/1/latest/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedIn });

			// Execute.
			runner.MFWSClient.ObjectOperations.SetCheckoutStatus(new ObjID()
			{
				Type = 0,
				ID = 1
			}, MFCheckOutStatus.CheckedIn);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(ObjID,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// when providing the latest version
		/// requests the correct resource address using the correct HTTP method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CheckedOutToMe()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, $"/REST/objects/0/1/latest/checkedout");

			// Set the expected body.
			runner.SetExpectedRequestBody(new PrimitiveType<MFCheckOutStatus>() { Value = MFCheckOutStatus.CheckedOutToMe });

			// Execute.
			runner.MFWSClient.ObjectOperations.SetCheckoutStatus(new ObjID()
			{
				Type = 0,
				ID = 1
			}, MFCheckOutStatus.CheckedOutToMe);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetHistory

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetHistory(int,int,System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task GetHistoryAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ObjectVersion>>(Method.GET, $"/REST/objects/0/1/history");

			// Execute.
			await runner.MFWSClient.ObjectOperations.GetHistoryAsync(new ObjID()
			{
				Type = 0,
				ID = 1
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetHistory(int,int,System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void GetHistory()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ObjectVersion>>(Method.GET, $"/REST/objects/0/1/history");

			// Execute.
			runner.MFWSClient.ObjectOperations.GetHistory(new ObjID()
			{
				Type = 0,
				ID = 1
			});

			// Verify.
			runner.Verify();
		}
		
		#endregion

		#region RemoveFromFavorites

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RemoveFromFavoritesAsync(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task RemoveFromFavoritesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.DELETE, $"/REST/favorites/1/2");

			// Execute.
			await runner.MFWSClient.ObjectOperations.RemoveFromFavoritesAsync(new ObjID()
			{
				Type = 1,
				ID = 2
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RemoveFromFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void RemoveFromFavorites()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.DELETE, $"/REST/favorites/1/2");

			// Execute.
			runner.MFWSClient.ObjectOperations.RemoveFromFavorites(new ObjID()
			{
				Type = 1,
				ID = 2
			});

			// Verify.
			runner.Verify();
		}

		#endregion

		#region AddToFavorites

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavoritesAsync(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task AddToFavoritesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.POST, $"/REST/favorites");

			// Set the expected body.
			var objId = new ObjID()
			{
				Type = 0,
				ID = 1
			};
			runner.SetExpectedRequestBody(objId);

			// Execute.
			await runner.MFWSClient.ObjectOperations.AddToFavoritesAsync(objId);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void AddToFavorites()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.POST, $"/REST/favorites");

			// Set the expected body.
			var objId = new ObjID()
			{
				Type = 0,
				ID = 1
			};
			runner.SetExpectedRequestBody(objId);

			// Execute.
			runner.MFWSClient.ObjectOperations.AddToFavorites(objId);

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
