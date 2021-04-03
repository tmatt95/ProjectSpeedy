using System.IO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using ProjectSpeedy.Controllers;

namespace Tests.Controllers
{
    [TestFixture]
    public class BetComment
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private Mock<ILogger<BetCommentController>> _logger;

        /// <summary>
        /// Contains services needed to interact with problems.
        /// </summary>
        private ProjectSpeedy.Services.IProblem _problemServices;

        /// <summary>
        /// Contains services needed to interact with bet comments.
        /// </summary>
        private ProjectSpeedy.Services.IBetComment _betCommentService;

        private ProjectSpeedy.Controllers.BetCommentController _controller;

        [SetUp]
        public void init()
        {
            this._logger = new Mock<ILogger<BetCommentController>>();
        }

        /// <summary>
        /// Tries to add a comment but not supplying a form
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
                    this._betCommentService = new ProjectSpeedy.Tests.ServicesTests.BetCommentData();
                    this._controller = new ProjectSpeedy.Controllers.BetCommentController(this._logger.Object, this._problemServices, this._betCommentService);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", null);

                    // Assert
                    var result = test as BadRequestResult;
                    Assert.AreEqual(400, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add a comment with the wrong project Id.
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
                    this._betCommentService = new ProjectSpeedy.Tests.ServicesTests.BetCommentData();
                    this._controller = new ProjectSpeedy.Controllers.BetCommentController(this._logger.Object, this._problemServices, this._betCommentService);

                    // Act
                    var test = await this._controller.PutAsync("ProjectIdWrong", "ProblemId", "BetId", new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate());

                    // Assert
                    var result = test as NotFoundResult;
                    Assert.AreEqual(404, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add a comment with the wrong problem Id.
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
                    this._betCommentService = new ProjectSpeedy.Tests.ServicesTests.BetCommentData();
                    this._controller = new ProjectSpeedy.Controllers.BetCommentController(this._logger.Object, this._problemServices, this._betCommentService);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate());

                    // Assert
                    var result = test as NotFoundResult;
                    Assert.AreEqual(404, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add a comment with the wrong bet Id.
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
                    this._betCommentService = new ProjectSpeedy.Tests.ServicesTests.BetCommentData();
                    this._controller = new ProjectSpeedy.Controllers.BetCommentController(this._logger.Object, this._problemServices, this._betCommentService);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetIdWrong", new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate());

                    // Assert
                    var result = test as NotFoundResult;
                    Assert.AreEqual(404, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add a comment successfully.
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
                    this._betCommentService = new ProjectSpeedy.Tests.ServicesTests.BetCommentData();
                    this._controller = new ProjectSpeedy.Controllers.BetCommentController(this._logger.Object, this._problemServices, this._betCommentService);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate(){
                        Comment="Test comment"
                    });

                    // Assert
                    var result = test as AcceptedResult;
                    Assert.AreEqual(202, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tries to add a comment successfully.
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
                    this._betCommentService = new ProjectSpeedy.Tests.ServicesTests.BetCommentDataNoCreate();
                    this._controller = new ProjectSpeedy.Controllers.BetCommentController(this._logger.Object, this._problemServices, this._betCommentService);

                    // Act
                    var test = await this._controller.PutAsync("ProjectId", "ProblemId", "BetId", new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate(){
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