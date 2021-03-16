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
        [Required]
        public string Name { get; set; }
    }
}
