using System.Threading.Tasks;
using ProjectSpeedy.Models.Projects;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// A project with dummy data
    /// </summary>
    public class ProjectDataException : IProject
    {
        /// <inheritdoc />
        public Task<bool> CreateAsync(Models.Project.ProjectNew form)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public bool Delete(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ProjectsView> GetAll()
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public Task<ProjectSpeedy.Models.Project.Project> Get(string projectId)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public Task<bool> Update(string projectId, Models.Project.ProjectUpdate form)
        {
            throw new System.Exception("Exception");
        }
    }
}