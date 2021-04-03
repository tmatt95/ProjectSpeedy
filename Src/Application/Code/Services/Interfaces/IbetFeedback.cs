namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet feedback related services.
    /// </summary>
    public interface IBetFeedback
    {
        /// <summary>
        /// Add a feedback to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="form">Form containing the new feedback</param>
        /// <returns>If the bet was added successfully.</returns>
        System.Threading.Tasks.Task<bool> CreateAsync(string projectId, string problemId, string betId, ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate form);

        /// <summary>
        /// Update a piece of feedback.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="form">Form containing updated feedback</param>
        /// <returns>If the update was a success.</returns>
        bool Update(string projectId, string problemId, string betId, ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate form);

        /// <summary>
        /// This action will delete the bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="feedbackId">Comment identifier</param>
        /// <returns>If the comment was deleted successfully.</returns>
        bool Delete(string projectId, string problemId, string betId, string feedbackId);
    }
}