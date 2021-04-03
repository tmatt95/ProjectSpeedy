using System;
using System.Linq;
using System.Net.Http;
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

        /// <summary>
        /// Contains services needed to interact with problems.
        /// </summary>
        private readonly ProjectSpeedy.Services.IProblem _problemServices;

        /// <summary>
        /// Contains services needed to interact with bet comments.
        /// </summary>
        private readonly ProjectSpeedy.Services.IBetComment _betCommentService;

        /// <summary>
        /// All bet comment related actions.
        /// </summary>
        /// <param name="logger">Used to log any errors which occur in this controller.</param>
        /// <param name="problemServices">Allows us to interact with problems.</param>
        /// <param name="betCommentService">Allows us to interact with bet comments.</param>
        public BetCommentController(ILogger<BetCommentController> logger, ProjectSpeedy.Services.IProblem problemServices, ProjectSpeedy.Services.IBetComment betCommentService)
        {
            this._logger = logger;
            this._problemServices = problemServices;
            this._betCommentService = betCommentService;
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
        public async System.Threading.Tasks.Task<ActionResult> PutAsync(string projectId, string problemId, string betId, Models.BetComment.BetCommentNewUpdate form)
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
                if (await this._betCommentService.CreateAsync(projectId, problemId, betId, form))
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
            throw new System.NotImplementedException();
        }
    }
}
