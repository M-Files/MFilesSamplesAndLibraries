using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonDataProvider.Tests
{
	[TestClass]
	public class DataProviderRepository
	{
		[TestMethod]
		public void UserDataProvider()
		{
			Assert.IsNotNull(new JsonDataProvider.DataProviderRepository().GetDataProvider<User>());
		}
		[TestMethod]
		public void PostDataProvider()
		{
			Assert.IsNotNull(new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>());
		}
		[TestMethod]
		public void CommentDataProvider()
		{
			Assert.IsNotNull(new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>());
		}
	}
}
