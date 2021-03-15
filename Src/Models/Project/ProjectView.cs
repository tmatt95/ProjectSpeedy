using System;
using System.Collections.Generic;

namespace ProjectSpeedy.Models.Project
{
    /// <summary>
    /// Information on a single project.
    /// </summary>
    public class ProjectView
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date the comment was created (added).
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the list of problems linked to the project.
        /// </summary>
        public List<General.ListItem> Problems { get; set; }
    }
}
