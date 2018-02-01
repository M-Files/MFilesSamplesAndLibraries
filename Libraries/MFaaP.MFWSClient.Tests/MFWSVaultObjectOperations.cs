using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFilesAPI.Tests.ExtensionMethods;
using MFaaP.MFWSClient.Tests.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectOperations
	{

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
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatus(int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetCheckoutStatusAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<MFCheckOutStatus>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<MFCheckOutStatus>()
						{
							Value = MFCheckOutStatus.CheckedOutToMe
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.GetCheckoutStatusAsync(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatus(int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetCheckoutStatus_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<MFCheckOutStatus>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<MFCheckOutStatus>()
						{
							Value = MFCheckOutStatus.CheckedOutToMe
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.GetCheckoutStatus(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatus(int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetCheckoutStatusAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<MFCheckOutStatus>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<MFCheckOutStatus>()
						{
							Value = MFCheckOutStatus.CheckedOutToMe
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.GetCheckoutStatusAsync(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetCheckoutStatus(int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void GetCheckoutStatus_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<MFCheckOutStatus>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<MFCheckOutStatus>()
						{
							Value = MFCheckOutStatus.CheckedOutToMe
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.GetCheckoutStatus(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
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

		#region Delete object

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
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatusAsync_CorrectResource()
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
			await mfwsClient.ObjectOperations.SetCheckoutStatusAsync(0, 1, MFCheckOutStatus.CheckedOutToMe, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CorrectResource()
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
			mfwsClient.ObjectOperations.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatusAsync_CorrectResource_LatestVersion()
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
			await mfwsClient.ObjectOperations.SetCheckoutStatusAsync(0, 1, MFCheckOutStatus.CheckedOutToMe);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/latest/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CorrectResource_LatestVersion()
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
			mfwsClient.ObjectOperations.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/latest/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatusAsync_CorrectMethod()
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
			await mfwsClient.ObjectOperations.SetCheckoutStatusAsync(0, 1, MFCheckOutStatus.CheckedOutToMe, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.PUT.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CorrectMethod()
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
			mfwsClient.ObjectOperations.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.PUT.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatusAsync_CorrectMethod_LatestVersion()
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
			await mfwsClient.ObjectOperations.SetCheckoutStatusAsync(0, 1, MFCheckOutStatus.CheckedOutToMe);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.PUT.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CorrectMethod_LatestVersion()
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
			mfwsClient.ObjectOperations.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.PUT.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct request body.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CorrectRequestBody_CheckedIn()
		{
			/* Arrange */

			// The request body.
			PrimitiveType<MFCheckOutStatus> body = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the body requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					body = r.DeserializeBody<PrimitiveType<MFCheckOutStatus>>();
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
			mfwsClient.ObjectOperations.SetCheckoutStatus(1, 2, MFCheckOutStatus.CheckedIn);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.IsNotNull(body);
			Assert.AreEqual(MFCheckOutStatus.CheckedIn, body.Value);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct request body.
		/// </summary>
		[TestMethod]
		public void SetCheckoutStatus_CorrectRequestBody_CheckedOutToMe()
		{
			/* Arrange */

			// The request body.
			PrimitiveType<MFCheckOutStatus> body = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the body requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					body = r.DeserializeBody<PrimitiveType<MFCheckOutStatus>>();
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
			mfwsClient.ObjectOperations.SetCheckoutStatus(1, 2, MFCheckOutStatus.CheckedOutToMe);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.IsNotNull(body);
			Assert.AreEqual(MFCheckOutStatus.CheckedOutToMe, body.Value);
		}

		#endregion

		#region GetHistory

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetHistory(int,int,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetHistoryAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.GetHistoryAsync(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/history", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetHistory(int,int,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetHistory_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.GetHistory(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/history", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetHistory(int,int,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetHistoryAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.GetHistoryAsync(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetHistory(int,int,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void GetHistory_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.GetHistory(0, 1);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region RemoveFromFavorites

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RemoveFromFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task RemoveFromFavoritesAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.RemoveFromFavoritesAsync(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/favorites/1/2", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RemoveFromFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void RemoveFromFavorites_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.RemoveFromFavorites(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/favorites/1/2", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RemoveFromFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task RemoveFromFavoritesAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;
			string methodParameter = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
					methodParameter = r.Parameters.GetMethodQuerystringParameter();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.RemoveFromFavoritesAsync(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.DELETE.ToString(), methodParameter);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.RemoveFromFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void RemoveFromFavorites_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;
			string methodParameter = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
					methodParameter = r.Parameters.GetMethodQuerystringParameter();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.RemoveFromFavorites(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
			Assert.AreEqual(Method.DELETE.ToString(), methodParameter);
		}

		#endregion

		#region AddToFavorites

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task AddToFavoritesAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.AddToFavoritesAsync(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/favorites", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void AddToFavorites_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.AddToFavorites(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/favorites", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task AddToFavoritesAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.AddToFavoritesAsync(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void AddToFavorites_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.AddToFavorites(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct request body.
		/// </summary>
		[TestMethod]
		public async Task AddToFavoritesAsync_CorrectRequestBody()
		{
			/* Arrange */

			// The request body.
			ObjID objId = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the body requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					objId = r.DeserializeBody<ObjID>();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectOperations.AddToFavoritesAsync(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.IsNotNull(objId);
			Assert.AreEqual(1, objId.Type);
			Assert.AreEqual(2, objId.ID);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.AddToFavorites(MFaaP.MFWSClient.ObjID,System.Threading.CancellationToken)"/>
		/// uses the correct request body.
		/// </summary>
		[TestMethod]
		public void AddToFavorites_CorrectRequestBody()
		{
			/* Arrange */

			// The request body.
			ObjID objId = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the body requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					objId = r.DeserializeBody<ObjID>();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectOperations.AddToFavorites(1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.IsNotNull(objId);
			Assert.AreEqual(1, objId.Type);
			Assert.AreEqual(2, objId.ID);
		}

		#endregion

	}
}
