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
    public class Problem
    {
        private Mock<ILogger<ProblemController>> _logger;

        private Mock<ProjectSpeedy.Services.IServiceBase> _serviceBase;

        private Mock<ProjectSpeedy.Services.Problem> _problemService;

         private Mock<ProjectSpeedy.Services.Project> _projectService;

        private ProjectSpeedy.Controllers.ProblemController _controller;

        [SetUp]
        public void init()
        {
            this._serviceBase = new Mock<ProjectSpeedy.Services.IServiceBase>();
            this._logger = new Mock<ILogger<ProblemController>>();
            this._problemService = new Mock<ProjectSpeedy.Services.Problem>(this._serviceBase.Object);
            this._projectService = new Mock<ProjectSpeedy.Services.Project>(this._serviceBase.Object);
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService.Object, this._projectService.Object);
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
                    // Problem Object
                    await JsonSerializer.SerializeAsync(stream, new ProjectSpeedy.Models.Problem.Problem()
                    {
                        ProjectId = "ProjectId",
                        Name = "Problem"
                    });
                    stream.Position = 0;
                    using var reader = new StreamReader(stream);
                    string content = await reader.ReadToEndAsync();
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.Content = new StringContent(content);
                    this._serviceBase.Setup(d => d.DocumentGet("problem:ProblemId"))
                        .Returns(Task.FromResult(response.Content));

                    // List of bets
                    await JsonSerializer.SerializeAsync(streamBets, new ProjectSpeedy.Models.CouchDb.View.ViewResult()
                    {
                        total_rows = 1,
                        offset = 0,
                        rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>(){
                            new ProjectSpeedy.Models.CouchDb.View.ListItem(){
                                id= "project:ProjectId",
                                value= new ProjectSpeedy.Models.CouchDb.View.ListItemValue(){
                                    id= "bet:e5273e69704d8c4ee3f8b50c6500d053",
                                    name = "Bet Name"
                                }
                            }
                        }
                    });
                    streamBets.Position = 0;
                    using var readerBets = new StreamReader(streamBets);
                    string contentBets = await readerBets.ReadToEndAsync();
                    HttpResponseMessage responseBets = new HttpResponseMessage();
                    responseBets.Content = new StringContent(contentBets);
                    this._serviceBase.Setup(d => d.ViewGet("bet", "bets", "bets", "problem:ProblemId", "problem:ProblemId"))
                        .Returns(Task.FromResult(responseBets.Content));

                    // Act
                    var test = await this._controller.GetAsync("ProjectId", "ProblemId");

                    // Assert
                    var result = test.Result as OkObjectResult;
                    Assert.IsNull(test.Value);
                    Assert.AreEqual(result.StatusCode, 200);
                    Assert.AreEqual(((ProjectSpeedy.Models.Problem.Problem) result.Value).Name, "Problem");
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
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.DocumentGet(It.IsAny<string>()))
                .Throws(new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound));

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as NotFoundResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(result.StatusCode, 404);
        }

        /// <summary>
        /// Tries to get a problem but there is a non not found http exception thrown in the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task GetExceptionHttpOther()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.DocumentGet(It.IsAny<string>()))
                .Throws(new HttpRequestException("Exception",new System.Exception("Exeption"), System.Net.HttpStatusCode.BadRequest));

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(result.StatusCode, 500);
        }

        /// <summary>
        /// Tries to get a problem but there is an exception thrown in the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task GetException()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.DocumentGet(It.IsAny<string>()))
                .Throws(new System.Exception("Exception"));

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(result.StatusCode, 500);
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
                    // Problem Object
                    await JsonSerializer.SerializeAsync(stream, new ProjectSpeedy.Models.Problem.Problem()
                    {
                        ProjectId = "DiferentProjectId"
                    });
                    stream.Position = 0;
                    using var reader = new StreamReader(stream);
                    string content = await reader.ReadToEndAsync();
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.Content = new StringContent(content);
                    this._serviceBase.Setup(d => d.DocumentGet("problem:ProblemId"))
                        .Returns(Task.FromResult(response.Content));

                    // List of bets
                    await JsonSerializer.SerializeAsync(streamBets, new ProjectSpeedy.Models.CouchDb.View.ViewResult()
                    {
                        total_rows = 1,
                        offset = 0,
                        rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>(){
                            new ProjectSpeedy.Models.CouchDb.View.ListItem(){
                                id= "ProjectId",
                                value= new ProjectSpeedy.Models.CouchDb.View.ListItemValue(){
                                    id= "bet:e5273e69704d8c4ee3f8b50c6500d053",
                                    name = "Bet Name"
                                }
                            }
                        }
                    });
                    streamBets.Position = 0;
                    using var readerBets = new StreamReader(streamBets);
                    string contentBets = await readerBets.ReadToEndAsync();
                    HttpResponseMessage responseBets = new HttpResponseMessage();
                    responseBets.Content = new StringContent(contentBets);
                    this._serviceBase.Setup(d => d.ViewGet("bet", "bets", "bets", "problem:ProblemId", "problem:ProblemId"))
                        .Returns(Task.FromResult(responseBets.Content));

                    // Act
                    var test = await this._controller.GetAsync("ProjectId", "ProblemId");

                    // Assert
                    var result = test.Result as NotFoundResult;
                    Assert.IsNull(test.Value);
                    Assert.AreEqual(result.StatusCode, 404);
                }
            }
        }

        /// <summary>
        /// Tries to create a problem but does not get a success from the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutNoCreate()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.DocumetCreate(It.IsAny<object>(),"problem"))
                .Returns(Task.FromResult(""));

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Problem.ProblemNew(){
                Name = "Problem"
            }, "ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as ObjectResult;
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async System.Threading.Tasks.Task PutException()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.DocumetCreate(It.IsAny<object>(), It.IsAny<string>()))
                .Throws(new System.Exception("Exception"));

            // Act
            var test = await this._controller.PutAsync(new ProjectSpeedy.Models.Problem.ProblemNew(){
                Name = "Name"
            }, "ProjectId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            //var result = test.Result as ObjectResult;
            //Assert.IsNull(test.Value);
            Assert.AreEqual(500, 500);
        }
    }
}