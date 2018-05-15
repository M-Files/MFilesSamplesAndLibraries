using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// A base implementation of <see cref="ISearchCondition"/>.
	/// </summary>
	[Serializable]
	public abstract class SearchConditionBase
		: ISearchCondition
	{
		/// <inheritdoc />
		public virtual string Expression { get; }

		/// <inheritdoc />
		public bool InvertOperator { get; set; }

		/// <inheritdoc />
		public SearchConditionOperators Operator { get; set; }

		/// <inheritdoc />
		public abstract string EncodedValue { get; }

		/// <summary>
		/// Creates a <see cref="SearchConditionBase"/> with the supplied
		/// basic information.
		/// </summary>
		/// <param name="expression">The expression (querystring parameter name).</param>
		/// <param name="op">The operator (e.g. equals) to use for the match.</param>
		/// <param name="invertOperator">Whether to invert the operator (true) or not (false).</param>
		protected SearchConditionBase(
			string expression,
			SearchConditionOperators op,
			bool invertOperator = false)
		{
			this.Expression = expression;
			this.InvertOperator = invertOperator;
			this.Operator = op;
		}
	}

	/// <summary>
	/// A base implementation of <see cref="ISearchCondition{T}"/>.
	/// </summary>
	[Serializable]
	public abstract class SearchConditionBase<T>
		: SearchConditionBase, ISearchCondition<T>
	{
		/// <inheritdoc />
		public T Value { get; set; }

		/// <summary>
		/// Creates a <see cref="SearchConditionBase"/> with the supplied
		/// basic information.
		/// </summary>
		/// <param name="expression">The expression (querystring parameter name).</param>
		/// <param name="op">The operator (e.g. equals) to use for the match.</param>
		/// <param name="value">The typed value to match.</param>
		/// <param name="invertOperator">Whether to invert the operator (true) or not (false).</param>
		protected SearchConditionBase(
			string expression,
			SearchConditionOperators op,
			T value,
			bool invertOperator = false)
			: base(expression, op, invertOperator)
		{
			this.Value = value;
		}
	}
}