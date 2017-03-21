using System;
using MFaaP.MFilesAPI.ExtensionMethods;
using MFilesAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFaaP.MFilesAPI.Tests.ExtensionMethods
{
	[TestClass]
	// ReSharper disable once InconsistentNaming
	public class ISearchConditionsExtensionMethods
	{

		#region AddNotDeletedSearchCondition

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddNotDeletedSearchCondition_Throws_NullSearchConditions()
		{
			((ISearchConditions)null).AddNotDeletedSearchCondition();
		}

		[TestMethod]
		public void AddNotDeletedSearchCondition_CountAtOne()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddNotDeletedSearchCondition();

			// Assert.
			Assert.AreEqual(1, searchConditions.Count);
		}

		[TestMethod]
		public void AddNotDeletedSearchCondition_ConditionCorrect()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddNotDeletedSearchCondition();

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeEqual, condition.ConditionType);
			Assert.AreEqual(MFStatusType.MFStatusTypeDeleted, condition.Expression.DataStatusValueType);
			Assert.AreEqual(MFDataType.MFDatatypeBoolean, condition.TypedValue.DataType);
			Assert.AreEqual("false", condition.TypedValue.GetValueAsUnlocalizedText());
		}

		#endregion

		#region AddObjectTypeIdSearchCondition
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddObjectTypeIdSearchCondition_Throws_NullSearchConditions()
		{
			((ISearchConditions)null).AddObjectTypeIdSearchCondition(1);
		}

		[TestMethod]
		public void AddObjectTypeIdSearchCondition_CountAtOne()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddObjectTypeIdSearchCondition(1);

			// Assert.
			Assert.AreEqual(1, searchConditions.Count);
		}

		[TestMethod]
		public void AddObjectTypeIdSearchCondition_ConditionCorrect()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddObjectTypeIdSearchCondition(1);

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeEqual, condition.ConditionType);
			Assert.AreEqual(MFStatusType.MFStatusTypeObjectTypeID, condition.Expression.DataStatusValueType);
			Assert.AreEqual(MFDataType.MFDatatypeLookup, condition.TypedValue.DataType);
			Assert.AreEqual(1, condition.TypedValue.GetLookupID());
		}

		#endregion

		#region AddMinimumObjectIdSearchCondition

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddMinimumObjectIdSearchCondition_Throws_NullSearchConditions()
		{
			((ISearchConditions)null).AddMinimumObjectIdSearchCondition(1);
		}

		[TestMethod]
		public void AddMinimumObjectIdSearchCondition_CountAtOne()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddMinimumObjectIdSearchCondition(1);

			// Assert.
			Assert.AreEqual(1, searchConditions.Count);
		}

		[TestMethod]
		public void AddMinimumObjectIdSearchCondition_ConditionCorrect()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddMinimumObjectIdSearchCondition(1000);

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeGreaterThanOrEqual, condition.ConditionType);
			Assert.AreEqual(MFStatusType.MFStatusTypeObjectID, condition.Expression.DataStatusValueType);
			Assert.AreEqual(MFDataType.MFDatatypeInteger, condition.TypedValue.DataType);
			Assert.AreEqual("1000", condition.TypedValue.GetValueAsUnlocalizedText());
		}

		#endregion

		#region AddObjectIdSegmentSearchCondition

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddObjectIdSegmentSearchCondition_Throws_NullSearchConditions()
		{
			((ISearchConditions)null).AddObjectIdSegmentSearchCondition(1, 500);
		}

		[TestMethod]
		public void AddObjectIdSegmentSearchCondition_CountAtOne()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddObjectIdSegmentSearchCondition(1, 500);

			// Assert.
			Assert.AreEqual(1, searchConditions.Count);
		}

		[TestMethod]
		public void AddObjectIdSegmentSearchCondition_ConditionCorrect()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddObjectIdSegmentSearchCondition(1, 500);

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeEqual, condition.ConditionType);
			Assert.AreEqual(500, condition.Expression.DataObjectIDSegmentSegmentSize);
			Assert.AreEqual(MFExpressionType.MFExpressionTypeObjectIDSegment, condition.Expression.Type);
			Assert.AreEqual(MFDataType.MFDatatypeInteger, condition.TypedValue.DataType);
			Assert.AreEqual("1", condition.TypedValue.GetValueAsUnlocalizedText());
		}

		#endregion

		#region AddDisplayIdSearchCondition

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddDisplayIdSearchCondition_Throws_NullSearchConditions()
		{
			((ISearchConditions)null).AddDisplayIdSearchCondition("hello");
		}

		[TestMethod]
		public void AddDisplayIdSearchCondition_CountAtOne()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddDisplayIdSearchCondition("hello");

			// Assert.
			Assert.AreEqual(1, searchConditions.Count);
		}

		[TestMethod]
		public void AddDisplayIdSearchCondition_ConditionCorrect()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddDisplayIdSearchCondition("hello");

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeEqual, condition.ConditionType);
			Assert.AreEqual(MFStatusType.MFStatusTypeExtID, condition.Expression.DataStatusValueType);
			Assert.AreEqual(MFDataType.MFDatatypeText, condition.TypedValue.DataType);
			Assert.AreEqual("hello", condition.TypedValue.DisplayValue);
		}

		#endregion

		#region AddFullTextSearchCondition

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddFullTextSearchCondition_Throws_NullSearchConditions()
		{
			((ISearchConditions)null).AddFullTextSearchCondition("hello");
		}

		[TestMethod]
		public void AddFullTextSearchCondition_CountAtOne()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddFullTextSearchCondition("hello");

			// Assert.
			Assert.AreEqual(1, searchConditions.Count);
		}

		[TestMethod]
		public void AddFullTextSearchCondition_ConditionCorrect()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddFullTextSearchCondition("hello");

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeContains, condition.ConditionType);
			Assert.AreEqual(MFExpressionType.MFExpressionTypeAnyField, condition.Expression.Type);
			Assert.AreEqual(MFDataType.MFDatatypeText, condition.TypedValue.DataType);
			Assert.AreEqual(MFFullTextSearchFlags.MFFullTextSearchFlagsLookInFileData | MFFullTextSearchFlags.MFFullTextSearchFlagsLookInMetaData, condition.Expression.DataAnyFieldFTSFlags);
			Assert.AreEqual("hello", condition.TypedValue.DisplayValue);
		}

		[TestMethod]
		public void AddFullTextSearchCondition_ConditionCorrect_Flags_MetaDataOnly()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddFullTextSearchCondition("hello", MFFullTextSearchFlags.MFFullTextSearchFlagsLookInMetaData);

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeContains, condition.ConditionType);
			Assert.AreEqual(MFExpressionType.MFExpressionTypeAnyField, condition.Expression.Type);
			Assert.AreEqual(MFDataType.MFDatatypeText, condition.TypedValue.DataType);
			Assert.AreEqual(MFFullTextSearchFlags.MFFullTextSearchFlagsLookInMetaData, condition.Expression.DataAnyFieldFTSFlags);
			Assert.AreEqual("hello", condition.TypedValue.DisplayValue);
		}

		[TestMethod]
		public void AddFullTextSearchCondition_ConditionCorrect_Flags_FileDataOnly()
		{
			// Arrange.
			var searchConditions = new SearchConditions();

			// Act.
			searchConditions.AddFullTextSearchCondition("hello", MFFullTextSearchFlags.MFFullTextSearchFlagsLookInFileData);

			// Assert.
			var condition = searchConditions[1];
			Assert.IsNotNull(condition);
			Assert.AreEqual(MFConditionType.MFConditionTypeContains, condition.ConditionType);
			Assert.AreEqual(MFExpressionType.MFExpressionTypeAnyField, condition.Expression.Type);
			Assert.AreEqual(MFDataType.MFDatatypeText, condition.TypedValue.DataType);
			Assert.AreEqual(MFFullTextSearchFlags.MFFullTextSearchFlagsLookInFileData, condition.Expression.DataAnyFieldFTSFlags);
			Assert.AreEqual("hello", condition.TypedValue.DisplayValue);
		}

		#endregion

	}
}
