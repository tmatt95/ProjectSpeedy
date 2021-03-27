using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectSpeedy.Models.Project
{
    /// <summary>
    /// Add a new project.
    /// </summary>
    public class ProjectNew
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the project. 
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Gets or sets the date the comment was created (added).
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the list of problems linked to the project.
        /// </summary>
        public List<General.ListItem> Problems { get; set; } = new List<General.ListItem>();
    }
}
