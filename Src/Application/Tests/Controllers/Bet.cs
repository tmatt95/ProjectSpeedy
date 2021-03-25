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

        private ProjectSpeedy.Services.IProblem _problemService;

        private ProjectSpeedy.Services.IBet _betService;

        private ProjectSpeedy.Controllers.BetController _controller;

        [SetUp]
        public void init()
        {
            this._logger = new Mock<ILogger<BetController>>();
        }

        [Test]
        public async System.Threading.Tasks.Task GetOk()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.Bet();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            var test = await this._controller.GetAsync("ProjectId","ProblemId","BetId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as OkObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task GetWrongId()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.Bet();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            var test = await this._controller.GetAsync("ProjectIdWrong","ProblemId","BetId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as NotFoundResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task GetNotfound()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.BetDataNotFound();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId","BetId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as NotFoundResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task GetHttpException()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.BetDataNotFoundOther();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId","BetId");

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
            this._betService = new ProjectSpeedy.Tests.ServicesTests.BetDataException();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            var test = await this._controller.GetAsync("ProblemId","ProjectId","BetId");

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test.Result as ObjectResult;
            Assert.IsNull(test.Value);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutSuccess()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.Bet();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            ActionResult test = await this._controller.PutAsync("ProjectId","ProblemId", new ProjectSpeedy.Models.Bet.BetNew(){
                Name="New Bet Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as AcceptedResult;
            Assert.AreEqual(202, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutNotCreated()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.BetDataNoCreate();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            ActionResult test = await this._controller.PutAsync("ProjectId","ProblemId", new ProjectSpeedy.Models.Bet.BetNew(){
                Name="New Bet Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutNotFound()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.BetDataNoCreate();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFound();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            ActionResult test = await this._controller.PutAsync("ProjectId","ProblemId", new ProjectSpeedy.Models.Bet.BetNew(){
                Name="New Bet Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutHttpException()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.BetDataNoCreate();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFoundOther();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            ActionResult test = await this._controller.PutAsync("ProjectId","ProblemId", new ProjectSpeedy.Models.Bet.BetNew(){
                Name="New Bet Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutException()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.BetDataNoCreate();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemDataException();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            ActionResult test = await this._controller.PutAsync("ProjectId","ProblemId", new ProjectSpeedy.Models.Bet.BetNew(){
                Name="New Bet Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutNoForm()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.Bet();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            ActionResult test = await this._controller.PutAsync("ProjectId","ProblemId", null);

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as BadRequestResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async System.Threading.Tasks.Task PutWrongId()
        {
            // Arrange
            this._betService = new ProjectSpeedy.Tests.ServicesTests.Bet();
            this._problemService = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
            this._controller = new ProjectSpeedy.Controllers.BetController(this._logger.Object, this._betService, this._problemService);

            // Act
            ActionResult test = await this._controller.PutAsync("ProjectIdWrong","ProblemId", new ProjectSpeedy.Models.Bet.BetNew(){
                Name="New Bet Name"
            });

            // Assert
            // Taken from https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
            var result = test as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}