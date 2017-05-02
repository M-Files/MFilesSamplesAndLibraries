using System.Threading.Tasks;
using MFaaP.MFWSClient;

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
			System.Console.WriteLine($"Retrieving the value list items using the library.");
			Task.WaitAll(Program.UseLibrary());
			System.Console.WriteLine("Complete.  Press enter to continue.");
			System.Console.ReadLine();

		}
		/// <summary>
		/// Uses the helper library to execute a search.
		/// </summary>
		static async Task UseLibrary()
		{

			// Connect to the online knowledgebase.
			// Note that this doesn't require authentication.
			var client = new MFWSClient("http://kb.cloudvault.m-files.com");

			// Retrieve the value list items.
			var results = await client.GetValueListItems(Program.valueListId, "security");

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
