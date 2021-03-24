using System.Net.Http;
using System.Threading.Tasks;
using ProjectSpeedy.Models.Projects;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// A project with dummy data
    /// </summary>
    public class ProjectDataExceptionNotFoundOther : IProject
    {
        /// <inheritdoc />
        public Task<bool> CreateAsync(Models.Project.ProjectNew form)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.Forbidden);
        }

        /// <inheritdoc />
        public bool Delete(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ProjectsView> GetAll()
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.Forbidden);
        }

        /// <inheritdoc />
        public Task<ProjectSpeedy.Models.Project.Project> Get(string projectId)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.Forbidden);
        }

        /// <inheritdoc />
        public Task<bool> Update(string projectId, Models.Project.ProjectUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}