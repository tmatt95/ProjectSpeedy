using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Prefix of id's in couchdb.
        /// </summary>
        public const string PREFIX = "project:";

        /// <summary>
        /// Partitions allow CouchDB to scale better.
        /// </summary>
        public const string PARTITION = "project";

        /// <summary>
        /// Contains a cache of projects so we dont have to do the requests multiple times for them.
        /// </summary>
        private readonly Dictionary<string, Models.Project.Project> _cachedProjects = new Dictionary<string, Models.Project.Project>();

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
            // The new project object
            var newProject = new ProjectSpeedy.Models.Project.ProjectNew()
            {
                Name = form.Name,
                Created = DateTime.UtcNow,
                Description = form.Description
            };

            // Creates the project and checks if the id is returned.
            var newId = await this._serviceBase.DocumetCreate(newProject, Project.PARTITION);
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
            // Gets the data for from the couchdb view.
            var viewData = await this._serviceBase.ViewGet("project", "projects", "projects");
            using var responseStream = await viewData.ReadAsStreamAsync();

            // Converts the couchdb view model to an output class.
            var projects = new ProjectSpeedy.Models.Projects.ProjectsView();
            var rawProjects = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.CouchDb.View.ViewResult>(responseStream);
            foreach (var record in rawProjects.rows)
            {
                projects.rows.Add(new Models.General.ListItem()
                {
                    Id = record.value.id,
                    Name = record.value.name,
                    Address = "/project/" + record.value.id.Replace(Project.PREFIX, "")
                });
            }

            // Orders the projects by name asc
            projects.rows = projects.rows.OrderBy(r => r.Name).ToList();

            // Returns the list of projects.
            return projects;
        }

        /// <inheritdoc />
        public async Task<ProjectSpeedy.Models.Project.Project> Get(string projectId)
        {
            // Checks cache to see if problem is in it.
            if(this._cachedProjects.ContainsKey(projectId)){
                return this._cachedProjects[projectId];
            }

            // Id of the project in couchDb.
            var couchProjectId = "project:" + projectId;

            // Gets the base project.
            var viewData = await this._serviceBase.DocumentGet(couchProjectId);
            using var responseStream = await viewData.ReadAsStreamAsync();
            var project = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.Project.Project>(responseStream);

            // Populates the list of linked problems.
            var problemData = await this._serviceBase.ViewGet("problem", "problems", "problems", couchProjectId, couchProjectId);
            using var responseStreamProblems = await problemData.ReadAsStreamAsync();

            var rawProblems = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.CouchDb.View.ViewResult>(responseStreamProblems);
            foreach (var record in rawProblems.rows)
            {
                project.Problems.Add(new Models.General.ListItem()
                {
                    Id = record.value.id,
                    Name = record.value.name,
                    Address = "/project/" + projectId + "/" + record.value.id.Replace(Problem.PREFIX, "")
                });
            }

            // Saves the result to cache and returns it.
            this._cachedProjects.Add(projectId, project);
            return project;
        }

        /// <inheritdoc />
        public async Task<bool> Update(string projectId, Models.Project.ProjectUpdate form)
        {
            // Current form data
            var data = await this.Get(projectId);

            // Merges in new changes
            data.Description = form.Description;
            data.Name = form.Name;
            data.Problems = new System.Collections.Generic.List<Models.General.ListItem>();

            // Does update
            return await this._serviceBase.DocumentUpdate(Project.PARTITION + ":" + projectId, data);
        }
    }
}