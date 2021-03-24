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

        private Mock<ProjectSpeedy.Services.IServiceBase> _serviceBase;

        private Mock<ProjectSpeedy.Services.Project> _projectService;

        private ProjectSpeedy.Controllers.ProjectController _controller;

        [SetUp]
        public void init()
        {
            this._serviceBase = new Mock<ProjectSpeedy.Services.IServiceBase>();
            this._logger = new Mock<ILogger<ProjectController>>();
            this._projectService = new Mock<ProjectSpeedy.Services.Project>(this._serviceBase.Object);
            this._controller = new ProjectSpeedy.Controllers.ProjectController(this._logger.Object, this._projectService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task GetNotfound()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.GetDocument("project:ProjectId"))
                .Throws(new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound));

            // Act
            var test = await this._controller.GetAsync("ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as NotFoundResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(result.StatusCode, 404);
        }

        [Test]
        public async System.Threading.Tasks.Task GetException()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.GetDocument("project:ProjectId"))
                .Throws(new System.Exception("Exception"));

            // Act
            var test = await this._controller.GetAsync("ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}