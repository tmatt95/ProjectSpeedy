using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Prefix of id's in couchdb.
        /// </summary>
        public const string PREFIX = "problem:";

        /// <summary>
        /// Partitions allow CouchDB to scale better.
        /// </summary>
        public const string PARTITION = "problem";

        /// <summary>
        /// Contains a cache of problems so we dont have to do the requests multiple times for them.
        /// </summary>
        private readonly Dictionary<string, Models.Problem.Problem> _cachedProblems = new Dictionary<string, Models.Problem.Problem>();

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
            // The new project object
            var newProblem = new ProjectSpeedy.Models.Problem.ProblemNew()
            {
                Name = form.Name,
                Created = DateTime.UtcNow,
                ProjectId = "project:" + projectId,
                Description = form.Description,
                SuccessCriteria = form.SuccessCriteria,
            };

            // Creates the project and checks if the id is returned.
            var newId = await this._serviceBase.DocumentCreate(newProblem, Problem.PARTITION);
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
            // Checks cache to see if problem is in it.
            if(this._cachedProblems.ContainsKey(problemId)){
                return this._cachedProblems[problemId];
            }

            // Id of the problem in couchDb.
            var couchProblemId = "problem:" + problemId;

            // Gets the base problem.
            var viewData = await this._serviceBase.DocumentGet(couchProblemId);
            using var responseStream = await viewData.ReadAsStreamAsync();
            var problem = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.Problem.Problem>(responseStream);

            var problemData = await this._serviceBase.ViewGet("bet", "bets", "bets", couchProblemId, couchProblemId);
            using var responseStreamProblems = await problemData.ReadAsStreamAsync();

            var rawBets = await JsonSerializer.DeserializeAsync<ProjectSpeedy.Models.CouchDb.View.ViewResult>(responseStreamProblems);
            foreach (var record in rawBets.rows)
            {
                problem.Bets.Add(new Models.General.ListItem()
                {
                    Id = record.value.id,
                    Name = record.value.name,
                    Status = record.value.status,
                    Address = "/project/" + projectId + "/problem/" + problemId + "/bet/" + record.value.id.Replace(Bet.PREFIX, ""),
                    IconClasses = "bi bi-file-text grid-card-icon"
                });
            }

            // Orders the bets by name asc
            problem.Bets = problem.Bets.OrderBy(r => r.Name).ToList();

            // Adds problem to cache and returns it
            this._cachedProblems.Add(problemId, problem);
            return problem;
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task<bool> UpdateAsync(string projectId, string problemId, Models.Problem.ProblemUpdate form)
        {
            // Current form data
            var data = await this.GetAsync(projectId, problemId);

            // Merges in new changes
            data.Description = form.Description;
            data.Name = form.Name;
            data.SuccessCriteria = form.SuccessCriteria;
            data.Bets = new System.Collections.Generic.List<Models.General.ListItem>();

            // Does update
            return await this._serviceBase.DocumentUpdate(Problem.PARTITION + ":" + problemId, data);
        }
    }
}