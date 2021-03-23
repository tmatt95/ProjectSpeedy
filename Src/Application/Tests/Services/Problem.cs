using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class Problem
    {
        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var problemService = new ProjectSpeedy.Services.Problem(mockTest.Object);
            var form = new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Test Problem"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Problem.Problem>(), "Problem"))
                .Returns(Task.FromResult("TestNewId"));

            // Act
            var test = await problemService.CreateAsync("ProjectId", form);

            // Assert
            Assert.AreEqual(test, true);
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateInValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var problemService = new ProjectSpeedy.Services.Problem(mockTest.Object);
            var form = new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Test Problem"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Project.Problem>(), "Problem"))
                .Returns(Task.FromResult(""));

            // Act
            var test = await problemService.CreateAsync("ProjectId", form);

            // Assert
            Assert.AreEqual(test, false);
        }
    }
}