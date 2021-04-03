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
    }
}