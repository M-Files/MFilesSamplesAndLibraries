using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// An enumeration of the supported operators (from http://www.m-files.com/mfws/syntax.html#sect:search-encoding).
	/// </summary>
	[Serializable]
	public enum SearchConditionOperators
	{
		/// <summary>
		/// The default operator; note this throws an exception if used.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Equals. ("=")
		/// </summary>
		Equals = 1,

		/// <summary>
		/// Less than. ("&lt;&lt;=")
		/// </summary>
		LessThan = 2,

		/// <summary>
		/// Less than or equal to. (";&lt;=")
		/// </summary>
		LessThanOrEqual = 3,

		/// <summary>
		/// Greater than. ("&gt;&gt;=")
		/// </summary>
		GreaterThan = 4,

		/// <summary>
		/// Greater than or equal to. ("&gt;=")
		/// </summary>
		GreaterThanOrEqual = 5,

		/// <summary>
		/// Matches (including wildcards). ("**=")
		/// </summary>
		MatchesWildcard = 6,

		/// <summary>
		/// Contains. ("*=")
		/// </summary>
		Contains = 7,

		/// <summary>
		/// Starts with. ("^=")
		/// </summary>
		StartsWith = 8
	}
}