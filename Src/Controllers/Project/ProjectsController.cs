using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ILogger<ProjectsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Projects allows the application to group problems and bets together in one place. 
        /// This will return all of the projects in the applcation.
        /// </summary>
        /// <returns>List of projects in the application.</returns>
        [HttpGet("/api/projects")]
        public ActionResult Get()
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
    }
}
