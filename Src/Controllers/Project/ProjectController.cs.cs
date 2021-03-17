using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    /// <summary>
    /// All actions linked to an individual project (there is also projects for actions which affect more 
    /// than one project).
    /// </summary>
    [ApiController]
    public class ProjectController : ControllerBase
    {
        /// <summary>
        /// Used to capture any errors this controller encounters.
        /// </summary>
        private readonly ILogger<ProjectsController> _logger;

        /// <summary>
        /// Contains services needed to interact with projects.
        /// </summary>
        private readonly ProjectSpeedy.Services.IProject _projectServices;

        public ProjectController(ILogger<ProjectsController> logger, ProjectSpeedy.Services.IProject projectServices)
        {
            this._logger = logger;
            this._projectServices = projectServices;
        }

        /// <summary>
        /// Projects allows the application to group problems and bets together in one place. This will 
        /// return all of the projects in the applcation.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <returns>Information on the project.</returns>
        [HttpGet("/api/project/{projectId}")]
        public async System.Threading.Tasks.Task<ActionResult> GetAsync(string projectId)
        {
            try
            {
                return this.Ok(await this._projectServices.Get(projectId));
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// Every bet needs to be linked to a problem and a problem needs to be linked to a project. 
        /// This lets the user create a new project. Very little information is required when creating 
        /// a project. This can be filled in at a later date (as it might not be known at the time).
        /// </summary>
        /// <param name="form">Form containing information on the new project.</param>
        /// <returns>If the new project was added successfully.</returns>
        [HttpPut("/api/project")]
        public async System.Threading.Tasks.Task<ActionResult> PutAsync(Models.Project.ProjectNew form)
        {
            try
            {
                // Checks we have a valid request.
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                // Try and add the project.
                if (await this._projectServices.CreateAsync(form))
                {
                    return this.Accepted();
                }

                return this.Problem();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// The create project action has been designed to be simple and contain the minimum number of fields. 
        /// The user will need to have the ability to update the project to add missing information.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="form">Form containing information on the new project.</param>
        /// <returns>If the project has been updated successfully.</returns>
        [HttpPost("/api/project/{projectId}")]
        public ActionResult Post(string projectId, ProjectSpeedy.Models.Project.ProjectUpdate form)
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
        /// This action will delete the project and linked problems / bets.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <returns>If the project has been deleted successfully.</returns>
        [HttpDelete("/api/project/{projectId}")]
        public ActionResult Delete(string projectId)
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
