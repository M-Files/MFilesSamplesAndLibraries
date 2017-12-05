using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFaaP.MFWSClient.Tests
{
	/// <summary>
	/// Abstract class containing a <see cref="TestContext"/> for use by the testing framework.
	/// </summary>
	public abstract class TestBase
	{
		public TestContext TestContext { get; set; }
	}
}
