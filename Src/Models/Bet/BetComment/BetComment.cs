using System;
using System.Collections.Generic;

namespace ProjectSpeedy.BetComment
{
    /// <summary>
    /// A comment linked to a bet.
    /// </summary>
    public class BetComment
    {
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date the comment was created (added).
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who added the comment.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the list of bets linked to this comment.
        /// </summary>
        public List<ProjectSpeedy.General.ListItem> LinkedBets { get; set; }
    }
}
