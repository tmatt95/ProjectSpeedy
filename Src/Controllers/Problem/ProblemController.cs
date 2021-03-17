using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// All problem related actions.
    /// TODO Add authentication.
    /// </summary>
    [ApiController]
    public class ProblemController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<ProblemController> _logger;

        private ProjectSpeedy.Services.IProblem _iproblem;

        public ProblemController(ILogger<ProblemController> logger, ProjectSpeedy.Services.IProblem iProblem)
        {
            this._logger = logger;
            this._iproblem = iProblem;
        }

        /// <summary>
        /// This will get all the information on the problem (including a list of linked bets).
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">problem identifier</param>
        /// <returns>Information on a problem.</returns>
        [HttpGet("/api/project/{projectId}/problem/{problemId}")]
        public ActionResult Get(string projectId, string problemId)
        {
            try
            {
                return this.Ok();
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
        /// <param name="projectId">Project identifier</param>
        /// <returns>If the problem was added successfully.</returns>
        [HttpPut("/api/project/{projectId}/problem")]
        public async System.Threading.Tasks.Task<ActionResult> PutAsync(string projectId, ProjectSpeedy.Models.Problem.ProblemNew form)
        {
            try
            {
                // Checks we have a valid request.
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                // Try and add the project.
                if (await this._iproblem.CreateAsync(projectId, form))
                {
                    return this.Accepted();
                }

                // We should not get here unless something has gone wrong.
                throw new Exception("Problem adding problem");

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
