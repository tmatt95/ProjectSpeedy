using System;
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

        public BetFeedbackController(ILogger<BetFeedbackController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Add feedback to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <returns>If the feedback was added successfully.</returns>
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback")]
        public ActionResult Put(string projectId, string problemId, string betId)
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
