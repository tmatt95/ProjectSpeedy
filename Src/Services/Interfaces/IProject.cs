using System.Threading.Tasks;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All project related services.
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// Gets information on all projects in the application.
        /// </summary>
        /// <returns>Information on all projects in the application.</returns>
        Task<ProjectSpeedy.Models.Projects.ProjectsView> GetAll();

        /// <summary>
        /// Every bet needs to be linked to a problem and a problem needs to be linked to a project. 
        /// This lets the user create a new project. Very little information is required when creating 
        /// a project. This can be filled in at a later date (as it might not be known at the time).
        /// </summary>
        /// <param name="form">Form containing information on the new project.</param>
        /// <returns>If the new project was added successfully.</returns>
        Task<bool> CreateAsync(ProjectSpeedy.Models.Project.ProjectNew form);

        /// <summary>
        /// The create project action has been designed to be simple and contain the minimum number of fields. 
        /// The user will need to have the ability to update the project to add missing information.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="form">Form containing information on the new project.</param>
        /// <returns>If the project has been updated successfully.</returns>
        bool Update(string projectId, ProjectSpeedy.Models.Project.ProjectUpdate form);

        /// <summary>
        /// This action will delete the project and linked problems / bets.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <returns>If the project has been deleted successfully.</returns>
        bool Delete(string projectId);
    }
}