using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProjectSpeedy.Controllers;

namespace Tests.Controllers
{
    [TestFixture]
    public class Projects
    {
        private Mock<ILogger<ProjectsController>> _logger;

        private Mock<ProjectSpeedy.Services.IServiceBase> _serviceBase;

        private ProjectSpeedy.Services.IProject _projectService;

        private ProjectSpeedy.Controllers.ProjectsController _controller;

        [SetUp]
        public void init()
        {
            this._serviceBase = new Mock<ProjectSpeedy.Services.IServiceBase>();
            this._logger = new Mock<ILogger<ProjectsController>>();
        }

        [Test]
        public async System.Threading.Tasks.Task GetAllAsync()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._controller = new ProjectSpeedy.Controllers.ProjectsController(this._logger.Object, this._projectService);
            
            // Act
            var test = await this._controller.GetAsync();

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as OkObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProjectSpeedy.Models.Projects.ProjectsView) result.Value).rows.Count, 1);
        }

        [Test]
        public async System.Threading.Tasks.Task GetAllProblemAsync()
        {
            // Throws an error when calling the view
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectDataException();
            this._controller = new ProjectSpeedy.Controllers.ProjectsController(this._logger.Object, this._projectService);

            // Act
            var test = await this._controller.GetAsync();

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}