using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Serializers;

namespace MFaaP.MFWSClient.Tests
{
	/// <summary>
	/// A runner for REST API tests.
	/// </summary>
	/// <remarks>
	/// <see cref="RestApiTestRunner{T}"/> should be used in preference tot his base class.
	/// This class is used when interacting with endpoints that do not operate as REST-style endpoints (e.g. authentication).
	/// </remarks>
	public class RestApiTestRunner
	{
		private readonly object _lock = new object();
		private MFaaP.MFWSClient.MFWSClient mfwsClient;

		/// <summary>
		/// A collection of tasks that will be passed to <see cref="Mock{T}.Verify(System.Linq.Expressions.Expression{System.Action{T}})"/>
		/// to verify that the required methods were called on the <see cref="RestClientMock"/>.
		/// </summary>
		/// <remarks>By default it will check that <see cref="IRestClient.ExecuteTaskAsync{T}(RestSharp.IRestRequest,System.Threading.CancellationToken)"/> is called exactly once.</remarks>
		public Dictionary<Expression<Action<IRestClient>>, Moq.Times> VerifyTasks { get; private set; }
		= new Dictionary<Expression<Action<IRestClient>>, Moq.Times>();

		/// <summary>
		/// The mock of <see cref="IRestClient"/>.
		/// </summary>
		public Mock<IRestClient> RestClientMock { get; private set; }
			= new Mock<IRestClient>();

		/// <summary>
		/// The fully-mocked <see cref="MFaaP.MFWSClient.MFWSClient"/> for interacting with the vault.
		/// Execute your tests against this.
		/// </summary>
		public MFaaP.MFWSClient.MFWSClient MFWSClient
		{
			get
			{
				if (null == this.mfwsClient)
				{
					lock (this._lock)
					{
						if (null == this.mfwsClient)
						{
							this.mfwsClient = Tests.MFWSClient.GetMFWSClient(this.RestClientMock);
						}
					}
				}
				return this.mfwsClient;
			}
			protected set { this.mfwsClient = value; }
		}
		/// <summary>
		/// The HTTP method expected.
		/// </summary>
		public RestSharp.Method ExpectedMethod { get; set; }

		/// <summary>
		/// The resource that should be requested.
		/// </summary>
		public string ExpectedResourceAddress { get; set; }

		/// <summary>
		/// The expected request format (used for formatting the body, if required).
		/// </summary>
		public DataFormat ExpectedRequestFormat { get; set; }
		= DataFormat.Json;

		/// <summary>
		/// The HTTP response code to provide.
		/// </summary>
		public HttpStatusCode ResponseStatusCode { get; set; }
			= HttpStatusCode.OK;
		
		/// <summary>
		/// Gets or sets the response data which the endpoint will return.
		/// Defaults to null string.
		/// </summary>
		public virtual string ResponseData { get; set; }
			= null;
		/// <summary>
		/// The request body which is expected to be passed.
		/// </summary>
		public Parameter ExpectedRequestBody { get; set; }

		/// <summary>
		/// The extensions that must be specified in the <see cref="MFWSClientBase.ExtensionsHttpHeaderName"/>
		/// HTTP header.
		/// </summary>
		public MFWSExtensions ExpectedMFWSExtensions{ get; set; }
			= MFWSExtensions.None;

		/// <summary>
		/// The serialiser to use for Json encoding (should be the same as set up within
		/// RestSharp, so that the encoded bodies can be compared).
		/// </summary>
		public ISerializer JsonSerializer { get; set; }
		= new RestSharp.Serializers.JsonSerializer();

		/// <summary>
		/// The serialiser to use for Xml encoding (should be the same as set up within
		/// RestSharp, so that the encoded bodies can be compared).
		/// </summary>
		public ISerializer XmlSerializer { get; set; }
		= new RestSharp.Serializers.XmlSerializer();

		/// <summary>
		/// Creates a <see cref="RestApiTestRunner"/> with the supplied information.
		/// </summary>
		/// <param name="expectedMethod">The HTTP method that this endpoint is expected to return for this test.</param>
		/// <param name="expectedResourceAddress">
		/// The resource that this point should request.  
		/// <remarks>Should be of the format "/REST/objects...".</remarks>
		/// </param>
		public RestApiTestRunner(RestSharp.Method expectedMethod,
			string expectedResourceAddress)
		{
			// Update object members.
			this.ExpectedMethod = expectedMethod;
			this.ExpectedResourceAddress = expectedResourceAddress;

			// Add our basic verification task in.
			this.VerifyTasks.Add(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Set up our mock.
			this.RestClientMock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					this.HandleCallback(r);
				})
				.Returns((IRestRequest r, CancellationToken t) => this.HandleReturns(r));
		}

		/// <summary>
		/// Sets the expected request body.
		/// </summary>
		/// <param name="body">The body to set.</param>
		/// <param name="dataFormat"></param>
		/// <param name="xmlNamespace"></param>
		public void SetExpectedRequestBody(object body, DataFormat dataFormat = DataFormat.Json, string xmlNamespace = "")
		{
			// Set up the parameter.
			var parameter = new Parameter()
			{
				Type = ParameterType.RequestBody
			};

			// Fill in the parameter data based on the data format.
			ISerializer serializer;
			switch (dataFormat)
			{
				case DataFormat.Json:
					serializer = this.JsonSerializer;
					parameter.ContentType = serializer.ContentType;
					if (null != body)
					{
						parameter.Value = body is string
							? body
							: serializer.Serialize(body);
					}

					break;
				case DataFormat.Xml:
					serializer = this.XmlSerializer;
					serializer.Namespace = xmlNamespace;
					parameter.ContentType = serializer.ContentType;
					if (null != body)
					{
						parameter.Value = body is string
							? body
							: serializer.Serialize(body);
					}

					break;
				default:
					throw new ArgumentException("Data format not expected.");
			}

			// Set up the expected request body parameter.
			this.ExpectedRequestBody = parameter;

		}

		/// <summary>
		/// Verifies that the expressions described in <see cref="VerifyTasks"/>
		/// all pass.
		/// </summary>
		public void Verify()
		{
			// Iterate over the tasks and check they were called the correct number of times.
			foreach (var task in this.VerifyTasks)
				this.RestClientMock.Verify(task.Key, task.Value);
		}

		/// <summary>
		/// Handles the callback when a request has been "executed".
		/// </summary>
		/// <param name="r">The request that was made.</param>
		/// <returns>A task which should verify that the request was correctly structured.</returns>
		protected virtual void HandleCallback(IRestRequest r)
		{
			// Ensure the HTTP method is correct.
			var expectedMethod = this.ExpectedMethod;
			switch (this.ExpectedMethod)
			{
				case Method.GET:
				case Method.POST:
					// Get and post are fine, but others need to be routed correctly.
					break;
				default:
					// Others should use a "POST" method and should have the appropriate _method querystring parameter.
					expectedMethod = Method.POST;

					// Retrieve the method parameter and ensure that it has the original HTTP method.
					var methodParameter = r.Parameters
						.FirstOrDefault(p => p.Type == ParameterType.QueryString && p.Name == "_method");
					Assert.IsNotNull(methodParameter, "A method parameter was not found on the request.");
					Assert.AreEqual(methodParameter.Value,
						this.ExpectedMethod.ToString(),
						$"The {this.ExpectedMethod} should be routed through a HTTP POST, with the original method available in a querystring parameter named _method.");

					break;
			}

			// Check the HTTP method is as expected.
			Assert.AreEqual(
				expectedMethod,
				r.Method,
				$"HTTP method {expectedMethod} expected, but {r.Method} was executed.");

			// Ensure the correct resource was requested.
			Assert.AreEqual(
				this.ExpectedResourceAddress,
				r.Resource,
				$"Resource {this.ExpectedResourceAddress} expected, but {r.Resource} was used.");

			// Ensure the data format is correct.
			Assert.AreEqual(
				this.ExpectedRequestFormat,
				r.RequestFormat);

			// Was a request body expected?
			if (null != this.ExpectedRequestBody)
			{
				// Get the body from the request.
				var requestBody = (r.Parameters ?? new List<Parameter>())
					.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
				if (null == requestBody)
					Assert.Fail("A request body was expected but none was provided.");

				// Ensure the content type is correct.
				Assert.AreEqual(
					this.ExpectedRequestBody.Name,
					requestBody.ContentType);

				// Ensure that the value is correct.
				Assert.AreEqual(
					this.ExpectedRequestBody.Value,
					requestBody.Value);
			}

			// Were specific MFWS extensions expected?
			if (MFWSExtensions.None != this.ExpectedMFWSExtensions)
			{

				// Ensure that the "X-Extensions" header is set.
				var extensionsHeader = r
					.Parameters?
					.FirstOrDefault(dp => dp.Name == "X-Extensions" && dp.Type == ParameterType.HttpHeader);
				Assert.IsNotNull(extensionsHeader, $"The {MFWSClientBase.ExtensionsHttpHeaderName} HTTP header was not found on the request.");

				// Extract the registered extensions.
				var extensionsValues = ((extensionsHeader.Value as string) ?? "").Split(",".ToCharArray());

				// Ensure that the ones we want are added.
				foreach (var possibleExtension in Enum.GetValues(typeof(MFWSExtensions)).Cast<MFWSExtensions>())
				{
					// Ignore "none".
					if (possibleExtension == MFWSExtensions.None)
						continue;

					// Have we enabled this extension?
					if (false == this.ExpectedMFWSExtensions.HasFlag(possibleExtension))
						continue;

					// If it is missing then fail.
					if (false == extensionsValues.Contains(possibleExtension.ToString()))
					{
						Assert.Fail(
							$"{possibleExtension.ToString()} is not in the {MFWSClientBase.ExtensionsHttpHeaderName} HTTP header.");
					}
				}
			}
		}

		/// <summary>
		/// Defines the return value
		/// </summary>
		/// <returns></returns>
		public virtual Task<IRestResponse> HandleReturns(IRestRequest r)
		{
			// Create the mock response.
			var response = new Mock<IRestResponse>();

			// Setup the return data.
			response.SetupGet(re => re.Content)
				.Returns(this.ResponseData);
			response.SetupGet(re => re.StatusCode)
				.Returns(this.ResponseStatusCode);
			response.SetupGet(re => re.ResponseUri)
				.Returns(new Uri(r.Resource, UriKind.Relative));

			//Return the mock object.
			return Task.FromResult(response.Object);

		}
	}

	/// <summary>
	/// A runner for REST API tests.
	/// </summary>
	/// <typeparam name="T">The response type provided by this endpoint.</typeparam>
	public class RestApiTestRunner<T>
		: RestApiTestRunner
		where T : class, new()
	{
		/// <summary>
		/// Gets or sets the response data which the endpoint will return.
		/// Defaults to a new instance of <see cref="T"/>.
		/// </summary>
		public new T ResponseData { get; set; }
			= new T();

		/// <inheritdoc />
		public RestApiTestRunner(RestSharp.Method expectedMethod,
			string expectedResourceAddress)
			: base(expectedMethod, expectedResourceAddress)
		{
			// Remove the non-generic verification task.
			this.VerifyTasks.Clear();
			this.VerifyTasks.Add(c => c.ExecuteTaskAsync<T>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.AtLeast(1));

			// Set up the mock for the generic ExecuteTaskAsync method.
			this.RestClientMock
				.Setup(c => c.ExecuteTaskAsync<T>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					this.HandleCallback(r);
				})
				.Returns((IRestRequest r, CancellationToken t) => this.HandleReturns(r));
		}

		/// <inheritdoc />
		public new Task<IRestResponse<T>> HandleReturns(IRestRequest r)
		{
			// Create the mock response.
			var response = new Mock<IRestResponse<T>>();

			// Setup the return data.
			response.SetupGet(re => re.Data)
				.Returns(this.ResponseData);
			response.SetupGet(re => re.StatusCode)
				.Returns(this.ResponseStatusCode);
			response.SetupGet(re => re.ResponseUri)
				.Returns(new Uri(r.Resource, UriKind.Relative));

			//Return the mock object.
			return Task.FromResult(response.Object);

		}
	}
}