using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonDataProvider.Tests
{
	[TestClass]
	public class DataProvider_Users
	{
		[TestMethod]
		public void GetItems_NotNull()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<User>();

			// Act.
			var users = repository.GetAll();

			// Assert.
			Assert.IsNotNull(users);

		}

		[TestMethod]
		public void GetItems_Contains10Items()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<User>();

			// Act.
			var users = repository.GetAll().ToList();

			// Assert.
			Assert.AreEqual(10, users.Count);

		}

		[TestMethod]
		public void GetItems_FirstItem_IdCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<User>();

			// Act.
			var user = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual(1, user.Id);

		}

		[TestMethod]
		public void GetItems_FirstItem_NameCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<User>();

			// Act.
			var user = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("Leanne Graham", user.Name);

		}

		[TestMethod]
		public void GetItems_FirstItem_UsernameCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<User>();

			// Act.
			var user = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("Bret", user.Username);

		}

		[TestMethod]
		public void GetItems_FirstItem_EmailAddressCorrect()
		{
			// Arrange.
			var repository = new JsonDataProvider.DataProviderRepository().GetDataProvider<User>();

			// Act.
			var user = repository.GetAll().ToList()[0];

			// Assert.
			Assert.AreEqual("Sincere@april.biz", user.EmailAddress);

		}
	}
}
