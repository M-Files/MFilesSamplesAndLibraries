using System;
using System.Linq;
using System.Threading.Tasks;
using MFaaP.MFWSClient;

namespace MFWSVaultStructure
{
	class Program
	{

		static void Main(string[] args)
		{
			// Retrieve the vault structure using the library.
			Console.WriteLine("Retrieving the vault structure using the library.");
			Task.WaitAll(Program.UseLibrary());
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

			// Get the object types.
			var objectTypes = await client.ObjectTypeOperations.GetObjectTypesAsync();
			Console.WriteLine($"There are {objectTypes.Count} object types in the vault:");
			foreach (var item in objectTypes)
			{
				// Output basic content.
				System.Console.WriteLine($"\t{item.Name} ({item.NamePlural})");
				System.Console.WriteLine($"\t\tID: {item.ID}");

				// Get the classes.
				var classes = await client.ClassOperations.GetObjectClassesAsync(item.ID);
				Console.WriteLine($"\t\tThere are {classes.Count} classes in the vault:");
				foreach (var c in classes)
				{
					// Output basic content.
					System.Console.WriteLine($"\t\t\t{c.Name}");
					System.Console.WriteLine($"\t\t\t\tID: {c.ID}");

					// Are there any templates?
					var classTemplates = (await client.ClassOperations.GetObjectClassAsync(c.ID, true)).Templates;
					if (null != classTemplates && 0 != classTemplates.Count)
					{
						System.Console.WriteLine($"\t\t\t\tTemplates:");
						foreach (var template in classTemplates)
						{
							// Output basic content.
							System.Console.WriteLine($"\t\t\t\t\t{template.Title}");
							System.Console.WriteLine($"\t\t\t\t\t\tID: {template.ObjVer.ID}");
						}
					}

				}

			}

			// Get the value lists.
			var valueLists = await client.ValueListOperations.GetValueListsAsync();
			Console.WriteLine($"There are {valueLists.Count} value lists in the vault:");
			foreach (var item in valueLists)
			{
				// Output basic content.
				System.Console.WriteLine($"\t{item.Name}");
				System.Console.WriteLine("\t\tID: {item.ID}");

				// Retrieve the items.
				var valueListItems = await client.ValueListItemOperations.GetValueListItemsAsync(item.ID);
				System.Console.WriteLine($"\t\tItems ({valueListItems.Items.Count}):");

				// Output the items.
				foreach (var valueListItem in valueListItems.Items)
				{
					System.Console.WriteLine($"\t\t\t{valueListItem.Name}, ID: {valueListItem.ID}");
				}

			}

		}
	}
}
