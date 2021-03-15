namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All project related services.
    /// </summary>
    interface IProject
    {
        bool Create();

        bool update();

        bool Delete();
    }
}