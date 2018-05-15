using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a "quick search" search condition.
	/// </summary>
	[Serializable]
	public class QuickSearchCondition
		: SearchConditionBase<string>
	{
		/// <summary>
		/// Creates a <see cref="QuickSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="value">The value to search for.</param>
		public QuickSearchCondition(string value)
			: base("q", SearchConditionOperators.Equals, value)
		{
		}

		/// <inheritdoc />
		public override string EncodedValue => this.Value;
	}
}