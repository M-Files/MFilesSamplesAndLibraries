using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonDataProvider.Tests
{
	[TestClass]
	public class DataProvider_Posts
	{
		[TestMethod]
		public void GetItems_NotNull()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var posts = repository.GetAll();

			// Assert.
			Assert.IsNotNull(posts);

		}

		[TestMethod]
		public void GetItems_Contains100Items()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var posts = repository.GetAll().ToList();

			// Assert.
			Assert.AreEqual(100, posts.Count);

		}

		[TestMethod]
		public void GetItems_FirstItem_IdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var post = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual(1, post.Id);

		}

		[TestMethod]
		public void GetItems_FirstItem_UserIdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var post = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual(1, post.UserId);

		}

		[TestMethod]
		public void GetItems_FirstItem_TitleCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var post = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("sunt aut facere repellat provident occaecati excepturi optio reprehenderit", post.Title);

		}

		[TestMethod]
		public void GetItems_FirstItem_BodyCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var post = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto", post.Body);

		}

		[TestMethod]
		public void GetItems_SecondItem_IdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var post = repository.GetAll().ToList()[1];

			// Assert.
			Assert.AreEqual(2, post.Id);

		}

		[TestMethod]
		public void GetItems_SecondItem_UserIdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Post>();

			// Act.
			var post = repository.GetAll().ToList()[1];

			// Assert.
			Assert.AreEqual(1, post.UserId);

		}
	}
}
