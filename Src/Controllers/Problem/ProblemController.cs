using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProblemController : ControllerBase
    {
        /**
        * Used to capture any errors this controller encounters.
        **/
        private readonly ILogger<ProblemController> _logger;

        public ProblemController(ILogger<ProblemController> logger)
        {
            _logger = logger;
        }

        /**
        * 
        **/
        [HttpGet]
        public ActionResult Get()
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
        [HttpPut]
        public ActionResult Put()
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
        [HttpPost]
        public ActionResult Post()
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
        [HttpDelete]
        public ActionResult Delete()
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
