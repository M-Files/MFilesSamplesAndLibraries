using System;
using System.Net.Http;
using System.Threading.Tasks;
using MFaaP.MFWSClient;
using Newtonsoft.Json;

namespace MFWSCheckOutStatus
{
	/// <summary>
	/// An example of retrieving an object checkout status.
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{

			// Execute the search using the library.
			Console.WriteLine($"Retrieving the check out status using the library.");
			Task.WaitAll(Program.UseLibrary());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();

			// Execute the search without using the library.
			Console.WriteLine($"Retrieving the check out status using the API directly.");
			Task.WaitAll(Program.UseApiDirectly());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();

		}

		/// <summary>
		/// Uses the helper library to retrieve an object checkout status.
		/// </summary>
		static async Task UseLibrary()
		{

			// Connect to the online knowledgebase.
			// Note that this doesn't require authentication.
			var client = new MFWSClient("http://kb.cloudvault.m-files.com");

			// Search for automatic filling of properties document.
			var results = await client.ObjectSearchOperations.SearchForObjectsByString("automatic filling of properties");
			if (0 == results.Length)
			{
				// Could not find the object.
				Console.WriteLine($"\tDocument not found.");
				return;
			}

			// Whilst the checkout status is available in the ObjectVersion directly,
			// let's retrieve it to show the standard call.
			// Get the checkout status.
			var checkoutStatus = await client.ObjectOperations.GetCheckoutStatus(results[0].ObjVer.Type, results[0].ObjVer.ID);

			// Output it.
			Console.WriteLine($"\tCheckout status is: {checkoutStatus}");

		}

		/// <summary>
		/// Retrieves an object checkout status using the endpoint directly.
		/// </summary>
		static async Task UseApiDirectly()
		{
			// Build the url to request (note to encode the query term).
			var url =
				new Uri("http://kb.cloudvault.m-files.com/REST/objects?q=" + System.Net.WebUtility.UrlEncode("automatic filling of properties"));

			// Build the request.
			var httpClient = new HttpClient();

			// Start the request.
			var responseBody = await httpClient.GetStringAsync(url);

			// Output the body.
			// System.Console.WriteLine($"Raw content returned: {responseBody}.");

			// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
			var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);
			if (0 == results.Items.Count)
			{
				// Could not find the object.
				Console.WriteLine($"\tDocument not found.");
				return;
			}

			// Whilst the checkout status is available in the ObjectVersion directly,
			// let's retrieve it to show the standard call.
			// Get the checkout status.
			url = 
				new Uri($"http://kb.cloudvault.m-files.com/REST/objects/{results.Items[0].ObjVer.Type}/{results.Items[0].ObjVer.ID}/{results.Items[0].ObjVer.Version}/checkedout");
			responseBody = await httpClient.GetStringAsync(url);

			// Attempt to parse it.
			var checkoutStatus = JsonConvert.DeserializeObject<PrimitiveType<MFCheckOutStatus>>(responseBody);

			Console.WriteLine($"\tCheckout status is: {checkoutStatus.Value}");

		}
	}
}
