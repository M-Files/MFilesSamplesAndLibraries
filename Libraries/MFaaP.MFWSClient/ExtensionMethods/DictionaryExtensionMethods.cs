using System;
using System.Collections.Generic;

namespace MFaaP.MFWSClient.ExtensionMethods
{
	/// <summary>
	/// Dictionary extension methods.
	/// </summary>
	public static class DictionaryExtensionMethods
	{
		/// <summary>
		/// Gets the value from the dictionary, if the key exists.
		/// If the key does not exist then returns a default value.
		/// </summary>
		/// <typeparam name="TA">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TB">The type of the values in the dictionary.</typeparam>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="key">The key to look up.</param>
		/// <param name="defaultValue">The default value to return if the key cannot be found.</param>
		/// <returns>The value in the dictionary with the supplied key, or a default value if not found.</returns>
		public static TB GetValueOrDefault<TA, TB>(this Dictionary<TA, TB> dictionary, TA key, TB defaultValue = default(TB))
		{
			// Sanity.
			if(null == dictionary)
				throw new ArgumentNullException(nameof(dictionary));

			// If it has it then return it, otherwise default.
			return dictionary.ContainsKey(key)
				? dictionary[key]
				: defaultValue;
		}
	}
}
