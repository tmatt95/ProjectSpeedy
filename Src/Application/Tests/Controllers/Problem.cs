using System.Net.Http;
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

        private ProjectSpeedy.Controllers.ProblemController _controller;

        [SetUp]
        public void init()
        {
            this._serviceBase = new Mock<ProjectSpeedy.Services.IServiceBase>();
            this._logger = new Mock<ILogger<ProblemController>>();
            this._problemService = new Mock<ProjectSpeedy.Services.Problem>(this._serviceBase.Object);
            this._controller = new ProjectSpeedy.Controllers.ProblemController(this._logger.Object, this._problemService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task GetProblemNotfound()
        {
            // Throws an error when calling the view
            this._serviceBase.Setup(d => d.GetDocument("problem:ProblemId"))
                .Throws(new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound));

            // Act
            var test = await this._controller.GetAsync("ProjectId","ProblemId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as NotFoundResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(result.StatusCode, 404);
        }
    }
}