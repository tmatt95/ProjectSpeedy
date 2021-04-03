using System;

namespace ProjectSpeedy.Models.BetComment
{
    /// <summary>
    /// Used when adding or updating a comment against a bet.
    /// </summary>
    public class BetCommentNewUpdate
    {
        /// <summary>
        /// Gets or sets the id of the project the problem is linked to.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the id of the problem the problem is linked to.
        /// </summary>
        public string ProblemId { get; set; }

        /// <summary>
        /// Gets or sets the id of the bet the problem is linked to.
        /// </summary>
        public string BetId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date the comment was created (added).
        /// </summary>
        public DateTime Created { get; set; }
    }
}
