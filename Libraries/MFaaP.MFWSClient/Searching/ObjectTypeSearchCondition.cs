using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a search condition that restricts by object type.
	/// </summary>
	[Serializable]
	public class ObjectTypeSearchCondition
		: SearchConditionBase<int>
	{
		/// <summary>
		/// Creates a <see cref="ObjectTypeSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type to retrieve.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		public ObjectTypeSearchCondition(int objectTypeId, SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals, bool invertOperator = false)
			: base("o", searchConditionOperator, objectTypeId, invertOperator)
		{
		}

		/// <inheritdoc />
		public override string EncodedValue => this.Value.ToString();
	}
}