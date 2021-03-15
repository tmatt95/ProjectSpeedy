using System;
using System.Collections.Generic;

namespace ProjectSpeedy.Projects
{
    /// <summary>
    /// Information on all projects in the application.
    /// </summary>
    public class ProjectsView
    {
        /// <summary>
        /// Gets or sets the list of projects.
        /// </summary>
        public List<General.ListItem> Projects { get; set; }
    }
}