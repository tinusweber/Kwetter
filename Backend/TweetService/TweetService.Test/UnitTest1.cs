using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetService.DomainModel;

namespace TweetService.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = new Tweet(new System.Guid(), null);
        }
    }
}