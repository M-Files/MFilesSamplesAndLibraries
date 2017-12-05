﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
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
	/// <typeparam name="T"></typeparam>
	public class RestApiTestRunner<T>
		where T : class, new()
	{

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

		/// <summary>
		/// The fully-mocked <see cref="MFaaP.MFWSClient.MFWSClient"/> for interacting with the vault.
		/// Execute your tests against this.
		/// </summary>
		public MFaaP.MFWSClient.MFWSClient MFWSClient { get; private set; }

		/// <summary>
		/// A reference to the <see cref="RestApiTestAttribute"/> used to describe the expected
		/// endpoint behaviour.
		/// </summary>
		public RestApiTestAttribute RestApiTestAttribute { get; private set; }

		/// <summary>
		/// Gets or sets the response data which the endpoint will return.
		/// Defaults to a new instance of <see cref="T"/>.
		/// </summary>
		public T ResponseData { get; set; }
		= new T();

		public ISerializer JsonSerializer { get; set; }
		= new RestSharp.Serializers.JsonSerializer();

		public ISerializer XmlSerializer { get; set; }
		= new RestSharp.Serializers.XmlSerializer();

		public RestApiTestRunner()
		{
			this.VerifyTasks.Add(c => c.ExecuteTaskAsync<T>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
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
			var parameter =  new Parameter()
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
						parameter.Value = serializer.Serialize(body);
					}
					break;
				case DataFormat.Xml:
					serializer = this.XmlSerializer;
					serializer.Namespace = xmlNamespace;
					parameter.ContentType = serializer.ContentType;
					if (null != body)
					{
						parameter.Value = serializer.Serialize(body);
					}
					break;
				default:
					throw new ArgumentException("Data format not expected.");
			}

			// Set up the expected request body parameter.
			this.RestApiTestAttribute.ExpectedRequestBody = parameter;

		}

		/// <summary>
		/// Verifies that the expressions described in <see cref="VerifyTasks"/>
		/// all pass.
		/// </summary>
		public void Verify()
		{
			// Iterate over the tasks and check they were called the correct number of times.
			foreach(var task in this.VerifyTasks)
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
			Assert.AreEqual(
				this.RestApiTestAttribute.ExpectedMethod,
				r.Method,
				$"HTTP method {this.RestApiTestAttribute.ExpectedMethod} expected, but {r.Method} was executed.");

			// Ensure the correct resource was requested.
			Assert.AreEqual(
				this.RestApiTestAttribute.ExpectedResourceAddress,
				r.Resource,
				$"Resource {this.RestApiTestAttribute.ExpectedResourceAddress} expected, but a {r.Resource} was used.");

			// Ensure the data format is correct (if supplied).
			if (this.RestApiTestAttribute.ExpectedRequestFormat.HasValue)
			{
				Assert.AreEqual(
					this.RestApiTestAttribute.ExpectedRequestFormat.Value,
					r.RequestFormat);
			}

			// Was a request body expected?
			if (null != this.RestApiTestAttribute.ExpectedRequestBody)
			{
				// Get the body from the request.
				var requestBody = (r.Parameters ?? new List<Parameter>())
					.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
				if(null == requestBody)
					Assert.Fail("A request body was expected but none was provided.");

				// Ensure the content type is correct.
				Assert.AreEqual(
					this.RestApiTestAttribute.ExpectedRequestBody.Name,
					requestBody.ContentType);

				// Ensure that the value is correct.
				Assert.AreEqual(
					this.RestApiTestAttribute.ExpectedRequestBody.Value,
					requestBody.Value);
			}
		}

		/// <summary>
		/// Defines the return value
		/// </summary>
		/// <returns></returns>
		public virtual Task<IRestResponse<T>> HandleReturns(IRestRequest r)
		{
			// Create the mock response.
			var response = new Mock<IRestResponse<T>>();

			// Setup the return data.
			response.SetupGet(re => re.Data)
				.Returns(this.ResponseData);
			response.SetupGet(re => re.StatusCode)
				.Returns(this.RestApiTestAttribute.ResponseStatusCode);
			response.SetupGet(re => re.ResponseUri)
				.Returns(new Uri(r.Resource, UriKind.Relative));

			//Return the mock object.
			return Task.FromResult(response.Object);

		}

		public virtual void Setup(TestContext testContext)
		{
			// Get the RestApiTest attribute on the current test method.
			this.RestApiTestAttribute = Type
				.GetType(testContext.FullyQualifiedTestClassName)
				.GetMethod(testContext.TestName)
				.GetCustomAttribute<RestApiTestAttribute>();
			if (null == this.RestApiTestAttribute)
				throw new InvalidOperationException("No RestApiTestAttribute was found.");

			// Set up the mock object.
			this.RestClientMock = new Mock<IRestClient>();
			this.RestClientMock
				.Setup(c => c.ExecuteTaskAsync<T>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					this.HandleCallback(r);
				})
				.Returns((IRestRequest r, CancellationToken t) => this.HandleReturns(r));

			// Create our MFWSClient.
			this.MFWSClient = MFaaP.MFWSClient.Tests.MFWSClient.GetMFWSClient(this.RestClientMock);
		}
	}
}