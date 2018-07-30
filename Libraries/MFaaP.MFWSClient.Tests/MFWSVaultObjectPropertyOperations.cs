using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectPropertyOperations
	{

		#region Path encoding for unmanaged objects

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesAsync(ObjVer,System.Threading.CancellationToken)"/>
		/// correctly encodes paths for unmanaged objects.
		/// </summary>
		[TestMethod]
		public async Task GetPropertiesAsync_ExternalObject_LatestVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValue>>(Method.GET, $"/REST/objects/0/umyrepository%3A12%2B3456/latest/properties.aspx");

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.GetPropertiesAsync(new ObjVer()
			{
				Type = 0,
				VersionType = MFObjVerVersionType.Latest,
				ExternalRepositoryName = "myrepository",
				ExternalRepositoryObjectID = "12 3456" // NOTE: This will be double-encoded (" " to "+", then to "%2B").
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesAsync(ObjVer,System.Threading.CancellationToken)"/>
		/// correctly encodes paths for unmanaged objects.
		/// </summary>
		[TestMethod]
		public async Task GetPropertiesAsync_ExternalObject_SpecificVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValue>>(Method.GET, $"/REST/objects/0/umyrepository%3A12%2B3456/uabc%253A123/properties.aspx");

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.GetPropertiesAsync(new ObjVer()
			{
				Type = 0,
				VersionType = MFObjVerVersionType.Latest,
				ExternalRepositoryName = "myrepository",
				ExternalRepositoryObjectID = "12 3456", // NOTE: This will be double-encoded (" " to "+", then to "%2B").
				ExternalRepositoryObjectVersionID = "abc:123" // NOTE: This will be double-encoded (":" to "%3A", then to "%253A").
			});

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetProperties (single object)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetProperties(MFaaP.MFWSClient.ObjVer,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void GetProperties_ObjVer()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValue>>(Method.GET, "/REST/objects/1/2/4/properties.aspx");

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.GetProperties(1, 2, 4);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesAsync(MFaaP.MFWSClient.ObjVer,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task GetPropertiesAsync_ObjVer()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValue>>(Method.GET, "/REST/objects/1/2/4/properties.aspx");

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.GetPropertiesAsync(1, 2, 4);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetProperties(ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void GetProperties_ObjID()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValue>>(Method.GET, "/REST/objects/1/2/latest/properties.aspx");

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.GetProperties(1, 2);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesAsync(ObjID,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task GetPropertiesAsync_ObjID()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValue>>(Method.GET, "/REST/objects/1/2/latest/properties.aspx");

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.GetPropertiesAsync(1, 2);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetProperties (multiple object versions)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesOfMultipleObjects(MFaaP.MFWSClient.ObjVer[])"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void GetPropertiesOfMultipleObjects()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<List<PropertyValue>>>(Method.POST, "/REST/objects/properties.aspx");

			// Create the object to send in the body.
			var body = new ObjVer()
			{
				ID = 2,
				Type = 1,
				Version = 4
			};

			// We should post a collection of objvers (but only with this one in it).
			runner.SetExpectedRequestBody(new[] { body });

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.GetPropertiesOfMultipleObjects(body);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesOfMultipleObjectsAsync(MFaaP.MFWSClient.ObjVer[])"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task GetPropertiesOfMultipleObjectsAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<List<PropertyValue>>>(Method.POST, "/REST/objects/properties.aspx");

			// Create the object to send in the body.
			var body = new ObjVer()
			{
				ID = 2,
				Type = 1,
				Version = 4
			};

			// We should post a collection of objvers (but only with this one in it).
			runner.SetExpectedRequestBody(new[] { body });

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.GetPropertiesOfMultipleObjectsAsync(body);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesOfMultipleObjects(MFaaP.MFWSClient.ObjVer[])"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[ExpectedException(typeof(System.ArgumentException))]
		public void GetPropertiesOfMultipleObjects_ExceptionForNoVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<List<PropertyValue>>>(Method.POST, "/REST/objects/properties.aspx");

			// Create the object to send in the body.
			var body = new ObjVer()
			{
				ID = 2,
				Type = 1
			};

			// We should post a collection of objvers (but only with this one in it).
			runner.SetExpectedRequestBody(new[] { body });

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.GetPropertiesOfMultipleObjects(body);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesOfMultipleObjectsAsync(MFaaP.MFWSClient.ObjVer[])"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[ExpectedException(typeof(System.ArgumentException))]
		public async Task GetPropertiesOfMultipleObjectsAsync_ExceptionForNoVersion()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<List<PropertyValue>>>(Method.POST, "/REST/objects/properties.aspx");

			// Create the object to send in the body.
			var body = new ObjVer()
			{
				ID = 2,
				Type = 1
			};

			// We should post a collection of objvers (but only with this one in it).
			runner.SetExpectedRequestBody(new[] { body });

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.GetPropertiesOfMultipleObjectsAsync(body);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region SetProperty

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetProperty(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void SetProperty()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.PUT, "/REST/objects/1/2/latest/properties/0");

			// Create the object to send in the body.
			var body = new PropertyValue()
			{
				PropertyDef = 0,
				TypedValue = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "hello world"
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.SetProperty(1, 2, body);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetPropertyAsync(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task SetPropertyAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.PUT, "/REST/objects/1/2/latest/properties/0");

			// Create the object to send in the body.
			var body = new PropertyValue()
			{
				PropertyDef = 0,
				TypedValue = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "hello world"
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.SetPropertyAsync(1, 2, body);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region RemoveProperty

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.RemoveProperty(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void RemoveProperty()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.DELETE, "/REST/objects/1/2/latest/properties/0");

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.RemoveProperty(1, 2, 0);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.RemovePropertyAsync(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task RemovePropertyAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.DELETE, "/REST/objects/1/2/latest/properties/0");

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.RemovePropertyAsync(1, 2, 0);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region SetProperties

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetProperties(int,int,MFaaP.MFWSClient.PropertyValue[], bool,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void SetProperties_ReplaceAllProperties()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.PUT, "/REST/objects/1/2/latest/properties");

			// Create the object to send in the body.
			var body = new[]
			{
				new PropertyValue()
				{
					PropertyDef = 0,
					TypedValue = new TypedValue()
					{
						DataType = MFDataType.Text,
						Value = "hello world"
					}
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.SetProperties(1, 2, body, true);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetProperties(int,int,MFaaP.MFWSClient.PropertyValue[], bool,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public void SetProperties_UpdateProperties()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.POST, "/REST/objects/1/2/latest/properties");

			// Create the object to send in the body.
			var body = new[]
			{
				new PropertyValue()
				{
					PropertyDef = 0,
					TypedValue = new TypedValue()
					{
						DataType = MFDataType.Text,
						Value = "hello world"
					}
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.SetProperties(1, 2, body, false);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetPropertyAsync(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task SetPropertiesAsync_ReplaceAllProperties()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.PUT, "/REST/objects/1/2/latest/properties");

			// Create the object to send in the body.
			var body = new[]
			{
				new PropertyValue()
				{
					PropertyDef = 0,
					TypedValue = new TypedValue()
					{
						DataType = MFDataType.Text,
						Value = "hello world"
					}
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.SetPropertiesAsync(1, 2, body, true);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetPropertyAsync(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task SetPropertiesAsync_UpdateProperties()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>(Method.POST, "/REST/objects/1/2/latest/properties");

			// Create the object to send in the body.
			var body = new[]
			{
				new PropertyValue()
				{
					PropertyDef = 0,
					TypedValue = new TypedValue()
					{
						DataType = MFDataType.Text,
						Value = "hello world"
					}
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.SetPropertiesAsync(1, 2, body, false);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Set properties of multiple objects at once

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetPropertiesOfMultipleObjectsAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task SetPropertiesOfMultipleObjectsAsync()
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
							Type = 0,
							ID = 1,
							Version = 2
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
							Type = 0,
							ID = 2,
							Version = 1
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
			await runner.MFWSClient.ObjectPropertyOperations.SetPropertiesOfMultipleObjectsAsync(
				objectVersionUpdateInformation: body.MultipleObjectInfo.ToArray());

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetPropertiesOfMultipleObjects"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void SetPropertiesOfMultipleObjects()
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
							Type = 0,
							ID = 1,
							Version = 2
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
							Type = 0,
							ID = 2,
							Version = 1
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

	}
}
