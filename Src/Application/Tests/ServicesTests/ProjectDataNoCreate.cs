using System.Net.Http;
using System.Threading.Tasks;
using ProjectSpeedy.Models.Projects;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// A project with dummy data
    /// </summary>
    public class ProjectDataNoCreate : IProject
    {
        /// <inheritdoc />
        public Task<bool> CreateAsync(Models.Project.ProjectNew form)
        {
            return Task.FromResult(false);
        }

        /// <inheritdoc />
        public bool Delete(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ProjectsView> GetAll()
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public Task<ProjectSpeedy.Models.Project.Project> Get(string projectId)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public Task<bool> Update(string projectId, Models.Project.ProjectUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}