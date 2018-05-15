using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a search condition that restricts by a text property value.
	/// </summary
	[Serializable]
	public class NumericPropertyValueSearchCondition
		: TextPropertyValueSearchCondition
	{
		/// <summary>
		/// Creates a <see cref="NumericPropertyValueSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property def to search by.</param>
		/// <param name="value">The value to search by.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		public NumericPropertyValueSearchCondition(
			int propertyDefId,
			int value,
			SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals,
			bool invertOperator = false)
			: base(propertyDefId, value.ToString(), searchConditionOperator, invertOperator)
		{
		}

		/// <summary>
		/// Creates a <see cref="NumericPropertyValueSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property def to search by.</param>
		/// <param name="value">The value to search by.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		public NumericPropertyValueSearchCondition(
			int propertyDefId,
			long value,
			SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals,
			bool invertOperator = false)
			: base(propertyDefId, value.ToString(), searchConditionOperator, invertOperator)
		{
		}

		/// <summary>
		/// Creates a <see cref="NumericPropertyValueSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property def to search by.</param>
		/// <param name="value">The value to search by.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		public NumericPropertyValueSearchCondition(
			int propertyDefId,
			double value,
			SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals,
			bool invertOperator = false)
			: base(propertyDefId, value.ToString(), searchConditionOperator, invertOperator)
		{
		}

		/// <summary>
		/// Creates a <see cref="NumericPropertyValueSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property def to search by.</param>
		/// <param name="value">The value to search by.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		public NumericPropertyValueSearchCondition(
			int propertyDefId,
			float value,
			SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals,
			bool invertOperator = false)
			: base(propertyDefId, value.ToString(), searchConditionOperator, invertOperator)
		{
		}
	}
}