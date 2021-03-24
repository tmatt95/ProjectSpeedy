using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// All problem related actions.
    /// </summary>
    [ApiController]
    public class ProblemController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<ProblemController> _logger;

        /// <summary>
        /// Contains services needed to interact with problems.
        /// </summary>
        private readonly ProjectSpeedy.Services.IProblem _problemServices;

        /// <summary>
        /// Contains services needed to interact with projects.
        /// </summary>
        private readonly ProjectSpeedy.Services.IProject _projectService;

        public ProblemController(ILogger<ProblemController> logger, ProjectSpeedy.Services.IProblem problemServices, ProjectSpeedy.Services.IProject projectServices)
        {
            this._logger = logger;
            this._problemServices = problemServices;
            this._projectService = projectServices;
        }

        /// <summary>
        /// This will get all the information on the problem (including a list of linked bets).
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">problem identifier</param>
        /// <returns>Information on a problem.</returns>
        [HttpGet("/api/project/{projectId}/problem/{problemId}")]
        public async System.Threading.Tasks.Task<ActionResult<Models.Problem.Problem>> GetAsync(string projectId, string problemId)
        {
            try
            {
                // Gets the problem and checks the project ids are valid for it.
                // If the problem cannot be found it will throw a 404 exception.
                var problem = await this._problemServices.GetAsync(projectId, problemId);
                if(problem.ProjectId != projectId){
                    return this.NotFound();
                }

                // We have a valid problem.
                return this.Ok(problem);
            }
            catch (HttpRequestException e)
            {
                // Can we find the problem
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                return this.Problem();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// This will let you add a problem to a project. You will then be able to place bets against
        /// the problem and try and solve it.
        /// </summary>
        /// <param name="form">Contains information on the new problem</param>
        /// <param name="projectId">Project identifier</param>
        /// <returns>If the problem was added successfully.</returns>
        [HttpPut("/api/project/{projectId}/problem")]
        public async System.Threading.Tasks.Task<ActionResult> PutAsync(ProjectSpeedy.Models.Problem.ProblemNew form, string projectId)
        {
            try
            {
                // Tries to load the project to check that it exists
                await this._projectService.Get(projectId);

                // Checks we have a valid request.
                if (form == null || !ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                // Try and add the project.
                if (await this._problemServices.CreateAsync(projectId, form))
                {
                    return this.Accepted();
                }

                return this.Problem();
            }
            catch (HttpRequestException e)
            {
                // Can we find the problem
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                return this.Problem();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// Ideally before adding bets, this API will be used to add as much information as possible to the 
        /// problem. All the bets made will try and solve this. Try and make the desciption as clear as possible 
        /// and the success criteria unambigous so it is clear when the project has been solved.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <returns>If the problem was updated successfully.</returns>
        [HttpPost("/api/project/{projectId}/problem/{problemId}")]
        public ActionResult Post(string projectId, string problemId)
        {
            try
            {
                return this.Accepted();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// This action will delete the problem and all linked bets.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <returns>If the problem was deleted successfully.</returns>
        [HttpDelete("/api/project/{projectId}/problem/{problemId}")]
        public ActionResult Delete(string projectId, string problemId)
        {
            try
            {
                return this.Accepted();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }
    }
}
