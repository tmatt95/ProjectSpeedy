namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public interface IBet
    {
        /// <summary>
        /// Gets all the information related to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <returns>Information on the bet.</returns>
        ProjectSpeedy.Models.Bet.Bet Get(string projectId, string problemId, string betId);

        /// <summary>
        /// Add a new bet to a problem. As with the problem and project this form only asks for the minimum
        /// amount of information. Extra information can be added before the bet is started.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="form">Form containing the new bet.</param>
        /// <returns>If the bet was added successfully.</returns>
        bool Create(string projectId, string problemId, ProjectSpeedy.Models.Bet.BetNew form);

        /// <summary>
        /// Update a new bet (once a bet has been started it cannot be updated).
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="form">Form containing updated bet information.</param>
        /// <returns>If the update was a success.</returns>
        bool Update(string projectId, string problemId, string betId, ProjectSpeedy.Models.Bet.BetNew form);

        /// <summary>
        /// This action will delete the bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <returns>If the bet was deleted successfully.</returns>
        bool Delete(string projectId, string betId);
    }
}