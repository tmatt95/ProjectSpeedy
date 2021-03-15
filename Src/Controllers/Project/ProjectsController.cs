using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// Actions which relate to more than one project.
    /// TODO Add authentication.
    /// </summary>
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<ProjectsController> _logger;

        /// <summary>
        /// Used to access project services.
        /// </summary>
        private readonly Services.IProject _iProject;

        public ProjectsController(ILogger<ProjectsController> logger, Services.IProject iProject)
        {
            _logger = logger;
            _iProject = iProject;
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
                return this.Ok(this._iProject.GetAll());
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }
    }
}
