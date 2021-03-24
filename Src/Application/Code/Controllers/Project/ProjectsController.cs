using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// Actions which relate to more than one project.
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
        private readonly ProjectSpeedy.Services.IProject _projectServices;

        public ProjectsController(ILogger<ProjectsController> logger, Services.IProject projectServices)
        {
            _logger = logger;
            _projectServices = projectServices;
        }

        /// <summary>
        /// Projects allows the application to group problems and bets together in one place. 
        /// This will return all of the projects in the applcation.
        /// </summary>
        /// <returns>List of projects in the application.</returns>
        [HttpGet("/api/projects")]
        public async System.Threading.Tasks.Task<ActionResult<ProjectSpeedy.Models.Projects.ProjectsView>> GetAsync()
        {
            try
            {
                return this.Ok(await this._projectServices.GetAll());
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }
    }
}
