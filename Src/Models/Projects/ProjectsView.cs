using System.Collections.Generic;

namespace ProjectSpeedy.Models.Projects
{
    /// <summary>
    /// Information on all projects in the application.
    /// </summary>
    public class ProjectsView
    {
        /// <summary>
        /// Gets or sets the rows containing the projects that are in the project.
        /// </summary>
        public List<General.ViewResult> rows { get; set; }
    }
}