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

				// Get the view contents (root if no parent).
				// We have to reverse this as, by default, Stack<T> will return data from
				// the top of the stack downwards (newest -> oldest), whereas we want the bottom upwards
				// (oldest -> newest).
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

				// Ask them where to go next.
				var nextNavItem = GetNextNavigationItem(results, out quit);

				// If they didn't choose to quit then select the next navigation level.
				if (false == quit)
				{

					// Did they choose to go up a view?
					if (null == nextNavItem)
					{
						// Remove the top one from the navigation stack ("go back").
						if(navigation.Count > 0)
							navigation.Pop();
					}
					else
					{
						// Add it to the navigation stack ("go in").
						navigation.Push(nextNavItem);
					}

				}

			}

		}

		/// <summary>
		/// Prompts the user which view or property grouping to go int.
		/// </summary>
		/// <param name="itemsToChooseFrom">The items at this level.</param>
		/// <param name="quit">If true, the user chose to quit rather than navigate.</param>
		/// <returns>The selected item to navigate to, or null to go back a folder.</returns>
		private static FolderContentItem GetNextNavigationItem(FolderContentItems itemsToChooseFrom, out bool quit)
		{
			int selectedIndex = -1;
			quit = false;

			// As the user to select a number to go to, or q to exit.
			while (selectedIndex == -1)
			{
				// Prompt the user.
				System.Console.WriteLine("Enter the number of the item to navigate to, or q to exit.");
				var enteredText = System.Console.ReadLine();

				// Did they ask to quit?
				if ("q".Equals(enteredText, StringComparison.InvariantCultureIgnoreCase))
				{
					// Quit!
					quit = true;
					return null;
				}

				// Did they select a valid item?
				if (Int32.TryParse(enteredText, out selectedIndex) && selectedIndex >= 0 && selectedIndex <= itemsToChooseFrom.Items.Count)
				{
					// It was valid, but what did they select?

					// 0 == go up a view.
					if (selectedIndex == 0)
					{
						return null;
					}
					else
					{
						// Get the item selected.
						var nextNavItem = itemsToChooseFrom.Items[selectedIndex - 1];

						// If it was an object version then we can't go in.
						if (nextNavItem.FolderContentItemType == MFFolderContentItemType.ObjectVersion)
						{
							System.Console.WriteLine("You cannot navigate into an object.");
							selectedIndex = -1;
						}
						else
						{
							// Otherwise navigate to the new level.
							return nextNavItem;
						}
					}
				}

				// Not a valid item - ask again.
				selectedIndex = -1;
			}

			return null;
		}

	}
}
