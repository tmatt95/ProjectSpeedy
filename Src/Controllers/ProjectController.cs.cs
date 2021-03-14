﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectSpeedy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        /**
        * Used to capture any errors this controller encounters.
        **/
        private readonly ILogger<ProjectsController> _logger;

        public ProjectController(ILogger<ProjectsController> logger)
        {
            _logger = logger;
        }

        /**
        * Projects allows the application to group problems and bets together in one place. This will 
        * return all of the projects in the applcation.
        **/
        [HttpGet]
        public ActionResult Get()
        {
            return this.Ok();
        }

        /**
        * Every bet needs to be linked to a problem and a problem needs to be linked to a project. 
        * This lets the user create a new project. Very little information is required when creating 
        * a project. This can be filled in at a later date (as it might not be known at the time).
        **/
        [HttpPut]
        public ActionResult Put()
        {
            return this.Accepted();
        }

        /**
        * The create project action has been designed to be simple and contain the minimum number of fields.
        * The user will need to have the ability to update the project to add missing information.
        **/
        [HttpPost]
        public ActionResult Post()
        {
            return this.Accepted();
        }

        /**
        * This action will delete the project and linked problems / bets.
        **/
        [HttpDelete]
        public ActionResult Delete()
        {
            return this.Accepted();
        }
    }
}
