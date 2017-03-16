using System;
using System.Linq;
using MFaaP.MFilesAPI;
using MFaaP.MFilesAPI.ExtensionMethods;
using MFilesAPI;

namespace SearchByDisplayId
{
	/// <summary>
	/// A sample program showing how to execute a search by the display (or "external" id) of an object.
	/// </summary>
	/// <remarks>Note that this sample uses extension methods from the MFaaP.MFilesAPI helper library.</remarks>
	class Program
	{
		/// <summary>
		/// The Id that we will search for.
		/// </summary>
		private static string customerDisplayId = "CUSTOMER1";

		/// <summary>
		/// The (default) guid of the sample vault.
		/// </summary>
		private static readonly Guid sampleVaultGuid
			= Guid.Parse("{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}");

		static void Main(string[] args)
		{

			// We have two methods in the class.

			// One uses the helper library and is designed to read as clearly as possible.
			// However, it does depend upon the helper library.
			UseLibrary();

			// The second does not use the helper library and is designed to show the full
			// process using the API.  It can be used in situations where the helper library
			// cannot be used.
			UseAPIDirectly();

		}

		/// <summary>
		/// Uses the helper library to execute a search by display/external id.
		/// </summary>
		static void UseLibrary()
		{
			// Declare variables for our vault connection.
			Vault vault;
			MFilesServerApplication application;

			// The default connection (localhost, tcp, current Windows user) will suffice.
			var connectionDetails = new ConnectionDetails();

			// Connect to the vault.
			connectionDetails.ConnectToVaultAdministrative(
				Program.sampleVaultGuid,
				out vault, out application);

			// Create the basic search conditions collection.
			var searchConditions = new SearchConditions();

			// Add a condition for the display Id.
			searchConditions.AddDisplayIdSearchCondition(Program.customerDisplayId);

			// Search.
			var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
				MFSearchFlags.MFSearchFlagNone, SortResults: false);

			// Output the number of items matching (should be one in each object type, at a maximum).
			System.Console.WriteLine($"There were {results.Count} objects with the display Id of {Program.customerDisplayId}:");

			System.Console.WriteLine($"Complete.");

			// Ensure we're disconnected.
			application.Disconnect(vault);
		}

		/// <summary>
		/// Executes a search by display/external id using the API directly.
		/// </summary>
		static void UseAPIDirectly()
		{

			// Connect to the server (localhost, tcp, current Windows user).
			var application = new MFilesServerApplication();
			application.ConnectAdministrative();

			// Get a connection to the vault.
			var vault = application.LogInToVault(Program.sampleVaultGuid.ToString("B"));

			// Create the basic search conditions collection.
			var searchConditions = new SearchConditions();

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeEqual,
			};
			condition.Expression.DataStatusValueType = MFStatusType.MFStatusTypeExtID;
			condition.TypedValue.SetValue(MFDataType.MFDatatypeText, Program.customerDisplayId);

			// Add the condition to the collection.
			searchConditions.Add(-1, condition);

			// Search.
			var results = vault.ObjectSearchOperations.SearchForObjectsByConditions(searchConditions,
				MFSearchFlags.MFSearchFlagNone, SortResults: false);

			// Output the number of items matching (should be one in each object type, at a maximum).
			System.Console.WriteLine($"There were {results.Count} objects with the display Id of {Program.customerDisplayId}:");

			System.Console.WriteLine($"Complete.");

			// Disconnect.
			vault.LogOutSilent();
			application.Disconnect();

		}
	}
}
