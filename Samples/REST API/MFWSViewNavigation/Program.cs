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
			Console.WriteLine($"Navigating views using the library.");
			 Task.WaitAll(Program.UseLibrary());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();
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
				var results = await client.ViewOperations.GetFolderContentsAsync(navigation.Reverse().ToArray());

				// Clear the screen.
				Console.Clear();

				// Get some indication of where we are.
				var navigationString = String.Join(" > ", navigation.Reverse().Select(i => i.GetDisplayName()));

				// Output it to screen.
				Console.WriteLine(navigationString);

				// Output the number returned.
				Console.WriteLine($"There are {results.Items.Count} items:");
				var count = 0;

				// If we can go up a view then give that option.
				if (navigation.Count > 0)
				{
					Console.WriteLine("\t0: .. (up a view)");
				}

				// Iterate over the results and output them.
				foreach (var item in results.Items)
				{
					Console.WriteLine($"\t{++count}: {item.FolderContentItemType}: {item.GetDisplayName()}");
				}

				// Ask them where to go next.
				var nextNavItem = Program.GetNextNavigationItem(results, out quit);

				// If they chose to quit then exit out now.
				if (quit)
					continue;

				// Did they choose to go up a view?
				if (null == nextNavItem)
				{
					// Remove the top one from the navigation stack ("go back").
					if(navigation.Count > 0)
						navigation.Pop();
				}
				else
				{
					// If they chose to go "into" an object then return the history.
					if (nextNavItem.FolderContentItemType == MFFolderContentItemType.ObjectVersion)
					{
						// If it's an unpromoted external object then we can't do anything.
						if (nextNavItem.ObjectVersion.ObjVer.ID == 0
							&& false == string.IsNullOrEmpty(nextNavItem.ObjectVersion.ObjVer.ExternalRepositoryObjectID))
						{
							Console.WriteLine("History cannot be viewed on unpromoted objects:.");
							Console.WriteLine($"\tu{nextNavItem.ObjectVersion.ObjVer.ExternalRepositoryName}:{nextNavItem.ObjectVersion.ObjVer.ExternalRepositoryObjectID}");
							Console.WriteLine("Press any key to go back to the previous listing.");
							Console.ReadKey();
						}
						else
						{

							// Get the history.
							var versions = await client.ObjectOperations.GetHistoryAsync(new ObjID()
							{
								Type = nextNavItem.ObjectVersion.ObjVer.Type,
								ID = nextNavItem.ObjectVersion.ObjVer.ID,
								ExternalRepositoryName = nextNavItem.ObjectVersion.ObjVer.ExternalRepositoryName,
								ExternalRepositoryObjectID = nextNavItem.ObjectVersion.ObjVer.ExternalRepositoryObjectID
							});

							// Output them.
							Console.WriteLine($"There are {versions.Count} versions:");
							foreach (var version in versions)
							{
								Console.WriteLine($"{version.ObjVer.Version} (Created: {version.LastModifiedUtc})");
							}

							// Allow the user to go out.
							Console.WriteLine("Press any key to go back to the previous listing.");
							Console.ReadKey();
						}
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
				Console.WriteLine("Enter the number of the item to navigate to, or q to exit.");
				var enteredText = Console.ReadLine();

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

						// Navigate to the new level.
						return nextNavItem;
					}
				}

				// Not a valid item - ask again.
				selectedIndex = -1;
			}

			return null;
		}

	}
}
