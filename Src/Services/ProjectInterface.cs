namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All project related services.
    /// </summary>
    interface ProjectInterface
    {
        bool Create();

        bool update();

        bool Delete();
    }
}