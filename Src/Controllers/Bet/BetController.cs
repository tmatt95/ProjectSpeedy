using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetController : ControllerBase
    {
        /**
        * Used to capture any errors this controller encounters.
        **/
        private readonly ILogger<BetController> _logger;

        public BetController(ILogger<BetController> logger)
        {
            _logger = logger;
        }

        /**
        * 
        **/
        [HttpGet("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public ActionResult Get(string projectId, string problemId, string betId)
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
        [HttpPut("/api/project/{projectId}/problem/{problemId}/bet")]
        public ActionResult Put(string projectId, string problemId, Bet.BetNew form)
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
        [HttpPost("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public ActionResult Post(string projectId, string problemId, string betId, Bet.BetUpdate form)
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
        [HttpDelete("/api/project/{projectId}/problem/{problemId}/bet/{betId}")]
        public ActionResult Delete(string projectId, string problemId, string betId)
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
