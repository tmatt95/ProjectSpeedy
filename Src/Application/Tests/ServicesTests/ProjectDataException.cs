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
        public async Task<bool> CreateAsync(Models.Project.ProjectNew form)
        {
            await Task.FromResult(new System.Exception("Exception"));
            return await Task.FromResult(false);
        }

        /// <inheritdoc />
        public bool Delete(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ProjectsView> GetAll()
        {
            await Task.FromResult(true);
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public async Task<ProjectSpeedy.Models.Project.Project> Get(string projectId)
        {
            await Task.FromResult(new System.Exception("Exception"));
            return new Models.Project.Project();
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Project.ProjectUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}