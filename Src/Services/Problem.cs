using System;
using System.Text.Json;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All problem related services.
    /// </summary>
    public class Problem : IProblem
    {
        /// <summary>
        /// Contains helper functions needed for all services to work
        /// </summary>
        private readonly IServiceBase _serviceBase;

        /// <summary>
        /// All problem related services.
        /// </summary>
        /// <param name="serviceBase">Contains helper functions needed for all services to work.</param>
        public Problem(IServiceBase serviceBase)
        {
            this._serviceBase = serviceBase;
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task<bool> CreateAsync(string projectId, Models.Problem.ProblemNew form)
        {
            // TODO We need to ensure that the project exists with the supplied Id.

            // TODO We need to ensure that there is not another problem with the same name.

            // The new project object
            var newProblem = new ProjectSpeedy.Models.Problem.Problem()
            {
                Name = form.Name,
                Created = DateTime.UtcNow,
                ProjectId = "project:" + projectId
            };

            // Creates the project and checks if the id is returned.
            var newId = await this._serviceBase.DocumetCreate(newProblem, "problem");
            return !string.IsNullOrWhiteSpace(newId);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task<Models.Problem.Problem> GetAsync(string projectId, string problemId)
        {
            // Id of the project in couchDb.
            var couchProjectId = "problem:" + problemId;

            // Gets the base project.
            var viewData = await this._serviceBase.GetDocument(couchProjectId);
            using var responseStream = await viewData.ReadAsStreamAsync();
            var problem = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.Problem.Problem>(responseStream);
            return problem;
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Problem.ProblemUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}