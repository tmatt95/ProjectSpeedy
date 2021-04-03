using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// Actions which related to adding or updating feedback on a bet.
    /// </summary>
    [ApiController]
    public class BetFeedbackController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<BetFeedbackController> _logger;

        /// <summary>
        /// Contains services needed to interact with problems.
        /// </summary>
        private readonly ProjectSpeedy.Services.IProblem _problemServices;
        
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ProjectSpeedy.Services.IBetFeedback _betFeedback;

        public BetFeedbackController(ILogger<BetFeedbackController> logger, ProjectSpeedy.Services.IBetFeedback iBetFeedback, ProjectSpeedy.Services.IProblem problemServices)
        {
            this._logger = logger;
            this._betFeedback = iBetFeedback;
            this._problemServices = problemServices;
        }

        /// <summary>
        /// Add feedback to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <returns>If the feedback was added successfully.</returns>
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback")]
        public async System.Threading.Tasks.Task<ActionResult> PutAsync(string projectId, string problemId, string betId, ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate form)
        {
            try
            {
                // Checks we have a valid request.
                if (form == null || !ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                // Gets the problem and checks the project id / bet id is valid against it.
                var problem = await this._problemServices.GetAsync(projectId, problemId);
                if(problem.ProjectId != ProjectSpeedy.Services.Project.PREFIX + projectId ||
                !problem.Bets.Any( b => b.Id == ProjectSpeedy.Services.Bet.PREFIX + betId)){
                    return this.NotFound();
                }

                // Tries to add the comment
                if (await this._betFeedback.CreateAsync(projectId, problemId, betId, form))
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
        /// Updates a piece of feedback against a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="feedbackId">Feedback identifier</param>
        /// <returns>If the feedback was updated successfully.</returns>
        [HttpPost("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback/{feedbackId}")]
        public ActionResult Post(string projectId, string problemId, string betId, string feedbackId)
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
        /// Deletes an outcome (linked to a bet).
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="feedbackId">Feedback identifier</param>
        /// <returns>If the feedback was deleted successfully.</returns>
        [HttpDelete("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback/{feedbackId}")]
        public ActionResult Delete(string projectId, string problemId, string betId, string feedbackId)
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
