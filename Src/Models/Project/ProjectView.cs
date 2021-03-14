using System;
using System.Collections.Generic;

namespace ProjectSpeedy.Project
{
    /**
    * Information on a single project.
    **/
    public class ProjectView
    {
        /**
        * Gets or sets the name of the project.
        **/
        public string Name { get; set; }

        /**
        * Gets or sets the description of the project.
        **/
        public string Description { get; set; }

        /**
        * Gets or sets the date the comment was created (added).
        **/
        public DateTime Created { get; set; }

        /**
        * Gets or sets the list of problems linked to the project.
        **/
        public List<General.ListItem> Problems { get; set; }
    }
}
