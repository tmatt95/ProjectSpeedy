using System;
using System.Threading.Tasks;
using ProjectSpeedy.Models.Projects;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All project related services.
    /// </summary>
    public class Project : IProject
    {
        /// <summary>
        /// Contains helper functions needed for all services to work
        /// </summary>
        private IServiceBase _serviceBase;

        /// <summary>
        /// All project related services.
        /// </summary>
        /// <param name="serviceBase">Contains helper functions needed for all services to work.</param>
        public Project(IServiceBase serviceBase)
        {
            this._serviceBase = serviceBase;
        }

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
        public async Task<ProjectsView> GetAll()
        {
            var test = await this._serviceBase.DocumetCreate(new Models.Project.ProjectView()
            {
                Name = "Project Name",
                Created = DateTime.UtcNow
            }, "project");

            return new ProjectsView();
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Project.ProjectUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}