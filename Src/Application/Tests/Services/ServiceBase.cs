using System.Collections.Generic;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{
    public class ServiceBase
    {
        [Test]
        public async System.Threading.Tasks.Task GenerateIdOk()
        {
            // Arrange
            var config = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var httpHandler = new ProjectSpeedy.Tests.ServicesTests.HttpHandlerCreate();
            var serviceBase = new ProjectSpeedy.Services.ServiceBase(config.Object, httpHandler);

            // Act
            var test = await serviceBase.GenerateId();

            // Assert
            Assert.AreEqual("NewId", test);
        }

        [Test]
        public async System.Threading.Tasks.Task GetDocumentOk()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"couchdb:document_get", "{DocumentId"}
            };
            Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var httpHandler = new ProjectSpeedy.Tests.ServicesTests.HttpHandlerCreate();
            var serviceBase = new ProjectSpeedy.Services.ServiceBase(configuration, httpHandler);

            // Act
            var test = await serviceBase.DocumentGet("DocumentId");

            // Assert
            Assert.NotNull(test);
        }

        [Test]
        public async System.Threading.Tasks.Task GetViewOkNoKeys()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"couchdb:view_get", "{partition}{designDocumentName}{viewName}"},
                {"couchdb:view_get_keys", "{partition}{designDocumentName}{viewName}{startKey}{endKey}"}
            };
            Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var httpHandler = new ProjectSpeedy.Tests.ServicesTests.HttpHandlerCreate();
            var serviceBase = new ProjectSpeedy.Services.ServiceBase(configuration, httpHandler);

            // Act
            var test = await serviceBase.ViewGet("Partition", "DesignDocumentName", "ViewName");

            // Assert
            Assert.NotNull(test);
        }

         [Test]
        public async System.Threading.Tasks.Task GetViewOkKeys()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"couchdb:view_get", "{partition}{designDocumentName}{viewName}"},
                {"couchdb:view_get_keys", "{partition}{designDocumentName}{viewName}{startKey}{endKey}"}
            };
            Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var httpHandler = new ProjectSpeedy.Tests.ServicesTests.HttpHandlerCreate();
            var serviceBase = new ProjectSpeedy.Services.ServiceBase(configuration, httpHandler);

            // Act
            var test = await serviceBase.ViewGet("Partition", "DesignDocumentName", "ViewName", "StartKey", "EndKey");

            // Assert
            Assert.NotNull(test);
        }
    }
}