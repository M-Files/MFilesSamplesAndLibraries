using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using RestSharp.Extensions.MonoHttp;

namespace MFaaP.MFWSClient.ExtensionMethods
{
	/// <summary>
	/// Folder item extension methods.
	/// </summary>
	public static class FolderContentItemExtensionMethods
	{
		/// <summary>
		/// Retrieves the name of a folder content item for display.
		/// </summary>
		/// <param name="item">The folder content item to retrieve the name of.</param>
		/// <returns>The name, or empty if cannot be found.</returns>
		public static string GetDisplayName(this FolderContentItem item)
		{
			// Sanity.
			if (null == item)
				throw new ArgumentNullException(nameof(item));

			// Get the name depending on type.
			switch (item.FolderContentItemType)
			{
				case MFFolderContentItemType.ObjectVersion:
					return item.ObjectVersion.Title;
				case MFFolderContentItemType.PropertyFolder:
					return item.PropertyFolder.DisplayValue;
				case MFFolderContentItemType.ViewFolder:
					return item.View.ViewLocation?.Overlapping == true
						? item.View.ViewLocation.OverlappedFolder.DisplayValue
						: item.View.Name;
				case MFFolderContentItemType.TraditionalFolder:
					return item.TraditionalFolder.DisplayValue;
				case MFFolderContentItemType.ExternalViewFolder:
					return item.ExternalView.DisplayName;
				default:
					// Unknown.
					return string.Empty;
			}
		}

		/// <summary>
		/// Returns the path information to be used to retrieve items from the view.
		/// </summary>
		/// <remarks>
		/// See: http://www.m-files.com/mfws/resources/views/path/items.html
		/// </remarks>
		/// <param name="items">The items to return the view path for.</param>
		/// <returns>The view path.</returns>
		public static string GetPath(this FolderContentItem[] items)
		{
			// Sanity.
			if (null == items || 0 == items.Length)
				return string.Empty;

			// Return the paths, separated by "/".
			return String.Join("/", items.Select(FolderContentItemExtensionMethods.GetPath).Where(p => false == string.IsNullOrWhiteSpace(p))) + "/";

		}

		/// <summary>
		/// Returns the path information to be used to retrieve items from the view.
		/// </summary>
		/// <remarks>
		/// See: http://www.m-files.com/mfws/resources/views/path/items.html
		/// </remarks>
		/// <param name="item">The item to return the view path for.</param>
		/// <returns>The view path.</returns>
		public static string GetPath(this FolderContentItem item)
		{
			// Sanity.
			if (null == item)
				return string.Empty;

			// The return value depends on the item type.
			// See: http://www.m-files.com/mfws/syntax.html
			switch (item.FolderContentItemType)
			{
				case MFFolderContentItemType.ExternalViewFolder:
					// NOTE: The string should be "u", followed by the URL-encoded external repository name, followed by a colon, followed by the URL-encoded view ID.
					// The entire string should then be URL-encoded a second time to ensure that it is passed correctly.
					return WebUtility.UrlEncode($"u{WebUtility.UrlEncode(item.ExternalView.ExternalRepositoryName)}:{WebUtility.UrlEncode(item.ExternalView.ID)}");
				case MFFolderContentItemType.ViewFolder:
					return "v" + item.View.ID;
				case MFFolderContentItemType.TraditionalFolder:
					return "y" + item.TraditionalFolder.Item;
				case MFFolderContentItemType.PropertyFolder:
					{
						string prefix = null;
						string suffix = item.PropertyFolder.Value?.ToString();
						switch (item.PropertyFolder.DataType)
						{
							case MFDataType.Text:
								prefix = "T";
								break;
							case MFDataType.MultiLineText:
								prefix = "M";
								break;
							case MFDataType.Integer:
								prefix = "I";
								break;
							case MFDataType.Integer64:
								prefix = "J";
								break;
							case MFDataType.Floating:
								prefix = "R";
								break;
							case MFDataType.Date:
								prefix = "D";
								break;
							case MFDataType.Time:
								prefix = "C";
								break;
							case MFDataType.FILETIME:
								prefix = "E";
								break;
							case MFDataType.Lookup:
								prefix = "L";
								suffix = (item.PropertyFolder.Lookup?.Item ?? 0).ToString();
								break;
							case MFDataType.MultiSelectLookup:
								prefix = "S";
								suffix = String.Join(",", item.PropertyFolder.Lookups?.Select(l => l.Item) ?? new int[0]);
								break;
							case MFDataType.Uninitialized:
								prefix = "-";
								break;
							case MFDataType.ACL:
								prefix = "A";
								break;
							case MFDataType.Boolean:
								prefix = "B";
								break;
						}

						// Sanity.
						if (null == prefix || null == suffix)
							return null;

						// Return the formatted value.
						return $"{prefix}{WebUtility.UrlEncode(suffix)}";
					}
				default:
					return null;
			}

		}

		/// <summary>
		/// Compares one folder item to another, for ordering purposes.
		/// </summary>
		/// <param name="x">The first item.</param>
		/// <param name="y">The second item.</param>
		/// <returns>0 means same, -1 means (x &lt; y), 1 means (y &gt; x)</returns>
		private static int CompareTo(this FolderContentItem x, FolderContentItem y)
		{
			// Sanity.
			if (null == x && null == y)
				return 0;
			if (null == x)
				return 1;
			if (null == y)
				return -1;

			// Compare types.
			var typeCompare = FolderContentItemExtensionMethods.Compare(x.FolderContentItemType, y.FolderContentItemType);
			if (typeCompare != 0)
				return typeCompare;

			// Same type; compare name.
			var xName = x.GetDisplayName();
			var yName = y.GetDisplayName();
			return String.Compare(xName, yName, StringComparison.OrdinalIgnoreCase);
		}
		
		/// <summary>
		/// Compares one folder item type to another, for ordering purposes.
		/// </summary>
		/// <param name="x">The first item.</param>
		/// <param name="y">The second item.</param>
		/// <returns>0 means same, -1 means (x &lt; y), 1 means (y &gt; x)</returns>
		private static int Compare(MFFolderContentItemType x, MFFolderContentItemType y)
		{
			// Are they the same?
			if (x == y)
				return 0;

			switch (x)
			{
				case MFFolderContentItemType.ObjectVersion:
					{
						// Objects always at the end.
						return 1;
					}
				case MFFolderContentItemType.PropertyFolder:
				case MFFolderContentItemType.TraditionalFolder:
				case MFFolderContentItemType.ViewFolder:
					{
						// If y is an object, then put y is greater.
						if (y == MFFolderContentItemType.ObjectVersion)
							return -1;

						// Otherwise they are the same.
						return 0;
					}
				default:
					// Unknown.
					return -1;
			}
		}

	}
}
