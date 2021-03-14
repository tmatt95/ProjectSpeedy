using System;
using System.Collections.Generic;

namespace ProjectSpeedy.BetFeedback
{
    /// <summary>
    /// A piece of feedback linked to a bet.
    /// </summary>
    public class BetFeedback
    {
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date the feedback was created (added).
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who added the feedback.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the list of bets linked to this feedback.
        /// </summary>
        public List<ProjectSpeedy.General.ListItem> LinkedBets { get; set; }
    }
}
