using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();

            var betService = new ProjectSpeedy.Services.Bet(mockTest.Object);
            var form = new ProjectSpeedy.Models.Bet.BetNew()
            {
                Name = "Test Bet"
            };

            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Bet.Bet>(), "bet"))
                .Returns(Task.FromResult("TestNewId"));

            Assert.AreEqual(await betService.CreateAsync("ProjectId","ProblemId",form), true);
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateInValidAsync()
        {
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();

            var betService = new ProjectSpeedy.Services.Bet(mockTest.Object);
            var form = new ProjectSpeedy.Models.Bet.BetNew()
            {
                Name = "Test Bet"
            };

            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Bet.Bet>(), "bet"))
                .Returns(Task.FromResult(""));

            Assert.AreEqual(await betService.CreateAsync("ProjectId","ProblemId",form), false);
        }
    }
}