using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// Actions which relate to a bet.
    /// </summary>
    [ApiController]
    public class BetController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<BetController> _logger;

        /// <summary>
        /// Used to interact with problem data.
        /// </summary>
        private readonly ProjectSpeedy.Services.IProblem _problemService;

        /// <summary>
        /// Used to interact with bet data.
        /// </summary>
        private readonly ProjectSpeedy.Services.IBet _betService;

        public BetController(ILogger<BetController> logger, ProjectSpeedy.Services.IBet betService, ProjectSpeedy.Services.IProblem problemService)
        {
            this._logger = logger;
            this._betService = betService;
            this._problemService = problemService;
        }

        /// <summary>
        /// Gets all the information related to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <returns>Information on the bet.</returns>
        [HttpGet("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public async System.Threading.Tasks.Task<ActionResult<Models.Bet.Bet>> GetAsync(string projectId, string problemId, string betId)
        {
            try
            {
                // Gets the bet and checks the project and problem ids are valid for it.
                // If the bet cannot be found it will throw a 404 exception.
                var bet = await this._betService.GetAsync(projectId, problemId, betId);
                if(bet.ProjectId != ProjectSpeedy.Services.Project.PREFIX + projectId || bet.ProblemId != ProjectSpeedy.Services.Problem.PREFIX + problemId){
                    return this.NotFound();
                }

                // If the id's are valid we can return a result.
                return this.Ok(bet);
            }
            catch (HttpRequestException e)
            {
                // Will be triggered if the bet cannot be found (wrong id).
                if(e.StatusCode == System.Net.HttpStatusCode.NotFound){
                    return NotFound();
                }

                // Any other exception code is a problem.
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
            catch (Exception e)
            {
                // Unhandled exception.
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// Add a new bet to a problem. As with the problem and project this form only asks for the minimum
        /// amount of information. Extra information can be added before the bet is started.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="form">Form containing the new bet.</param>
        /// <returns>If the bet was added successfully.</returns>
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet")]
        public async System.Threading.Tasks.Task<ActionResult> PutAsync(string projectId, string problemId, Models.Bet.BetNew form)
        {
            try
            {
                // Checks we have a valid request.
                if (form == null || !ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                // Gets the problem and checks the project id is valid for it.
                // If the problem cannot be found it will throw a 404 exception.
                var problem = await this._problemService.GetAsync(projectId, problemId);
                if(problem.ProjectId != ProjectSpeedy.Services.Project.PREFIX + projectId){
                    return this.NotFound();
                }

                // Ensures we dont have a bets with the same name already
                if (problem.Bets.Any(p => p.Name.Trim().ToLower() == form.Name.Trim().ToLower()))
                {
                    return BadRequest(new ProjectSpeedy.Models.General.BadRequest()
                    {
                        Message = "There is already a bet with the same name."
                    });
                }

                // Try and add the bet.
                if (await this._betService.CreateAsync(projectId, problemId, form))
                {
                    return this.Accepted();
                }

                // If we get here something has gone wrong.
                return this.Problem();
            }
            catch (HttpRequestException e)
            {
                // Can we find the problem.
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
                
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// Update a new bet (once a bet has been started it cannot be updated).
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="form">Form containing updated bet information.</param>
        /// <returns>If the update was a success.</returns>
        [HttpPost("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public async System.Threading.Tasks.Task<ActionResult> PostAsync(string projectId, string problemId, string betId, Models.Bet.BetUpdate form)
        {
            try
            {
                // Gets the bet and checks the project and problem ids are valid for it.
                // If the bet cannot be found it will throw a 404 exception.
                var bet = await this._betService.GetAsync(projectId, problemId, betId);
                if(bet.ProjectId != projectId || bet.ProblemId != problemId){
                    return this.NotFound();
                }

                return this.Accepted();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// This action will delete the bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <returns>If the bet was deleted successfully.</returns>
        [HttpDelete("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public ActionResult Delete(string projectId, string problemId, string betId)
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
