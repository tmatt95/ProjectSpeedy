using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class Problem
    {
        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var problemService = new ProjectSpeedy.Services.Problem(mockTest.Object);
            var form = new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Test Problem"
            };
            mockTest.Setup(d => d.DocumentCreate(It.IsAny<ProjectSpeedy.Models.Problem.ProblemNew>(), "problem"))
                .Returns(Task.FromResult("TestNewId"));

            // Act
            var test = await problemService.CreateAsync("ProjectId", form);

            // Assert
            Assert.AreEqual(true, test);
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateInValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var problemService = new ProjectSpeedy.Services.Problem(mockTest.Object);
            var form = new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = "Test Problem New"
            };
            mockTest.Setup(d => d.DocumentCreate(It.IsAny<ProjectSpeedy.Models.Problem.Problem>(), "problem"))
                .Returns(Task.FromResult(""));

            // Act
            var test = await problemService.CreateAsync("ProjectId", form);

            // Assert
            Assert.AreEqual(false, test);
        }

        // Get project
        [Test]
        public async System.Threading.Tasks.Task GetProblem()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var problemService = new ProjectSpeedy.Services.Problem(mockTest.Object);

            // Creates the fake response
            HttpResponseMessage responseView = new HttpResponseMessage();
            string contentView = JsonSerializer.Serialize(new ProjectSpeedy.Models.CouchDb.View.ViewResult(){
                rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>(){
                    new ProjectSpeedy.Models.CouchDb.View.ListItem(){
                        id="recordId",
                        key="recordKey",
                        value = new ProjectSpeedy.Models.CouchDb.View.ListItemValue(){
                            name = "Bet Name",
                            id="bet:BetId",
                            status="Not Started"
                        }
                    }
                }
            });
            responseView.Content = new StringContent(contentView);

            HttpResponseMessage response = new HttpResponseMessage();
            string content = JsonSerializer.Serialize(new ProjectSpeedy.Models.Project.Project(){
                Name="Problem Name",
                Description="Problem Description"
            });
            response.Content = new StringContent(content);
            mockTest.Setup(d => d.DocumentGet(It.IsAny<string>()))
                .Returns(Task.FromResult(response.Content));

            mockTest.Setup(d => d.ViewGet(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()))
                .Returns(Task.FromResult(responseView.Content));

            // Act
            var test = await problemService.GetAsync("ProjectId","ProblemId");

            // Assert
            Assert.IsInstanceOf<ProjectSpeedy.Models.Problem.Problem>(test);
            Assert.IsNotNull(test);
            Assert.AreEqual("Problem Name", test.Name);
        }

        [Test]
        public async System.Threading.Tasks.Task TestUpdateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var problemService = new ProjectSpeedy.Services.Problem(mockTest.Object);

            // Form to go into function
            var form = new ProjectSpeedy.Models.Problem.ProblemUpdate()
            {
                Name = "Test Problem",
                Description = "Description"
            };

            // Response from document get
            HttpResponseMessage response = new HttpResponseMessage();
            string content = JsonSerializer.Serialize(new ProjectSpeedy.Models.Problem.Problem(){
                Name="Problem Name",
                Description="Problem Description"
            });
            response.Content = new StringContent(content);
            mockTest.Setup(d => d.DocumentGet(It.IsAny<string>()))
                .Returns(Task.FromResult(response.Content));

            // Response from view get
            HttpResponseMessage responseView = new HttpResponseMessage();
            string contentView = JsonSerializer.Serialize(new ProjectSpeedy.Models.CouchDb.View.ViewResult(){
                rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>()
            });
            responseView.Content = new StringContent(contentView);
            mockTest.Setup(d => d.ViewGet(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()))
                .Returns(Task.FromResult(responseView.Content));

            // Response from document update
            mockTest.Setup(d => d.DocumentUpdate(It.IsAny<string>(),It.IsAny<ProjectSpeedy.Models.Problem.Problem>()))
                .Returns(Task.FromResult(true));

            // Act
            var test = await problemService.UpdateAsync("ProjectId", "ProblemId", form);

            // Assert
            Assert.AreEqual(true, test);
        }
    }
}