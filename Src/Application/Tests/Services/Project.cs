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
            // Arrange
            using (var stream = new MemoryStream())
            {
                // General set up
                var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
                var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);
                
                // Creates the fake response
                await JsonSerializer.SerializeAsync(stream, new ProjectSpeedy.Models.CouchDb.View.ViewResult(){
                    rows = new List<ProjectSpeedy.Models.CouchDb.View.ListItem>()
                });
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                string content = await reader.ReadToEndAsync();
                
                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StringContent(content);
                mockTest.Setup(d => d.GetView("project", "projects", "projects", "", ""))
                    .Returns(Task.FromResult(response.Content));

                // Act
                var test = await projectService.GetAll();

                // Assert
                Assert.IsInstanceOf<ProjectSpeedy.Models.Projects.ProjectsView>(test);
                Assert.IsNotNull(test.rows);
                Assert.AreEqual(test.rows.Count, 0);
            }
        }

        // Get all has projects
        [Test]
        public async System.Threading.Tasks.Task GetAllHasProjects()
        {
            // Arrange
            using (var stream = new MemoryStream())
            {
                // General set up
                var mockTest = new Mock<ProjectSpeedy.Services.IServiceBase>();
                var projectService = new ProjectSpeedy.Services.Project(mockTest.Object);
                
                // Creates the fake response
                await JsonSerializer.SerializeAsync(stream, new ProjectSpeedy.Models.CouchDb.View.ViewResult(){
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
                mockTest.Setup(d => d.GetView("project", "projects", "projects", "", ""))
                    .Returns(Task.FromResult(response.Content));

                // Act
                var test = await projectService.GetAll();

                // Assert
                Assert.IsInstanceOf<ProjectSpeedy.Models.Projects.ProjectsView>(test);
                Assert.IsNotNull(test.rows);
                Assert.AreEqual(test.rows.Count, 1);
            }
        }

    }
}