using System;
using System.Collections.Generic;
using System.Text;

namespace MFaaP.MFWSClient.ExtensionMethods
{
	/// <summary>
	/// Extension methods for IEnumerable{T}.
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public static class IEnumerableExtensionMethods
	{
		/// <summary>
		/// Creates a string from the sequence by concatenating the result
		/// of the specified string selector function for each element.
		/// </summary>
		public static string ToConcatenatedString<T>(this IEnumerable<T> source,
			Func<T, string> stringSelector)
		{
			return source.ToConcatenatedString(stringSelector, String.Empty);
		}

		///  <summary>
		///  Creates a string from the sequence by concatenating the result
		///  of the specified string selector function for each element.
		///  </summary>
		///  <param name="stringSelector">A function describing how to select a string from the source.</param>
		///  <param name="separator">The string which separates each concatenated item.</param>
		///  <param name="source">The source collection to enumerate.</param>
		public static string ToConcatenatedString<T>(this IEnumerable<T> source,
			Func<T, string> stringSelector,
			string separator)
		{
			var b = new StringBuilder();
			bool needsSeparator = false; // don't use for first item

			foreach (var item in source)
			{
				if (needsSeparator)
					b.Append(separator);

				b.Append(stringSelector(item));
				needsSeparator = true;
			}

			return b.ToString();
		}
	}
}
