using ProjectSpeedy.Models.Projects;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All project related services.
    /// </summary>
    public class Project : IProject
    {
        /// <inheritdoc />
        public bool Create(Models.Project.ProjectUpdate form)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Delete(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ProjectsView GetAll()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Project.ProjectUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}