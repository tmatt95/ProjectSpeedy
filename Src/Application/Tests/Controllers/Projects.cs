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

        private Mock<ProjectSpeedy.Services.Project> _projectService;

        private ProjectSpeedy.Controllers.ProjectsController _controller;

        [SetUp]
        public void init()
        {
            this._serviceBase = new Mock<ProjectSpeedy.Services.IServiceBase>();
            this._logger = new Mock<ILogger<ProjectsController>>();
            this._projectService = new Mock<ProjectSpeedy.Services.Project>(this._serviceBase.Object);
            this._controller = new ProjectSpeedy.Controllers.ProjectsController(this._logger.Object, this._projectService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task GetAllAsync()
        {
            using (var stream = new MemoryStream())
            {
                // Arrange
                await JsonSerializer.SerializeAsync(stream, new ProjectSpeedy.Models.CouchDb.View.ViewResult()
                {
                    total_rows = 1,
                    offset = 0,
                    rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>(){
                        new ProjectSpeedy.Models.CouchDb.View.ListItem(){
                            id= "ProjectId",
                            value= new ProjectSpeedy.Models.CouchDb.View.ListItemValue(){
                                id= "project:e5273e69704d8c4ee3f8b50c6500d053",
                                name = "Project Name"
                            }
                        }
                    }
                });
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                string content = await reader.ReadToEndAsync();

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StringContent(content);
                this._serviceBase.Setup(d => d.GetView("project", "projects", "projects", "", ""))
                    .Returns(Task.FromResult(response.Content));

                // Act
                var test = await this._controller.GetAsync();

                // Assert
                // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
                var result = test.Result as OkObjectResult;
                Assert.IsNotNull(result.Value);
                Assert.AreEqual(((ProjectSpeedy.Models.Projects.ProjectsView) result.Value).rows.Count, 1);
            }
        }

        [Test]
        public async System.Threading.Tasks.Task GetAllProblemAsync()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.GetView("project", "projects", "projects", "", ""))
                .Throws(new HttpRequestException("test",new System.Exception("test"), System.Net.HttpStatusCode.NotFound));

            // Act
            var test = await this._controller.GetAsync();

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}