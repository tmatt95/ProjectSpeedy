using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    [ApiController]
    public class BetFeedbackController : ControllerBase
    {
        /**
        * Used to capture any errors this controller encounters.
        **/
        private readonly ILogger<BetFeedbackController> _logger;

        public BetFeedbackController(ILogger<BetFeedbackController> logger)
        {
            _logger = logger;
        }

        /**
        * 
        **/
        [HttpGet("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback/{feedbackId}")]
        public ActionResult Get(string projectId, string problemId, string betId, string feedbackId)
        {
            try{
                return this.Ok();
            }
            catch(Exception e){
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /**
        * 
        **/
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback")]
        public ActionResult Put(string projectId, string problemId, string betId)
        {
            try{
                return this.Accepted();
            }
            catch(Exception e){
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /**
        * 
        **/
        [HttpPost("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback/{feedbackId}")]
        public ActionResult Post(string projectId, string problemId, string betId, string feedbackId)
        {
            try{
                return this.Accepted();
            }
            catch(Exception e){
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /**
        * 
        **/
        [HttpDelete("/api/project/{projectId}/problem/{problemId}/bet/{betId}/feedback/{feedbackId}")]
        public ActionResult Delete(string projectId, string problemId, string betId, string feedbackId)
        {
            try{
                return this.Accepted();
            }
            catch(Exception e){
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }
    }
}
