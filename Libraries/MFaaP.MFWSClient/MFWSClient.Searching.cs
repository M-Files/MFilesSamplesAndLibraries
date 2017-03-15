using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Extensions.MonoHttp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Searches the vault for a simple text string.
		/// </summary>
		/// <param name="searchTerm">The string to search for.</param>
		/// <param name="objectTypeId">If provided, also restricts the results by the given object type.</param>
		/// <returns>An array of items that match the search term.</returns>
		/// <remarks>For more comprehensive search options, construct a series of <see cref="ISearchCondition"/> objects and use the <see cref="Search"/> method.</remarks>
		public ObjectVersion[] QuickSearch(string searchTerm, int? objectTypeId = null)
		{
			// Create a collection of conditions.
			var conditions = new List<ISearchCondition>
			{
				// Add our quick search condition.
				new QuickSearchCondition(searchTerm)
			};

			// If we were given an object type Id then add it too.
			if (objectTypeId.HasValue)
			{
				conditions.Add(new ObjectTypeSearchCondition(objectTypeId.Value));
			}

			// Search.
			return this.Search(conditions.ToArray());
		}

		/// <summary>
		/// Searches the vault.
		/// </summary>
		/// <param name="searchConditions">The conditions to search for.</param>
		/// <returns>An array of items that match the search conditions.</returns>
		public ObjectVersion[] Search(params ISearchCondition[] searchConditions)
		{
			// Sanity.
			if(null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the request.
			var request = new RestRequest("/REST/objects");

			// Append the conditions.
			request.Resource += "?";
			foreach (var searchCondition in searchConditions)
			{
				// Sanity.
				if (null == searchCondition)
					continue;

				// Build up the search condition as detailed at http://www.m-files.com/mfws/syntax.html#sect:search-encoding

				// Add on the expression.
				request.Resource += HttpUtility.UrlEncode(searchCondition.Expression);

				// Add the operator (parsed from the enumerated value).
				request.Resource += GetSearchConditionOperator(searchCondition);

				// Add the value.  Handle the null special-case.
				request.Resource += null == searchCondition.EncodedValue ? "%00" : HttpUtility.UrlEncode(searchCondition.EncodedValue);

				// We need to separate conditions with ampersands.
				request.Resource += "&";
			}

			// Strip last ampersand if needed.
			if (request.Resource.EndsWith("&"))
				request.Resource = request.Resource.Substring(0, request.Resource.Length - 1);

			// Strip last "?" if needed.
			if (request.Resource.EndsWith("?"))
				request.Resource = request.Resource.Substring(0, request.Resource.Length - 1);

			// Make the request and get the response.
			var response = this.Get<Results<ObjectVersion>>(request);

			// Return the data.
			return response.Data?.Items?.ToArray();
		}

		/// <summary>
		/// Returns the "operator" for the given search condition.
		/// </summary>
		/// <param name="searchCondition">The condition to search on.</param>
		/// <returns>The operator, including a flag if it should be inverted.</returns>
		protected virtual string GetSearchConditionOperator(ISearchCondition searchCondition)
		{
			// Sanity.
			if(null == searchCondition)
				throw new ArgumentNullException(nameof(searchCondition));

			// Set the default value.  If we are to invert, add a "!", otherwise blank string.
			string returnValue = searchCondition.InvertOperator ? "!" : "";

			// Build the operator based on the search condition.
			switch (searchCondition.Operator)
			{
				case SearchConditionOperators.Unknown:
					throw new NotImplementedException($"The operator {searchCondition.Operator} cannot be used.");
				case SearchConditionOperators.Equals:
					returnValue += "=";
					break;
				case SearchConditionOperators.LessThan:
					returnValue += "<<=";
					break;
				case SearchConditionOperators.LessThanOrEqual:
					returnValue += "<=";
					break;
				case SearchConditionOperators.GreaterThan:
					returnValue += ">>=";
					break;
				case SearchConditionOperators.GreaterThanOrEqual:
					returnValue += ">=";
					break;
				case SearchConditionOperators.MatchesWildcard:
					returnValue += "**=";
					break;
				case SearchConditionOperators.Contains:
					returnValue += "*=";
					break;
				case SearchConditionOperators.StartsWith:
					returnValue += "^=";
					break;
				default:
					throw new NotImplementedException($"The operator {searchCondition.Operator} is not supported.");
			}

			// Return the operator.
			return returnValue;

		}

	}
	
}
