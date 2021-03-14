using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// Actions which relate to a bet.
    /// TODO Add authentication.
    /// </summary>
    [ApiController]
    public class BetController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<BetController> _logger;

        public BetController(ILogger<BetController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all the information related to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <returns>Information on the bet.</returns>
        [HttpGet("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public ActionResult Get(string projectId, string problemId, string betId)
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
        /// Add a new bet to a problem. As with the problem and project this form only asks for the minimum
        /// amount of information. Extra information can be added before the bet is started.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="form">Form containing the new bet.</param>
        /// <returns>If the bet was added successfully.</returns>
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet")]
        public ActionResult Put(string projectId, string problemId, Bet.BetNew form)
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
        /// Update a new bet (once a bet has been started it cannot be updated).
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="form">Form containing updated bet information.</param>
        /// <returns>If the update was a success.</returns>
        [HttpPost("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public ActionResult Post(string projectId, string problemId, string betId, Bet.BetUpdate form)
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
