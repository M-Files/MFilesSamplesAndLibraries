using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFaaP.MFWSClient;
using MFaaP.MFWSClient.ExtensionMethods;

namespace MFWSViewNavigation
{
	/// <summary>
	/// An example of navigating views through the M-Files Web Service.
	/// </summary>
	class Program
	{

		static void Main(string[] args)
		{
			// Execute the search using the library.
			System.Console.WriteLine($"Navigating views using the library.");
			 Task.WaitAll(UseLibrary());
			System.Console.WriteLine("Complete.  Press enter to continue.");
			System.Console.ReadLine();
		}

		/// <summary>
		/// Uses the helper library to navigate the view structure.
		/// </summary>
		static async Task UseLibrary()
		{

			// Connect to the online knowledgebase.
			// Note that this doesn't require authentication.
			var client = new MFWSClient("http://kb.cloudvault.m-files.com");

			// Create a stack for the navigation.
			// As we go in/out views and groupings, this will hold where we are.
			Stack<FolderContentItem> navigation = new Stack<FolderContentItem>();

			// Have they selected to quit?
			bool quit = false;

			// Whilst they haven't selected to quit, show the selected contents.
			while (false == quit)
			{

				// Get the view contents (root if no parent)
				var results = await client.GetViewContents(navigation.Reverse().ToArray());

				// Output the number returned.
				System.Console.WriteLine($"There were {results.Items.Count} results returned.");
				var count = 0;

				// If we can go up a view then give that option.
				if (navigation.Count > 0)
				{
					System.Console.WriteLine("\t0: .. (up a view)");
				}

				// Iterate over the results and output them.
				foreach (var item in results.Items)
				{
					System.Console.WriteLine($"\t{++count}: {item.FolderContentItemType}: {item.GetDisplayName()}");
				}

				// As the user to select a number to go to, or q to exit.
				int selectedIndex = 0;
				while (selectedIndex == 0)
				{
					// Prompt the user.
					System.Console.WriteLine("Enter the number of the item to navigate to, or q to exit.");
					var enteredText = System.Console.ReadLine();

					// Did they ask to quit?
					if ("q".Equals(enteredText, StringComparison.InvariantCultureIgnoreCase))
					{
						// Quit!
						quit = true;
						break;
					}

					// Did they select a valid item?
					if (Int32.TryParse(enteredText, out selectedIndex) && selectedIndex >= 0 && selectedIndex <= results.Items.Count)
					{
						// It was valid, but what did they select?

						// 0 == go up a view.
						if (selectedIndex == 0)
						{
							// Remove the top one from the navigation stack ("go back").
							navigation.Pop();

							// Break (query for items at the new navigation stack);
							break;
						}
						else
						{
							// Get the item selected.
							var nextNavItem = results.Items[selectedIndex - 1];

							// If it was an object version then we can't go in.
							if (nextNavItem.FolderContentItemType == MFFolderContentItemType.ObjectVersion)
							{
								System.Console.WriteLine("You cannot navigate into an object.");
								selectedIndex = 0;
							}
							else
							{
								// Add it to the navigation stack ("go in").
								navigation.Push(nextNavItem);

								// Break (query for items at the new navigation stack);
								break;
							}
						}
					}
					else
					{
						// Not a valid item - ask again.
						selectedIndex = 0;
					}
				}
			}

		}

	}
}
