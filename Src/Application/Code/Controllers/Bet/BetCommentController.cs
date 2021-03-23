using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// Actions which relate to adding or updating a comment against a bet.
    /// </summary>
    [ApiController]
    public class BetCommentController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<BetCommentController> _logger;

        public BetCommentController(ILogger<BetCommentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Add a comment to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="form">Form containing the comment.</param>
        /// <returns>If the comment was added successfully.</returns>
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet/{betId}/comment")]
        public ActionResult Put(string projectId, string problemId, string betId, Models.BetComment.BetCommentNewUpdate form)
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
        /// Update a comment.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="commentId">Comment identifier</param>
        /// <param name="form">Form containing the comment.</param>
        /// <returns>If the comment was updated successfully.</returns>
        [HttpPost("/api/project/{projectId}/problem/{problemId}/bet/{betId}/comment/{commentId}")]
        public ActionResult Post(string projectId, string problemId, string betId, string commentId, Models.BetComment.BetCommentNewUpdate form)
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
        /// Delete a comment.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="commentId">Comment identifier</param>
        /// <returns>If the comment was deleted successfully.</returns>
        [HttpDelete("/api/project/{projectId}/problem/{problemId}/bet/{betId}/comment/{commentId}")]
        public ActionResult Delete(string projectId, string problemId, string betId, string commentId)
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
