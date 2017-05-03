using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MFaaP.MFWSClient;
using Newtonsoft.Json;

namespace MFWSSearching
{
	/// <summary>
	/// A example of executing a simple search using the M-Files Web Service.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The term to search for.
		/// </summary>
		private const string queryTerm = "mfws";

		static void Main(string[] args)
		{
			// Execute the search using the library.
			Console.WriteLine($"Executing a search using the library.");
			Task.WaitAll(Program.UseLibrary());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();

			// Execute the search without using the library.
			Console.WriteLine($"Executing a search using the API directly.");
			Task.WaitAll(Program.UseApiDirectly());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();
		}

		/// <summary>
		/// Uses the helper library to execute a search.
		/// </summary>
		static async Task UseLibrary()
		{

			// Connect to the online knowledgebase.
			// Note that this doesn't require authentication.
			var client = new MFWSClient("http://kb.cloudvault.m-files.com");

			// Execute a quick search for the query term.
			var results = await client.QuickSearch(Program.queryTerm);
			Console.WriteLine($"There were {results.Length} results returned.");

			// Get the object property values (not necessary, but shows how to retrieve multiple sets of properties in one call).
			var properties = await client.GetObjectPropertyValues(results.Select(r => r.ObjVer).ToArray());

			// Iterate over the results and output them.
			for(var i=0; i<results.Length; i++)
			{
				// Output the object version details.
				var objectVersion = results[i];
				Console.WriteLine($"\t{objectVersion.Title}");
				Console.WriteLine($"\t\tType: {objectVersion.ObjVer.Type}, ID: {objectVersion.ObjVer.ID}");

				// Output the properties.
				var objectProperties = properties[i];
				foreach (var property in objectProperties)
				{
					Console.WriteLine($"\t\tProperty: {property.PropertyDef}, Value: {property.TypedValue.Value}");
				}
			}


		}

		/// <summary>
		/// Executes a search using the endpoint directly.
		/// </summary>
		static async Task UseApiDirectly()
		{
			// Build the url to request (note to encode the query term).
			var url =
				new Uri("http://kb.cloudvault.m-files.com/REST/objects?q=" + System.Net.WebUtility.UrlEncode(Program.queryTerm));

			// Build the request.
			var httpClient = new HttpClient();

			// Start the request.
			var responseBody = await httpClient.GetStringAsync(url);

			// Output the body.
			// System.Console.WriteLine($"Raw content returned: {responseBody}.");

			// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
			var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);
			Console.WriteLine($"There were {results.Items.Count} results returned.");

			// Get the object property values (not necessary, but shows how to retrieve multiple sets of properties in one call).
			url = new Uri("http://kb.cloudvault.m-files.com/REST/objects/properties;"
				        + Program.GetObjVersString(results.Items.Select(r => r.ObjVer)));
			responseBody = await httpClient.GetStringAsync(url);
			var properties = JsonConvert.DeserializeObject<PropertyValue[][]>(responseBody);

			// Iterate over the results and output them.
			for (var i = 0; i < results.Items.Count; i++)
			{
				// Output the object version details.
				var objectVersion = results.Items[i];
				Console.WriteLine($"\t{objectVersion.Title}");
				Console.WriteLine($"\t\tType: {objectVersion.ObjVer.Type}, ID: {objectVersion.ObjVer.ID}");

				// Output the properties.
				var objectProperties = properties[i];
				foreach (var property in objectProperties)
				{
					Console.WriteLine($"\t\tProperty: {property.PropertyDef}, Value: {property.TypedValue.Value}");
				}
			}

		}

		/// <summary>
		/// Converts a set of <see cref="ObjVer"/>s into a string that can be passed to 
		/// http://www.m-files.com/mfws/resources/objects/properties.html.
		/// </summary>
		/// <param name="objVers">The object versions to convert.</param>
		/// <returns>The formatted string.</returns>
		static string GetObjVersString(IEnumerable<ObjVer> objVers )
		{
			// Sanity.
			if(null == objVers)
				throw new ArgumentNullException(nameof(objVers));

			// Need to be of the format:
			// {type1}/{id1}/{version1};{type2}/{id2}/{version2}...
			return String.Join(";", objVers.Select(ov => $"{ov.Type}/{ov.ID}/{ov.Version}"));
		}

		#region Classes for deserialisation (only used with the direct API calls)

		/// <summary>
		/// Results of a query which might leave only a partial set of items.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class Results<T>
		{
			public Results()
			{
			}

			/// <summary>
			/// Contains results of a query
			/// </summary>
			public List<T> Items { get; set; }

			/// <summary>
			/// True if there were more results which were left out.
			/// </summary>
			public bool MoreResults { get; set; }

		}

		/// <summary>
		/// Based on M-Files API.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class ObjectVersion
		{

			public ObjectVersion()
			{
			}

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime AccessedByMeUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime CheckedOutAtUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int CheckedOutTo { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string CheckedOutToUserName { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Class { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime CreatedUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool Deleted { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string DisplayID { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public List<ObjectFile> Files { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool HasAssignments { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool HasRelationshipsFromThis { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool HasRelationshipsToThis { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool IsStub { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime LastModifiedUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool ObjectCheckedOut { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool ObjectCheckedOutToThisUser { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public MFObjectVersionFlag ObjectVersionFlags { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public ObjVer ObjVer { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool SingleFile { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool ThisVersionLatestToThisUser { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string Title { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool VisibleAfterOperation { get; set; }

		}



		/// <summary>
		/// Based on M-Files API.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class ObjectFile
		{

			public ObjectFile()
			{
			}

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime ChangeTimeUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string Extension { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int ID { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Version { get; set; }

		}



		/// <summary>
		/// Based on M-Files API.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class ObjVer
		{

			public ObjVer()
			{
			}

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int ID { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Type { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Version { get; set; }

		}

		#endregion

	}
}
