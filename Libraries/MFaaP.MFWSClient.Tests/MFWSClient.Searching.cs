using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	public partial class MFWSClient
	{

		#region QuickSearch

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.QuickSearch"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task QuickSearch_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.QuickSearch("hello world");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.QuickSearch"/>
		/// requests the correct resource address when used with an object type.
		/// </summary>
		[TestMethod]
		public async Task QuickSearch_CorrectResource_WithObjectType()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.QuickSearch("hello world", 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world&o=0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.QuickSearch"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task QuickSearch_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.QuickSearch("hello world");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.QuickSearch"/>
		/// uses the correct Http method when used with an object type.
		/// </summary>
		[TestMethod]
		public async Task QuickSearch_CorrectMethod_WithObjectType()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.QuickSearch("hello world", 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region Search

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task Search_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new QuickSearchCondition("hello world"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region QuickSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_QuickSearch_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new QuickSearchCondition("hello world"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?q=hello+world", resourceAddress);
		}

		#endregion

		#region ObjectTypeSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_ObjectTypeSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new ObjectTypeSearchCondition(123));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?o=123", resourceAddress);
		}

		#endregion

		#region IncludeDeletedObjectsSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_IncludeDeletedObjectsSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new IncludeDeletedObjectsSearchCondition());

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?d=include", resourceAddress);
		}

		#endregion

		#region BooleanPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_BooleanPropertyValueSearchCondition_CorrectResource_True()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new BooleanPropertyValueSearchCondition(123, true));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=true", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_BooleanPropertyValueSearchCondition_CorrectResource_False()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new BooleanPropertyValueSearchCondition(123, false));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=false", resourceAddress);
		}

		#endregion

		#region DatePropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_DatePropertyValueSearchCondition_CorrectResource_Equals()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01)));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_DatePropertyValueSearchCondition_CorrectResource_LessThan()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.LessThan));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123<<=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_DatePropertyValueSearchCondition_CorrectResource_LessThanOrEqual()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.LessThanOrEqual));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123<=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_DatePropertyValueSearchCondition_CorrectResource_GreaterThan()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.GreaterThan));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123>>=2017-01-01", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_DatePropertyValueSearchCondition_CorrectResource_GreaterThanOrEqual()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new DatePropertyValueSearchCondition(123, new DateTime(2017, 01, 01), SearchConditionOperators.GreaterThanOrEqual));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123>=2017-01-01", resourceAddress);
		}

		#endregion

		#region LookupPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_LookupPropertyValueSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new LookupPropertyValueSearchCondition(123, lookupIds: 456));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=456", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_LookupPropertyValueSearchCondition_CorrectResource_ExternalLookup()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new LookupPropertyValueSearchCondition(123, externalLookupIds: "456"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=e456", resourceAddress);
		}

		#endregion

		#region MultiSelectLookupPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_MultiSelectLookupPropertyValueSearchCondition_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new MultiSelectLookupPropertyValueSearchCondition(123, lookupIds: new [] {  456, 789 }));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=456%2C789", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_MultiSelectLookupPropertyValueSearchCondition_CorrectResource_ExternalLookup()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new MultiSelectLookupPropertyValueSearchCondition(123, externalLookupIds: new[] {  "456", "789" }));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=e456%2Ce789", resourceAddress);
		}

		#endregion

		#region TextPropertyValueSearchCondition

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_TextPropertyValueSearchCondition_CorrectResource_Equals()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new TextPropertyValueSearchCondition(123, "hello"));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123=hello", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_TextPropertyValueSearchCondition_CorrectResource_MatchesWildcard()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new TextPropertyValueSearchCondition(123, "hello*", SearchConditionOperators.MatchesWildcard));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123**=hello*", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_TextPropertyValueSearchCondition_CorrectResource_Contains()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new TextPropertyValueSearchCondition(123, "hello", SearchConditionOperators.Contains));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123*=hello", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.Search"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task Search_TextPropertyValueSearchCondition_CorrectResource_StartsWith()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.Search(new TextPropertyValueSearchCondition(123, "hello", SearchConditionOperators.StartsWith));

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<Results<ObjectVersion>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects?p123^=hello", resourceAddress);
		}

		#endregion

	}
}
