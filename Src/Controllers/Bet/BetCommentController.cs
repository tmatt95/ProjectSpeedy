using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetCommentController : ControllerBase
    {
        /**
        * Used to capture any errors this controller encounters.
        **/
        private readonly ILogger<BetCommentController> _logger;

        public BetCommentController(ILogger<BetCommentController> logger)
        {
            _logger = logger;
        }

        /**
        * 
        **/
        [HttpGet("/api/project/{projectId}/problem/{problemId}/bet/{betId}/comment/{commentId}")]
        public ActionResult Get(string projectId, string problemId, string betId, string commentId)
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
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet/{betId}/comment")]
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
        [HttpPost("/api/project/{projectId}/problem/{problemId}/bet/{betId}/comment/{commentId}")]
        public ActionResult Post(string projectId, string problemId, string betId, string commentId)
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
        [HttpDelete("/api/project/{projectId}/problem/{problemId}/bet/{betId}/comment/{commentId}")]
        public ActionResult Delete(string projectId, string problemId, string betId, string commentId)
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
