using System.IO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using ProjectSpeedy.Controllers;

namespace Tests.Controllers
{
    [TestFixture]
    public class BetFeedback
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private Mock<ILogger<BetFeedbackController>> _logger;

        /// <summary>
        /// Contains services needed to interact with problems.
        /// </summary>
        private ProjectSpeedy.Services.IProblem _problemServices;

        /// <summary>
        /// Contains services needed to interact with bet feedback.
        /// </summary>
        private ProjectSpeedy.Services.IBetFeedback _betFeedbackService;

        private ProjectSpeedy.Controllers.BetFeedbackController _controller;

        [SetUp]
        public void init()
        {
            this._logger = new Mock<ILogger<BetFeedbackController>>();
        }

        /// <summary>
        /// Tries to add feedback but not supplying a form
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutNoForm()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackData();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", null);

                    // Assert
                    var result = test as BadRequestResult;
                    Assert.AreEqual(400, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add feedback with the wrong project Id.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutBadProjectId()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackData();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectIdWrong", "ProblemId", "BetId", new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate());

                    // Assert
                    var result = test as NotFoundResult;
                    Assert.AreEqual(404, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add feedback with the wrong problem Id.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutBadProblemId()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFound();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackData();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate());

                    // Assert
                    var result = test as NotFoundResult;
                    Assert.AreEqual(404, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add feedback with the wrong bet Id.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutBadBetId()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackData();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetIdWrong", new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate());

                    // Assert
                    var result = test as NotFoundResult;
                    Assert.AreEqual(404, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add feedback successfully.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutOk()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackData();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate(){
                        Comment="Test comment"
                    });

                    // Assert
                    var result = test as AcceptedResult;
                    Assert.AreEqual(202, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add feedback but does not get a success response from the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutProblem()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemData();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackDataNoCreate();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate(){
                        Comment="Test comment"
                    });

                    // Assert
                    var result = test as ObjectResult;
                    Assert.AreEqual(500, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add feedback but gets a non 404 exception from the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutExceptionServiceWebOther()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemDataNotFoundOther();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackData();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate(){
                        Comment="Test comment"
                    });

                    // Assert
                    var result = test as ObjectResult;
                    Assert.AreEqual(500, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add feedback but gets an exception thrown from the service.
        /// </summary>
        /// <returns>Unit test task</returns>
        [Test]
        public async System.Threading.Tasks.Task PutExceptionService()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamBets = new MemoryStream())
                {
                    // Arrange
                    this._problemServices = new ProjectSpeedy.Tests.ServicesTests.ProblemDataException();
                    this._betFeedbackService = new ProjectSpeedy.Tests.ServicesTests.BetFeedbackData();
                    this._controller = new ProjectSpeedy.Controllers.BetFeedbackController(this._logger.Object, this._betFeedbackService, this._problemServices);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate(){
                        Comment="Test comment"
                    });

                    // Assert
                    var result = test as ObjectResult;
                    Assert.AreEqual(500, result.StatusCode);
                }
            }
        }
    }
}