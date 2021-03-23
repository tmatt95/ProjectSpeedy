using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class Project
    {
        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);
            var form = new ProjectSpeedy.Models.Project.ProjectNew()
            {
                Name = "Test Project"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Project.Project>(), "project"))
                .Returns(Task.FromResult("TestNewId"));

            // Act
            var test = await projectService.CreateAsync(form);

            // Assert
            Assert.AreEqual(test, true);
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateInValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);
            var form = new ProjectSpeedy.Models.Project.ProjectNew()
            {
                Name = "Test Project"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Project.Project>(), "project"))
                .Returns(Task.FromResult(""));

            // Act
            var test = await projectService.CreateAsync(form);

            // Assert
            Assert.AreEqual(test, false);
        }
    }
}