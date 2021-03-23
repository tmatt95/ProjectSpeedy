using NUnit.Framework;
using ProjectSpeedy.Services;
using Moq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var IServiceBase = new Mock<IServiceBase>();
            var delete = new Bet(IServiceBase.Object);
            Assert.Pass();
        }
    }
}