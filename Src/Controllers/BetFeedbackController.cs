using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [HttpGet]
        public ActionResult Get()
        {
            return this.Ok();
        }

        /**
        * 
        **/
        [HttpPut]
        public ActionResult Put()
        {
            return this.Accepted();
        }

        /**
        * 
        **/
        [HttpPost]
        public ActionResult Post()
        {
            return this.Accepted();
        }

        /**
        * 
        **/
        [HttpDelete]
        public ActionResult Delete()
        {
            return this.Accepted();
        }
    }
}
