using System.IO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using ProjectSpeedy.Controllers;

namespace Tests.Controllers
{
    [TestFixture]
    public class Problem
    {
        private Mock<ILogger<ProblemController>> _logger;

        private ProjectSpeedy.Services.IProblem _problemService;

        private ProjectSpeedy.Services.IProject _projectService;

        private ProjectSpeedy.Controllers.ProblemController _controller;

        [SetUp]
        public void init()
        {
            this._logger = new Mock<ILogger<ProblemController>>();
        }

        /// <summary>
        /// Tries to get a problem successfully.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task Get()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
                    this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
                    this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

                    // Act
                    var test = await this._controller.GetAsync("ProjectId", "ProblemId");

                    // Assert
                    var result = test.Result as OkObjectResult;
                    Assert.IsNull(test.Value);
                    Assert.AreEqual(200, result.StatusCode);
                    Assert.AreEqual(((ProjectSpeedy.Models.Problem.Problem)result.Value).Name, "Problem Name");
                }
            }
        }

        /// <summary>
        /// Tries to get a problem which does not exist.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task GetNotfound()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFound();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.GetAsync("ProblemId", "ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as NotFoundResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(404, result.StatusCode);
        }

        /// <summary>
        /// Tries to get a problem but there is a non not found http exception thrown in the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task GetExceptionHttpOther()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFoundOther();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.GetAsync("ProblemId", "ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(500, result.StatusCode);
        }

        /// <summary>
        /// Tries to get a problem but there is an exception thrown in the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task GetException()
        {
            // Assert
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataException();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.GetAsync("ProjectId", "ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(500, result.StatusCode);
        }

        /// <summary>
        /// Tries to get a problem but passes a different project id to the controller to the one it was expecting.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task GetInvalidIds()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
                    this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
                    this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

                    // Act
                    var test = await this._controller.GetAsync("ProjectIdOther", "ProblemId");

                    // Assert
                    var result = test.Result as NotFoundResult;
                    Assert.IsNull(test.Value);
                    Assert.AreEqual(404, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to create a problem successfully.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task Put()
        {
            // Throws an error when calling the view
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "New Problem"
            }, "ProjectId");

            // Assert
            var result = test as ObjectResult;
            Assert.AreEqual(202, result.StatusCode);
        }

        /// <summary>
        /// Tries to create a problem but does not get a success from the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutNoCreate()
        {
            // Throws an error when calling the view
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNoCreate();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Problem"
            }, "ProjectId");

            // Assert
            var result = test as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutException()
        {

            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataException();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Name"
            }, "ProjectId");

            // Assert
            var result = test as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutExceptionHttp()
        {

            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFound();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Name"
            }, "ProjectId");

            // Assert
            var result = test as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutProblemSameName()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Problem Name"
            }, "ProjectId");

            // Assert
            var result = test as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutProblemNoForm()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PutAsync(null, "ProjectId");

            // Assert
            var result = test as BadRequestResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PostProblem()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PostAsync(new ProjectSpeedy.Models.Problem.ProblemUpdate(){
                Name= "Problem Name",
                Description="Added Description"
            }, "ProjectId", "ProblemId");

            // Assert
            var result = test as AcceptedResult;
            Assert.AreEqual(202, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PostProblemException()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataException();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PostAsync(new ProjectSpeedy.Models.Problem.ProblemUpdate(){
                Name="Name",
                Description="Desc"
            }, "ProjectId", "ProblemId");

            // Assert
            var result = test as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PostProblemNoForm()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PostAsync(null, "ProjectId", "ProblemId");

            // Assert
            var result = test as BadRequestResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PostProblemNoProblemFound()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFound();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PostAsync(new ProjectSpeedy.Models.Problem.ProblemUpdate(){
                Name= "Updated Problem Name"
            }, "ProjectId", "ProblemId");

            // Assert
            var result = test as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PostProblemBadProjectId()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PostAsync(new ProjectSpeedy.Models.Problem.ProblemUpdate(){
                Name= "Updated Problem Name",
            }, "ProjectIdDifferent", "ProblemId");

            // Assert
            var result = test as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PostProblemDuplicateName()
        {
            // Arrange
            this._projectService = new ProjectSpeedy.Tests.ServicesTests.ProjectData();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService, this._projectService);

            // Act
            var test = await this._controller.PostAsync(new ProjectSpeedy.Models.Problem.ProblemUpdate(){
                Name= "Problem Name",
            }, "ProjectId", "ProblemIdDif");

            // Assert
            var result = test as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}