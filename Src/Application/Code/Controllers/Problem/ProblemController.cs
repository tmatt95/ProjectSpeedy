using System;
using System.Linq;
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

        /// <summary>
        /// All problem related actions.
        /// </summary>
        /// <param name="logger">Used to log any errors which occur in this controller.</param>
        /// <param name="problemServices">Allows us to interact with problems.</param>
        /// <param name="projectServices">Allows us to interact with projects.</param>
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
                if(problem.ProjectId != ProjectSpeedy.Services.Project.PREFIX + projectId){
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
                // Checks we have a valid request.
                if (form == null || !ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                // Tries to load the project to check that it exists
                var project = await this._projectService.Get(projectId);

                // Ensures we dont have a problem with the same name already
                if (project.Problems.Any(p => p.Name.Trim().ToLower() == form.Name.Trim().ToLower()))
                {
                    return BadRequest(new ProjectSpeedy.Models.General.BadRequest()
                    {
                        Message = "There is already a problem with the same name."
                    });
                }

                // Try and add the project.
                if (await this._problemServices.CreateAsync(projectId, form))
                {
                    return this.Accepted();
                }

                // If we get here something has gone wrong.
                return this.Problem();
            }
            catch (HttpRequestException e)
            {
                // Can we find the problem or project.
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }

                // There has been a problem loading or saving data
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
            catch (Exception e)
            {
                // There has been a system error.
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
        public async System.Threading.Tasks.Task<ActionResult> PostAsync(ProjectSpeedy.Models.Problem.ProblemUpdate form, string projectId, string problemId)
        {
            try
            {
                // Checks we have a valid request.
                if (form == null || !ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                // Tries to load the project to check that it exists
                var problem = await this._problemServices.GetAsync(projectId,problemId);

                // Ensures the project id is correct.
                if(problem.ProjectId != ProjectSpeedy.Services.Project.PREFIX + projectId){
                    return this.NotFound();
                }

                // Ensures we dont have a problem with the same name already
                var project = await this._projectService.Get(projectId);
                if (project.Problems.Any(p => p.Name.Trim().ToLower() == form.Name.Trim().ToLower() && p.Id != ProjectSpeedy.Services.Problem.PREFIX + problemId))
                {
                    return BadRequest(new ProjectSpeedy.Models.General.BadRequest()
                    {
                        Message = "There is already a problem with the same name."
                    });
                }

                // Tries to carry out the save.
                return this.Accepted(await this._problemServices.UpdateAsync(projectId, problemId, form));
            }
            catch (HttpRequestException e)
            {
                // Cant find the problem
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }

                // There has been a problem loading or saving data.
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
            catch (Exception e)
            {
                // There has been a system error.
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
            throw new System.NotImplementedException();
        }
    }
}
