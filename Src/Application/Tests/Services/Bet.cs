using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class Bet
    {
        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var betService = new ProjectSpeedy.Services.Bet(mockTest.Object);
            var form = new ProjectSpeedy.Models.Bet.BetNew()
            {
                Name = "Test Bet"
            };
            mockTest.Setup(d => d.DocumentCreate(It.IsAny<ProjectSpeedy.Models.Bet.Bet>(), "bet"))
                .Returns(Task.FromResult("TestNewId"));

            // Act
            var test = await betService.CreateAsync("ProjectId", "ProblemId", form);

            // Assert
            Assert.AreEqual(true, test);
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateInValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var betService = new ProjectSpeedy.Services.Bet(mockTest.Object);
            var form = new ProjectSpeedy.Models.Bet.BetNew()
            {
                Name = "Test Bet"
            };
            mockTest.Setup(d => d.DocumentCreate(It.IsAny<ProjectSpeedy.Models.Bet.Bet>(), "bet"))
                .Returns(Task.FromResult(""));

            // Act
            var test = await betService.CreateAsync("ProjectId", "ProblemId", form);

            // Assert
            Assert.AreEqual(false, test);
        }

        [Test]
        public async System.Threading.Tasks.Task TestGetValid()
        {
            // Arrange
            // Bet Response data.
            HttpResponseMessage responseBet = new HttpResponseMessage();
            responseBet.Content = new StringContent(JsonSerializer.Serialize(new ProjectSpeedy.Models.Bet.Bet(){
                Name="Bet Name"
            }));

            // Sets up mocks for response.
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var betService = new ProjectSpeedy.Services.Bet(mockTest.Object);
            mockTest.Setup(d => d.DocumentGet(It.IsAny<string>()))
            .Returns(Task.FromResult(responseBet.Content));

            // Act
            var test = await betService.GetAsync("ProjectId", "ProblemId", "BetId");

            // Assert
            Assert.NotNull(test);
            Assert.AreEqual("Bet Name", test.Name);
        }
    }
}