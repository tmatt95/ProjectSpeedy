using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProjectSpeedy.Controllers;

namespace Tests.Controllers
{
    [TestFixture]
    public class Project
    {
        private Mock<ILogger<ProjectController>> _logger;

        private ProjectSpeedy.Services.IProject _projectService;

        private ProjectSpeedy.Controllers.ProjectController _controller;

        [SetUp]
        public void init()
        {
            this._logger = new Mock<ILogger<ProjectController>>();
        }

        [Test]
        public async System.Threading.Tasks.Task GetNotfound()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectDataExceptionNotFound();
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService);        

            // Act
            var test = await this._controller.GetAsync("ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as NotFoundResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task GetNotfoundOther()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectDataExceptionNotFoundOther();
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService);        

            // Act
            var test = await this._controller.GetAsync("ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task GetException()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectDataException();
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService); 

            // Act
            var test = await this._controller.GetAsync("ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task Put()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService); 

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Project.ProjectNew(){
                Name = "New Project Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as AcceptedResult;
            Assert.AreEqual(202, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutNoForm()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectDataException();
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService); 

            // Act
            var test = await this._controller.PutAsync(null);

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as BadRequestObjectResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutSameNameAsOtherProject()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService); 

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Project.ProjectNew(){
                Name = "Project Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as BadRequestObjectResult;
            Assert.AreEqual(400, result.StatusCode);
        }

         [Test]
        public async System.Threading.Tasks.Task PutException()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectDataException();
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService); 

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Project.ProjectNew(){
                Name = "Project Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}