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
        private readonly IServiceBase _serviceBase;

        /// <summary>
        /// All project related services.
        /// </summary>
        /// <param name="serviceBase">Contains helper functions needed for all services to work.</param>
        public Project(IServiceBase serviceBase)
        {
            this._serviceBase = serviceBase;
        }

        /// <inheritdoc />
        public async Task<bool> CreateAsync(Models.Project.ProjectNew form)
        {
            // TODO We need to ensure that there is not another project with the same name.

            // The new project object
            var newProject = new ProjectSpeedy.Models.Project.Project()
            {
                Name = form.Name,
                Created = DateTime.UtcNow
            };

            // Creates the project and checks if the id is returned.
            var newId = await this._serviceBase.DocumetCreate(newProject, "project");
            return !string.IsNullOrWhiteSpace(newId);
        }

        /// <inheritdoc />
        public bool Delete(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ProjectsView> GetAll()
        {
            await this._serviceBase.DocumetCreate(new Models.Project.Project()
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