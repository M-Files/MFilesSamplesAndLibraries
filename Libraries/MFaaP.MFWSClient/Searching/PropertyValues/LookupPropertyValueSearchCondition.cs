namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a search condition that restricts by a lookup property value.
	/// </summary>
	public class LookupPropertyValueSearchCondition
		: PropertyValueSearchConditionBase<string>
	{
		/// <summary>
		/// Creates a <see cref="LookupPropertyValueSearchCondition"/>, searching for the lookup value supplied.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property def to search by.</param>
		/// <param name="lookupValueId">The Id of the lookup value to search by.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		public LookupPropertyValueSearchCondition(
			int propertyDefId,
			int lookupValueId,
			SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals,
			bool invertOperator = false)
			: base(propertyDefId, lookupValueId.ToString(), searchConditionOperator, invertOperator)
		{
		}

		/// <summary>
		/// Creates a <see cref="LookupPropertyValueSearchCondition"/>, searching for the lookup value supplied.
		/// Note that this constructor allows you to provide an external Id (or display Id).  This is used ONLY when objects are
		/// loaded from an external system: http://www.m-files.com/user-guide/latest/eng/#Connection_to_external_database.html.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property def to search by.</param>
		/// <param name="externalLookupValueId">The Id of the lookup value IN THE EXTERNAL SYSTEM.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		public LookupPropertyValueSearchCondition(
			int propertyDefId,
			string externalLookupValueId,
			SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals,
			bool invertOperator = false)
			: base(propertyDefId, "e" + externalLookupValueId, searchConditionOperator, invertOperator)
		{
		}
	}
}