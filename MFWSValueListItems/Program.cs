using System;
using System.Net.Http;
using System.Threading.Tasks;
using MFaaP.MFWSClient;
using Newtonsoft.Json;

namespace MFWSValueListItems
{
	class Program
	{

		/// <summary>
		///  The Id of the value list to retrieve.
		/// </summary>
		private static readonly int valueListId = 165;

		static void Main(string[] args)
		{
			// Retrieve the value list items using the library.
			Console.WriteLine("Retrieving the value list items using the library.");
			Task.WaitAll(Program.UseLibrary());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();

			// Retrieve the value list items without using the library.
			Console.WriteLine("Retrieving the value list items using the API directly.");
			Task.WaitAll(Program.UseApiDirectly());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();

		}

		/// <summary>
		/// Uses the helper library to retrieve the value list items.
		/// </summary>
		static async Task UseLibrary()
		{

			// Connect to the online knowledgebase.
			// Note that this doesn't require authentication.
			var client = new MFWSClient("http://kb.cloudvault.m-files.com");

			// Retrieve the value list items.
			var results = await client.GetValueListItems(Program.valueListId);

			// Iterate over the results and output them.
			System.Console.WriteLine($"There were {results.Items.Count} results returned.");
			foreach (var item in results.Items)
			{
				System.Console.WriteLine($"\t{item.Name}");
				System.Console.WriteLine($"\t\tType: {item.ValueListID}, ID: {item.ID}");

			}

		}

		/// <summary>
		/// Uses the APi directly to retrieve the value list items.
		/// </summary>
		static async Task UseApiDirectly()
		{
			// Build the url to request (note to encode the query term).
			var url =
				new Uri("http://kb.cloudvault.m-files.com/REST/valuelists/" + Program.valueListId + "/items");

			// Build the request.
			var httpClient = new HttpClient();

			// Start the request.
			string responseBody = await httpClient.GetStringAsync(url);

			// Output the body.
			// System.Console.WriteLine($"Raw content returned: {responseBody}.");

			// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
			var results = JsonConvert.DeserializeObject<Results<ValueListItem>>(responseBody);

			// Iterate over the results and output them.
			System.Console.WriteLine($"There were {results.Items.Count} results returned.");
			foreach (var item in results.Items)
			{
				System.Console.WriteLine($"\t{item.Name}");
				System.Console.WriteLine($"\t\tType: {item.ValueListID}, ID: {item.ID}");

			}

		}
	}
}
