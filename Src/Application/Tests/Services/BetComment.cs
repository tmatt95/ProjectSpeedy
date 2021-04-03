using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class BetComment
    {
        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var betService = new ProjectSpeedy.Services.BetComment(mockTest.Object);
            var form = new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate()
            {
                Comment = "Comment"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.BetComment.BetCommentNewUpdate>(), ProjectSpeedy.Services.BetComment.PARTITION))
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
            var betService = new ProjectSpeedy.Services.BetComment(mockTest.Object);
            var form = new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate()
            {
                Comment = "Comment"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.BetComment.BetCommentNewUpdate>(), ProjectSpeedy.Services.BetComment.PARTITION))
                .Returns(Task.FromResult(""));

            // Act
            var test = await betService.CreateAsync("ProjectId", "ProblemId", "BetId", form);

            // Assert
            Assert.AreEqual(false, test);
        }
    }
}