using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultExternalObjectOperations
	{

		#region Demoting objects

		#region Demoting single objects

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.DemoteObjectAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task DemoteObjectAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/demotemultiobjects");

			// Create the expected body.
			var body = new[]
			{
				new ObjID()
				{
					Type = 0,
					ID = 123
				}
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ExternalObjectOperations.DemoteObjectAsync(body[0]);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.DemoteObject"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void DemoteObject()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/demotemultiobjects");

			// Create the expected body.
			var body = new[]
			{
				new ObjID()
				{
					Type = 0,
					ID = 123
				}
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ExternalObjectOperations.DemoteObject(body[0]);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Demoting multiple objects

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.DemoteObjectsAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task DemoteObjectsAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/demotemultiobjects");

			// Create the expected body.
			var body = new[]
			{
				new ObjID()
				{
					Type = 0,
					ID = 123
				},
				new ObjID()
				{
					Type = 0,
					ID = 456
				}
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ExternalObjectOperations.DemoteObjectsAsync(objectsToDemote: body);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.DemoteObjects"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void DemoteObjects()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/demotemultiobjects");

			// Create the expected body.
			var body = new[]
			{
				new ObjID()
				{
					Type = 0,
					ID = 123
				},
				new ObjID()
				{
					Type = 0,
					ID = 456
				}
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ExternalObjectOperations.DemoteObjects(objectsToDemote: body);

			// Verify.
			runner.Verify();
		}

		#endregion

		#endregion

		#region Promoting objects

		#region Promoting single objects

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.PromoteObjectAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task PromoteObjectAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/setmultipleobjproperties");

			// Create the expected body.
			var body = new ObjectsUpdateInfo()
			{
				MultipleObjectInfo = new[]
				{
					new ObjectVersionUpdateInformation()
					{
						ObjVer = new ObjVer()
						{
							ExternalRepositoryName = "hello world",
							ExternalRepositoryObjectID = "my object id",
							ExternalRepositoryObjectVersionID = "version",
							Type = 0
						},
						Properties = new List<PropertyValue>
						{
							new PropertyValue()
							{
								PropertyDef = (int) MFBuiltInPropertyDef.MFBuiltInPropertyDefClass,
								TypedValue = new TypedValue()
								{
									Lookup = new Lookup()
									{
										Item = (int) MFBuiltInDocumentClass.MFBuiltInDocumentClassOtherDocument
									}
								}
							}
						}
					}
				}.ToList()
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Set the return value.
			runner.ResponseData = new List<ExtendedObjectVersion>()
			{
				new ExtendedObjectVersion()
				{
					ObjVer = new ObjVer()
					{
						Type = 0,
						ID = 987
					}
				}
			};

			// Execute.
			await runner.MFWSClient.ExternalObjectOperations.PromoteObjectAsync(
			body.MultipleObjectInfo[0].ObjVer,
			body.MultipleObjectInfo[0].Properties.ToArray());

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.PromoteObject"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void PromoteObject()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/setmultipleobjproperties");

			// Create the expected body.
			var body = new ObjectsUpdateInfo()
			{
				MultipleObjectInfo = new[]
				{
					new ObjectVersionUpdateInformation()
					{
						ObjVer = new ObjVer()
						{
							ExternalRepositoryName = "hello world",
							ExternalRepositoryObjectID = "my object id",
							ExternalRepositoryObjectVersionID = "version",
							Type = 0
						},
						Properties = new List<PropertyValue>
						{
							new PropertyValue()
							{
								PropertyDef = (int) MFBuiltInPropertyDef.MFBuiltInPropertyDefClass,
								TypedValue = new TypedValue()
								{
									Lookup = new Lookup()
									{
										Item = (int) MFBuiltInDocumentClass.MFBuiltInDocumentClassOtherDocument
									}
								}
							}
						}
					}
				}.ToList()
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Set the return value.
			runner.ResponseData = new List<ExtendedObjectVersion>()
			{
				new ExtendedObjectVersion()
				{
					ObjVer = new ObjVer()
					{
						Type = 0,
						ID = 987
					}
				}
			};

			// Execute.
			runner.MFWSClient.ExternalObjectOperations.PromoteObject(
				body.MultipleObjectInfo[0].ObjVer,
				body.MultipleObjectInfo[0].Properties.ToArray());

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Promoting multiple objects

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.PromoteObjectsAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task PromoteObjectsAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/setmultipleobjproperties");

			// Create the expected body.
			var body = new ObjectsUpdateInfo()
			{
				MultipleObjectInfo = new[]
				{
					new ObjectVersionUpdateInformation()
					{
						ObjVer = new ObjVer()
						{
							ExternalRepositoryName = "hello world",
							ExternalRepositoryObjectID = "my object id",
							ExternalRepositoryObjectVersionID = "version",
							Type = 0
						},
						Properties = new List<PropertyValue>
						{
							new PropertyValue()
							{
								PropertyDef = (int) MFBuiltInPropertyDef.MFBuiltInPropertyDefClass,
								TypedValue = new TypedValue()
								{
									Lookup = new Lookup()
									{
										Item = (int) MFBuiltInDocumentClass.MFBuiltInDocumentClassOtherDocument
									}
								}
							}
						}
					},
					new ObjectVersionUpdateInformation()
					{
						ObjVer = new ObjVer()
						{
							ExternalRepositoryName = "hello world",
							ExternalRepositoryObjectID = "my object id 2",
							ExternalRepositoryObjectVersionID = "version 2",
							Type = 0
						},
						Properties = new List<PropertyValue>
						{
							new PropertyValue()
							{
								PropertyDef = (int) MFBuiltInPropertyDef.MFBuiltInPropertyDefClass,
								TypedValue = new TypedValue()
								{
									Lookup = new Lookup()
									{
										Item = (int) MFBuiltInDocumentClass.MFBuiltInDocumentClassOtherDocument
									}
								}
							}
						}
					}
				}.ToList()
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ExternalObjectOperations.PromoteObjectsAsync(
				objectVersionUpdateInformation: body.MultipleObjectInfo.ToArray());

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExternalObjectOperations.PromoteObjects"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void PromoteObjects()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectVersion>>(Method.PUT, "/REST/objects/setmultipleobjproperties");

			// Create the expected body.
			var body = new ObjectsUpdateInfo()
			{
				MultipleObjectInfo = new[]
				{
					new ObjectVersionUpdateInformation()
					{
						ObjVer = new ObjVer()
						{
							ExternalRepositoryName = "hello world",
							ExternalRepositoryObjectID = "my object id",
							ExternalRepositoryObjectVersionID = "version",
							Type = 0
						},
						Properties = new List<PropertyValue>
						{
							new PropertyValue()
							{
								PropertyDef = (int) MFBuiltInPropertyDef.MFBuiltInPropertyDefClass,
								TypedValue = new TypedValue()
								{
									Lookup = new Lookup()
									{
										Item = (int) MFBuiltInDocumentClass.MFBuiltInDocumentClassOtherDocument
									}
								}
							}
						}
					},
					new ObjectVersionUpdateInformation()
					{
						ObjVer = new ObjVer()
						{
							ExternalRepositoryName = "hello world",
							ExternalRepositoryObjectID = "my object id 2",
							ExternalRepositoryObjectVersionID = "version 2",
							Type = 0
						},
						Properties = new List<PropertyValue>
						{
							new PropertyValue()
							{
								PropertyDef = (int) MFBuiltInPropertyDef.MFBuiltInPropertyDefClass,
								TypedValue = new TypedValue()
								{
									Lookup = new Lookup()
									{
										Item = (int) MFBuiltInDocumentClass.MFBuiltInDocumentClassOtherDocument
									}
								}
							}
						}
					}
				}.ToList()
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ExternalObjectOperations.PromoteObjects(
				objectVersionUpdateInformation: body.MultipleObjectInfo.ToArray());

			// Verify.
			runner.Verify();
		}

		#endregion

		#endregion

	}
}
