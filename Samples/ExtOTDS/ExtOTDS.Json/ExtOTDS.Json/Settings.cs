using System.Collections.Generic;
using System.Linq;

namespace ExtOTDS.Json
{
	/// <summary>
	/// Represents settings, typically stored as a string in key-value notation, e.g.
	/// "key1=value1;key2=value2".
	/// </summary>
	public class Settings
		: Dictionary<string, string>
	{
		private readonly object _lock = new object();

		/// <summary>
		/// The default string to use for separating pairs of key-values.
		/// </summary>
		public static string PairSeparator = ";";

		/// <summary>
		/// The default string to use for separating keys from values.
		/// </summary>
		public static string KeyValueSeparator = "=";

		/// <summary>
		/// Adds an item to the dictionary, or updates it if the key already exists.
		/// </summary>
		/// <param name="key">The key for the item in the dictionary.</param>
		/// <param name="value">The value for the item in the dictionary.</param>
		public void AddOrUpdate(string key, string value)
		{
			lock (this._lock)
			{
				if (this.ContainsKey(key))
					this[key] = value;
				else
					this.Add(key, value);
			}
		}

		/// <summary>
		/// Updates the <see cref="Settings"/> object from a string of key-value-pairs.
		/// </summary>
		/// <param name="input">The settings string (e.g. "key1=value1;key2=value2").</param>
		/// <param name="pairSeparator">The separator for each pair of values (e.g. in "key1=value1;key2=value2" it would be ";").</param>
		/// <param name="keyValueSeparator">The separator between the key and the value (e.g. in "key1=value1;key2=value2" it would be "=").</param>
		/// <param name="clearExistingSettings">If true, clears existing settings from the dictionary first.</param>
		/// <returns>The settings as a dictionary.  Note that keys are converted to upper-case for matching purposes.</returns>
		public void UpdateFromString(
			string input,
			string pairSeparator = null,
			string keyValueSeparator = null,
			bool clearExistingSettings = true)
		{
			// Are we clearing the existing settings?
			if(clearExistingSettings)
				this.Clear();

			// Sanity.
			input = input ?? "";
			pairSeparator = pairSeparator ?? Settings.PairSeparator;
			keyValueSeparator = keyValueSeparator ?? Settings.KeyValueSeparator;

			// Iterate over the input and pull out the settings.
			// Split by the pair separator (e.g. "key1=value1;key2=value2" into "key1=value1" and "key2=value2").
			foreach (var pairString in input.Split(pairSeparator.ToCharArray()))
			{
				// Split by the key-value separator (e.g. "key1=value1" into "key1" and "value1").
				// Note that flag keys may occur with no value. (e.g. "key1=value1;ForceRefresh;key2=value2").
				var pair = pairString.Split(keyValueSeparator.ToCharArray());

				// What was the key?
				// Convert to upper case and trim it, to be sure we can match later.
				var key = pair[0].ToUpper().Trim();

				// How many items did we retrieve?
				switch (pair.Length)
				{
					case 0: // Shouldn't happen; we should always have at least one.
						continue;
					case 1:
						// A flag, e.g. "ForceRefresh", with no value.
						this.AddOrUpdate(key, null);
						continue;
					case 2:
						// A key-value-pair.
						this.AddOrUpdate(key, pair[1]);
						continue;
					default:
						// Fall back to assuming later values are part of the string
						// (e.g. "key1=value1=value2" should have a value of "value1=value2").
						this.AddOrUpdate(key, string.Join(keyValueSeparator, pair.Skip(1)));
						continue;
				}
			}
		}

		/// <summary>
		/// Creates a <see cref="Settings"/> object from a string of key-value-pairs.
		/// </summary>
		/// <param name="input">The settings string (e.g. "key1=value1;key2=value2").</param>
		/// <param name="pairSeparator">The separator for each pair of values (e.g. in "key1=value1;key2=value2" it would be ";").</param>
		/// <param name="keyValueSeparator">The separator between the key and the value (e.g. in "key1=value1;key2=value2" it would be "=").</param>
		/// <returns>The settings as a dictionary.  Note that keys are converted to upper-case for matching purposes.</returns>
		public static Settings ParseFromString(
			string input,
			string pairSeparator = null,
			string keyValueSeparator = null)
		{
			// Create a settings object.
			var settings = new Settings();

			// Update it from the supplied data.
			settings.UpdateFromString(input, pairSeparator, keyValueSeparator);

			// Return it.
			return settings;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.ToString(Settings.PairSeparator, Settings.KeyValueSeparator);
		}

		/// <summary>
		/// Converts the dictionary of key-value pairs to a string representation.
		/// </summary>
		/// <param name="pairSeparator">The separator for each pair of values (e.g. in "key1=value1;key2=value2" it would be ";").</param>
		/// <param name="keyValueSeparator">The separator between the key and the value (e.g. in "key1=value1;key2=value2" it would be "=").</param>
		/// <returns>The string representation.</returns>
		public string ToString(string pairSeparator, string keyValueSeparator)
		{
			return string.Join(pairSeparator, this.Select((kvp) => $"{kvp.Key}{keyValueSeparator}{kvp.Value}"));
		}
	}
}
