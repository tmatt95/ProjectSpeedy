using System;
using System.Text.Json;
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
            var viewData = await this._serviceBase.GetView("projects", "project", "projects");
            using var responseStream = await viewData.ReadAsStreamAsync();
            var projectsView = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.Projects.ProjectsView>(responseStream);
            return projectsView;
        }

        /// <inheritdoc />
        public async Task<ProjectSpeedy.Models.Project.Project> Get(string projectId)
        {
            // Id of the project in couchDb.
            var couchProjectId = "project:" + projectId;

            // Gets the base project.
            var viewData = await this._serviceBase.GetDocument(couchProjectId);
            using var responseStream = await viewData.ReadAsStreamAsync();
            var project = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.Project.Project>(responseStream);

            // Populates the list of linked problems.
            var problemData = await this._serviceBase.GetView("problem", "problems", "problems", couchProjectId, couchProjectId);
            using var responseStreamProblems = await problemData.ReadAsStreamAsync();
            project.Problems = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.General.ViewResult>(responseStreamProblems);
            return project;
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Project.ProjectUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}