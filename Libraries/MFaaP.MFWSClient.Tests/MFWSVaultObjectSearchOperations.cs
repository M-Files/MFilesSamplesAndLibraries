using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectSearchOperations
	{

		#region SearchForObjectsByString

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByStringAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByStringAsync("hello world");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByString_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByString("hello world");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// requests the correct resource address when used with an object type.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByStringAsync_CorrectResource_WithObjectType()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByStringAsync("hello world", 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world&o=0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// requests the correct resource address when used with an object type.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByString_CorrectResource_WithObjectType()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByString("hello world", 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world&o=0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByStringAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByStringAsync("hello world");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByString_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByString("hello world");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// uses the correct Http method when used with an object type.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByStringAsync_CorrectMethod_WithObjectType()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByStringAsync("hello world", 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByString"/>
		/// uses the correct Http method when used with an object type.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByString_CorrectMethod_WithObjectType()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByString("hello world", 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region Search

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new QuickSearchCondition("hello world"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new QuickSearchCondition("hello world"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region QuickSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_QuickSearch_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new QuickSearchCondition("hello world"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_QuickSearch_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new QuickSearchCondition("hello world"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world", resourceAddress);
		}

		#endregion

		#region ObjectTypeSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_ObjectTypeSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new ObjectTypeSearchCondition(123));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?o=123", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_ObjectTypeSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new ObjectTypeSearchCondition(123));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?o=123", resourceAddress);
		}

		#endregion

		#region IncludeDeletedObjectsSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditionsAsync(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_IncludeDeletedObjectsSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new IncludeDeletedObjectsSearchCondition());

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?d=include", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditionsAsync(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_IncludeDeletedObjectsSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new IncludeDeletedObjectsSearchCondition());

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?d=include", resourceAddress);
		}

		#endregion

		#region BooleanPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_BooleanPropertyValueSearchCondition_CorrectResource_True()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new BooleanPropertyValueSearchCondition(123, true));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=true", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_BooleanPropertyValueSearchCondition_CorrectResource_True()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new BooleanPropertyValueSearchCondition(123, true));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=true", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_BooleanPropertyValueSearchCondition_CorrectResource_False()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new BooleanPropertyValueSearchCondition(123, false));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=false", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_BooleanPropertyValueSearchCondition_CorrectResource_False()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new BooleanPropertyValueSearchCondition(123, false));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=false", resourceAddress);
		}

		#endregion

		#region DatePropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_DatePropertyValueSearchCondition_CorrectResource_Equals()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01)));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_DatePropertyValueSearchCondition_CorrectResource_Equals()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01)));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_DatePropertyValueSearchCondition_CorrectResource_LessThan()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.LessThan));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123<<=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_DatePropertyValueSearchCondition_CorrectResource_LessThan()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.LessThan));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123<<=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_DatePropertyValueSearchCondition_CorrectResource_LessThanOrEqual()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.LessThanOrEqual));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123<=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_DatePropertyValueSearchCondition_CorrectResource_LessThanOrEqual()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.LessThanOrEqual));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123<=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_DatePropertyValueSearchCondition_CorrectResource_GreaterThan()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.GreaterThan));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123>>=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_DatePropertyValueSearchCondition_CorrectResource_GreaterThan()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.GreaterThan));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123>>=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_DatePropertyValueSearchCondition_CorrectResource_GreaterThanOrEqual()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.GreaterThanOrEqual));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123>=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_DatePropertyValueSearchCondition_CorrectResource_GreaterThanOrEqual()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.GreaterThanOrEqual));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123>=2017-01-01", resourceAddress);
		}

		#endregion

		#region LookupPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_LookupPropertyValueSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new LookupPropertyValueSearchCondition(123, lookupIds: 456));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=456", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_LookupPropertyValueSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new LookupPropertyValueSearchCondition(123, lookupIds: 456));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=456", resourceAddress);
		}

		#endregion

		#region MultiSelectLookupPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_MultiSelectLookupPropertyValueSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new MultiSelectLookupPropertyValueSearchCondition(123, lookupIds: new[] { 456, 789 }));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=456%2C789", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_MultiSelectLookupPropertyValueSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new MultiSelectLookupPropertyValueSearchCondition(123, lookupIds: new[] { 456, 789 }));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=456%2C789", resourceAddress);
		}

		#endregion

		#region TextPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_TextPropertyValueSearchCondition_CorrectResource_Equals()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new TextPropertyValueSearchCondition(123, "hello"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=hello", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_TextPropertyValueSearchCondition_CorrectResource_Equals()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new TextPropertyValueSearchCondition(123, "hello"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=hello", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_TextPropertyValueSearchCondition_CorrectResource_MatchesWildcard()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new TextPropertyValueSearchCondition(123, "hello*", SearchConditionOperators.MatchesWildcard));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123**=hello*", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_TextPropertyValueSearchCondition_CorrectResource_MatchesWildcard()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new TextPropertyValueSearchCondition(123, "hello*", SearchConditionOperators.MatchesWildcard));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123**=hello*", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_TextPropertyValueSearchCondition_CorrectResource_Contains()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new TextPropertyValueSearchCondition(123, "hello", SearchConditionOperators.Contains));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123*=hello", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_TextPropertyValueSearchCondition_CorrectResource_Contains()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new TextPropertyValueSearchCondition(123, "hello", SearchConditionOperators.Contains));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123*=hello", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_TextPropertyValueSearchCondition_CorrectResource_StartsWith()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new TextPropertyValueSearchCondition(123, "hello", SearchConditionOperators.StartsWith));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123^=hello", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditions_TextPropertyValueSearchCondition_CorrectResource_StartsWith()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new TextPropertyValueSearchCondition(123, "hello", SearchConditionOperators.StartsWith));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123^=hello", resourceAddress);
		}

		#endregion
		
		#region ValueListSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SearchForObjectsByConditionsAsync_ValueList_CorrectResource_InternalId()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectSearchOperations.SearchForObjectsByConditionsAsync(new ValueListSearchCondition(123, 123));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?vl123=123", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectSearchOperations.SearchForObjectsByConditions(MFaaP.MFWSClient.ISearchCondition[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SearchForObjectsByConditionsAsync_ValueList_CorrectResource_ExternalId()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<Results<ObjectVersion>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new Results<ObjectVersion>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectSearchOperations.SearchForObjectsByConditions(new ValueListSearchCondition(123, "hello"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?vl123=ehello", resourceAddress);
		}

		#endregion

	}
}
