using System;
using System.Linq;
using MFaaP.MFilesAPI;
using MFaaP.MFilesAPI.ExtensionMethods;
using MFilesAPI;

namespace SegmentedSearch
{
	/// <summary>
	/// A sample program showing how to execute a segmented object search using the API.
	/// </summary>
	/// <remarks>Note that this sample uses extension methods from the MFaaP.MFilesAPI helper library.</remarks>
	class Program
	{
		/// <summary>
		/// The (default) guid of the sample vault.
		/// </summary>
		private static readonly Guid sampleVaultGuid
			//= Guid.Parse("{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}");
			= Guid.Parse("{21F4D5AB-7E1A-4B98-A8D9-188C4A0EDF1B}");

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
		/// Uses the helper library to execute a segmented search.
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

			// Load the object types from the vault.
			System.Console.WriteLine("Loading object types...");
			var objectTypes = vault
				.ObjectTypeOperations
				.GetObjectTypes()
				.Cast<ObjType>()
				.ToList();
			System.Console.WriteLine($"Iterating over {objectTypes.Count} object types...");

			// Iterate over the object types to count the objects.
			foreach (var objectType in objectTypes)
			{
				// Create the basic search conditions collection.
				var searchConditions = new SearchConditions();

				// Add a condition for the object type we're interested in.
				searchConditions.AddObjectTypeIdSearchCondition(objectType.ID);

				// Count the items in this object type, including deleted ones.
				// This will execute the search in segments (i.e. items 0-1000, then 1001-2000),
				// but will "flatten" the results into a collection of ObjectVersion objects.
				var countIncludingDeleted = vault.ObjectSearchOperations
					.SearchForObjectsByConditionsSegmented_Flat(searchConditions)
					.Count();

				// Output the stats.
				System.Console.WriteLine($"\t{objectType.NamePlural}:");
				System.Console.WriteLine($"\t\tTotal: {countIncludingDeleted} (included deleted)");

			}

			System.Console.WriteLine($"Complete.");

			// Ensure we're disconnected.
			application.Disconnect(vault);
		}

		/// <summary>
		/// Executes a segmented search using the API directly.
		/// </summary>
		static void UseAPIDirectly()
		{

			// Connect to the server (localhost, tcp, current Windows user).
			var application = new MFilesServerApplication();
			application.ConnectAdministrative();

			// Get a connection to the vault.
			var vault = application.LogInToVault(Program.sampleVaultGuid.ToString("B"));

			// Load the object types from the vault.
			System.Console.WriteLine("Loading object types...");
			var objectTypes = vault
				.ObjectTypeOperations
				.GetObjectTypes()
				.Cast<ObjType>()
				.ToList();
			System.Console.WriteLine($"Iterating over {objectTypes.Count} object types...");

			// Iterate over the object types to count the objects.
			foreach (var objectType in objectTypes)
			{
				// Create the basic search conditions collection.
				var searchConditions = new SearchConditions();

				// Add a condition for the object type we're interested in.
				{
					// Create the search condition (for object type id).
					SearchCondition condition = new SearchCondition
					{
						ConditionType = MFConditionType.MFConditionTypeEqual
					};
					condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectTypeID, null);
					condition.TypedValue.SetValue(MFDataType.MFDatatypeLookup, objectType.ID);

					// Add the condition at the index provided.
					searchConditions.Add(-1, condition);
				}

				// Create variables for the segment information.
				const int itemsPerSegment = 1000; // Maximum number of items in each segment.
				var segment = 0; // Start; this will increment as we go.
				var moreItems = true; // Whether there are more items to load.
				var countIncludingDeleted = 0; // The count of matching items.

				// Whilst there are items in the results, we need to loop.
				while (moreItems)
				{
					// Execute a search within the object id segment.
					{
						// Clone the search conditions (so we can add current-segment condition).
						var internalSearchConditions = searchConditions.Clone();

						// Add search condition:
						//   Id within the range: (segment - itemsPerSegment) to ((segment + 1) * itemsPerSegment)
						{
							// Create the search condition.
							SearchCondition condition = new SearchCondition
							{
								ConditionType = MFConditionType.MFConditionTypeEqual
							};
							condition.Expression.SetObjectIDSegmentExpression(itemsPerSegment);
							condition.TypedValue.SetValue(MFDataType.MFDatatypeInteger, segment);

							// Add the condition at the index provided.
							internalSearchConditions.Add(-1, condition);
						}

						// Execute the search and increment the count.
						countIncludingDeleted += vault.ObjectSearchOperations
							.SearchForObjectsByConditionsEx(internalSearchConditions, MFSearchFlags.MFSearchFlagDisableRelevancyRanking,
								SortResults: false, MaxResultCount: 0,
								SearchTimeoutInSeconds: 0).Count;

						// Move to the next segment.
						segment++;

					}

					// Are there any more items?
					{
						// Clone the search conditions (so we can add object id condition).
						var internalSearchConditions = searchConditions.Clone();

						// Add search condition:
						//   Id at least (segment * itemsPerSegment)
						{
							// Create the search condition.
							SearchCondition condition = new SearchCondition
							{
								ConditionType = MFConditionType.MFConditionTypeGreaterThanOrEqual
							};
							condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectID, null);
							condition.TypedValue.SetValue(MFDataType.MFDatatypeInteger, segment * itemsPerSegment);

							// Add the condition at the index provided.
							internalSearchConditions.Add(-1, condition);
						}

						// If we get one item then there's more results.
						moreItems = 1 == vault.ObjectSearchOperations.SearchForObjectsByConditionsEx(
							internalSearchConditions, // Our search conditions.
							MFSearchFlags.MFSearchFlagDisableRelevancyRanking,
							SortResults: false, // Don't bother attempting to sort them.
							MaxResultCount: 1, // We only need to know if there is at least one, nothing more.
							SearchTimeoutInSeconds: 0).Count;
					}
				}

				// Output the stats.
				System.Console.WriteLine($"\t{objectType.NamePlural}:");
				System.Console.WriteLine($"\t\tTotal: {countIncludingDeleted} (included deleted)");

			}

			System.Console.WriteLine($"Complete.");

			// Disconnect.
			vault.LogOutSilent();
			application.Disconnect();

		}
	}
}
