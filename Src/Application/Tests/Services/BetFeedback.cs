using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class BetFeedback
    {
        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var betService = new ProjectSpeedy.Services.BetFeedback(mockTest.Object);
            var form = new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate()
            {
                Comment = "feedback"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate>(), ProjectSpeedy.Services.BetFeedback.PARTITION))
                .Returns(Task.FromResult("TestNewId"));

            // Act
            var test = await betService.CreateAsync("ProjectId", "ProblemId", "BetId", form);

            // Assert
            Assert.AreEqual(true, test);
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateNoSaveAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var betService = new ProjectSpeedy.Services.BetFeedback(mockTest.Object);
            var form = new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate()
            {
                Comment = "Comment"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate>(), ProjectSpeedy.Services.BetFeedback.PARTITION))
                .Returns(Task.FromResult(""));

            // Act
            var test = await betService.CreateAsync("ProjectId", "ProblemId", "BetId", form);

            // Assert
            Assert.AreEqual(false, test);
        }
    }
}