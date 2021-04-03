namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet comment related services.
    /// </summary>
    public interface IBetComment
    {
        /// <summary>
        /// Add a new comment to a bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="form">Form containing the new comment</param>
        /// <returns>If the bet was added successfully.</returns>
        System.Threading.Tasks.Task<bool> CreateAsync(string projectId, string problemId, string betId, ProjectSpeedy.Models.BetComment.BetCommentNewUpdate form);

        /// <summary>
        /// Update a comment.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="form">Form containing updated comment information</param>
        /// <returns>If the update was a success.</returns>
        bool Update(string projectId, string problemId, string betId, ProjectSpeedy.Models.BetComment.BetCommentNewUpdate form);

        /// <summary>
        /// This action will delete the bet.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="betId">Bet identifier</param>
        /// <param name="commentId">Comment identifier</param>
        /// <returns>If the comment was deleted successfully.</returns>
        bool Delete(string projectId, string problemId, string betId, string commentId);
    }
}