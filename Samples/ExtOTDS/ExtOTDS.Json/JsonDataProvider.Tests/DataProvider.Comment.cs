using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonDataProvider.Tests
{
	[TestClass]
    public class DataProvider_Comments
	{
		[TestMethod]
		public void GetItems_NotNull()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comments = repository.GetAll();

			// Assert.
			Assert.IsNotNull(comments);

		}

		[TestMethod]
		public void GetItems_Contains500Items()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comments = repository.GetAll().ToList();

			// Assert.
			Assert.AreEqual(500, comments.Count);

		}

		[TestMethod]
		public void GetItems_FirstItem_IdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comment = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual(1, comment.Id);

		}

		[TestMethod]
		public void GetItems_FirstItem_PostIdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comment = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual(1, comment.PostId);

		}

		[TestMethod]
		public void GetItems_FirstItem_NameCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comment = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("id labore ex et quam laborum", comment.Name);

		}

		[TestMethod]
		public void GetItems_FirstItem_EmailCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comment = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("Eliseo@gardner.biz", comment.EmailAddress);

		}

		[TestMethod]
		public void GetItems_FirstItem_BodyCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comment = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium", comment.Body);

		}

		[TestMethod]
		public void GetItems_SecondItem_IdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comment = repository.GetAll().ToList()[1];

			// Assert.
			Assert.AreEqual(2, comment.Id);

		}

		[TestMethod]
		public void GetItems_SecondItem_PostIdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<Comment>();

			// Act.
			var comment = repository.GetAll().ToList()[1];

			// Assert.
			Assert.AreEqual(1, comment.PostId);

		}
	}
}
