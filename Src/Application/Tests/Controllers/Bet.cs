using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProjectSpeedy.Controllers;

namespace Tests.Controllers
{
    [TestFixture]
    public class Bet
    {
        private Mock<ILogger<BetController>> _logger;

        private Mock<ProjectSpeedy.Services.IServiceBase> _serviceBase;

        private Mock<ProjectSpeedy.Services.Problem> _problemService;

        private Mock<ProjectSpeedy.Services.Bet> _betService;

        private ProjectSpeedy.Controllers.BetController _controller;

        [SetUp]
        public void init()
        {
            this._serviceBase = new Mock<ProjectSpeedy.Services.IServiceBase>();
            this._logger = new Mock<ILogger<BetController>>();
            this._problemService = new Mock<ProjectSpeedy.Services.Problem>(this._serviceBase.Object);
            this._betService = new Mock<ProjectSpeedy.Services.Bet>(this._serviceBase.Object);
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService.Object, this._problemService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task GetNotfound()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.GetDocument("bet:BetId"))
                .Throws(new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound));

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId","BetId");

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
            this._serviceBase.Setup(d => d.GetDocument("bet:BetId"))
                .Throws(new System.Exception("Exception"));

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId","BetId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}