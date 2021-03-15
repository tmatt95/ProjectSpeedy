namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All project related services.
    /// </summary>
    interface IProjects
    {
        /// <summary>
        /// Gets information on all projects in the application.
        /// </summary>
        /// <returns>Information on all projects in the application.</returns>
        ProjectSpeedy.Projects.ProjectsView Get();
    }
}