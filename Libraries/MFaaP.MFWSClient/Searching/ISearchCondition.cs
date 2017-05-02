namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a single search condition.
	/// </summary>
	/// <remarks>See http://www.m-files.com/mfws/syntax.html#sect:search-encoding .</remarks>
	public interface ISearchCondition
	{
		/// <summary>
		/// The expression to use in the search.
		/// </summary>
		/// <remarks>See http://www.m-files.com/mfws/syntax.html#sect:search-encoding .</remarks>
		string Expression { get; }

		/// <summary>
		/// Whether to invert the <see cref="Operator"/>.
		/// </summary>
		/// <remarks>See http://www.m-files.com/mfws/syntax.html#sect:search-encoding .</remarks>
		bool InvertOperator { get; set; }

		/// <summary>
		/// The operator to use in the search.
		/// </summary>
		/// <remarks>See http://www.m-files.com/mfws/syntax.html#sect:search-encoding .</remarks>
		SearchConditionOperators Operator { get; set; }

		/// <summary>
		/// Gets the encoded value as a string for use in the search.
		/// </summary>
		/// <remarks>See http://www.m-files.com/mfws/syntax.html#sect:search-encoding .</remarks>
		string EncodedValue { get; }
	}

	/// <summary>
	/// Represents a single search condition.
	/// Used alongside <see cref="ISearchCondition"/> to provide a strongly-typed value.
	/// </summary>
	/// <remarks>See http://www.m-files.com/mfws/syntax.html#sect:search-encoding .</remarks>
	public interface ISearchCondition<T>
		: ISearchCondition
	{
		/// <summary>
		/// The (raw) value to use in the search.
		/// </summary>
		/// <remarks>See http://www.m-files.com/mfws/syntax.html#sect:search-encoding .</remarks>
		T Value { get; set; }
	}
}
