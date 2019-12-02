using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
//using RestSharp.Extensions.MonoHttp;

namespace MFaaP.MFWSClient.ExtensionMethods
{
	/// <summary>
	/// URI builder extension methods.
	/// </summary>
	public static class UriBuilderExtensionMethods
	{
		/// <summary>
		/// Sets the specified query parameter key-value pair of the URI only if the value is not null and not whitespace.
		/// If the key already exists, the value is overwritten.
		/// </summary>
		public static UriBuilder SetQueryParamIfNotNullOrWhitespace(this UriBuilder uri, string key, string value)
		{
			// Call the other method if appropriate.
			if (false == string.IsNullOrWhiteSpace(value))
				uri.SetQueryParam(key, value);

			// Return the builder for chaining.
			return uri;
		}

		/// <summary>
		/// Sets the specified query parameter key-value pair of the URI.
		/// If the key already exists, the value is overwritten.
		/// </summary>
		public static UriBuilder SetQueryParam(this UriBuilder uri, string key, string value)
		{
			var collection = uri.ParseQuery();

			// add (or replace existing) key-value pair
			collection.Set(key, value);

			string query = collection
				.AsKeyValuePairs()
				.ToConcatenatedString(pair =>
					pair.Key == null
					? pair.Value
					: pair.Key + "=" + pair.Value, "&");

			uri.Query = query;

			return uri;
		}

		/// <summary>
		/// Gets the query string key-value pairs of the URI.
		/// Note that the one of the keys may be null ("?123") and
		/// that one of the keys may be an empty string ("?=123").
		/// </summary>
		public static IEnumerable<KeyValuePair<string, string>> GetQueryParams(
			this UriBuilder uri)
		{
			return uri.ParseQuery().AsKeyValuePairs();
		}

		/// <summary>
		/// Gets the query string key-value pairs of the URI.
		/// Note that the one of the keys may be null ("?123") and
		/// that one of the keys may be an empty string ("?=123").
		/// </summary>
		public static Dictionary<string, string> GetQueryParamsDictionary(
			this UriBuilder uri)
		{
			return uri.ParseQuery().AsDictionary();
		}

		/// <summary>
		/// Converts the legacy NameValueCollection into a strongly-typed KeyValuePair sequence.
		/// </summary>
		static IEnumerable<KeyValuePair<string, string>> AsKeyValuePairs(this NameValueCollection collection)
		{
			foreach (string key in collection.AllKeys)
			{
				yield return new KeyValuePair<string, string>(key, collection.Get(key));
			}
		}

		/// <summary>
		/// Converts the legacy NameValueCollection into a strongly-typed KeyValuePair sequence.
		/// </summary>
		static Dictionary<string, string> AsDictionary(this NameValueCollection collection)
		{
			var dictionary = new Dictionary<string, string>();
			foreach (string key in collection.AllKeys)
			{
				dictionary.Add(key, collection.Get(key));
			}
			return dictionary;
		}

		/// <summary>
		/// Parses the query string of the URI into a NameValueCollection.
		/// </summary>
		static NameValueCollection ParseQuery(this UriBuilder uri)
		{
			return HttpUtility.ParseQueryString(uri.Query);
		}
	}
}
