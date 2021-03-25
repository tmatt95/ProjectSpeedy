using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class Project
    {
        [Test]
        public async System.Threading.Tasks.Task TestCreateValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);
            var form = new ProjectSpeedy.Models.Project.ProjectNew()
            {
                Name = "Test Project"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Project.Project>(), "project"))
                .Returns(Task.FromResult("TestNewId"));

            // Act
            var test = await projectService.CreateAsync(form);

            // Assert
            Assert.AreEqual(test, true);
        }

        [Test]
        public async System.Threading.Tasks.Task TestCreateInValidAsync()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);
            var form = new ProjectSpeedy.Models.Project.ProjectNew()
            {
                Name = "Test Project"
            };
            mockTest.Setup(d => d.DocumetCreate(It.IsAny<ProjectSpeedy.Models.Project.Project>(), "project"))
                .Returns(Task.FromResult(""));

            // Act
            var test = await projectService.CreateAsync(form);

            // Assert
            Assert.AreEqual(test, false);
        }

        // Get all no data
        [Test]
        public async System.Threading.Tasks.Task GetAllNoDataAsync()
        {
            using (var stream = new MemoryStream())
            {
                // General set up
                var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
                var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);

                // Creates the fake response
                await JsonSerializer.SerializeAsync(stream, new ProjectSpeedy.Models.CouchDb.View.ViewResult()
                {
                    rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>()
                });
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                string content = await reader.ReadToEndAsync();

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StringContent(content);
                mockTest.Setup(d => d.ViewGet("project", "projects", "projects", "", ""))
                    .Returns(Task.FromResult(response.Content));

                // Act
                var test = await projectService.GetAll();

                // Assert
                Assert.IsInstanceOf<ProjectSpeedy.Models.Projects.ProjectsView>(test);
                Assert.IsNotNull(test.rows);
                Assert.AreEqual(test.rows.Count, 0);
            }
        }

        // Get all projects
        [Test]
        public async System.Threading.Tasks.Task GetAllProjects()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);

            // Creates the fake response
            string content = JsonSerializer.Serialize(new ProjectSpeedy.Models.CouchDb.View.ViewResult()
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

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(content);
            mockTest.Setup(d => d.ViewGet("project", "projects", "projects", "", ""))
                .Returns(Task.FromResult(response.Content));

            // Act
            var test = await projectService.GetAll();

            // Assert
            Assert.IsInstanceOf<ProjectSpeedy.Models.Projects.ProjectsView>(test);
            Assert.IsNotNull(test.rows);
            Assert.AreEqual(test.rows.Count, 1);
        }

        // Get project
        [Test]
        public async System.Threading.Tasks.Task GetProject()
        {
            // Arrange
            var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
            var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);

            // Creates the fake response
            HttpResponseMessage responseView = new HttpResponseMessage();
            string contentView = JsonSerializer.Serialize(new ProjectSpeedy.Models.CouchDb.View.ViewResult(){
                rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>(){
                    new ProjectSpeedy.Models.CouchDb.View.ListItem(){
                        id="recordId",
                        key="recordKey",
                        value = new ProjectSpeedy.Models.CouchDb.View.ListItemValue(){
                            name = "Problem Name",
                            id="problem:ProblemId"
                        }
                    }
                }
            });
            responseView.Content = new StringContent(contentView);

            HttpResponseMessage response = new HttpResponseMessage();
            string content = JsonSerializer.Serialize(new ProjectSpeedy.Models.Project.Project(){
                Name="Project Name",
                Description="Project Description"
            });
            response.Content = new StringContent(content);
            mockTest.Setup(d => d.DocumentGet(It.IsAny<string>()))
                .Returns(Task.FromResult(response.Content));

            mockTest.Setup(d => d.ViewGet(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()))
                .Returns(Task.FromResult(responseView.Content));

            // Act
            var test = await projectService.Get("ProjectId");

            // Assert
            Assert.IsInstanceOf<ProjectSpeedy.Models.Project.Project>(test);
            Assert.IsNotNull(test);
            Assert.AreEqual("Project Name", test.Name);
        }
    }
}