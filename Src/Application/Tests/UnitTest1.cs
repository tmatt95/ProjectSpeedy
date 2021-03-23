using Moq;
using NUnit.Framework;
using ProjectSpeedy.Services;
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
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var test = new ProjectSpeedy.Services.Bet(mockTest.Object);
            Assert.Pass();
        }
    }
}