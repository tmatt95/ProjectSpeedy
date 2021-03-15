using System;
using System.Collections.Generic;

namespace ProjectSpeedy.Models.BetOutcome
{
    /// <summary>
    /// An outcome linked to a bet.
    /// </summary>
    public class BetOutcome
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
            /// Gets or sets the date the outcome was created (added).
            /// </summary>
            public DateTime Created { get; set; }

            /// <summary>
            /// Gets or sets the name of the person who added the outcome.
            /// </summary>
            public string CreatedBy { get; set; }

            /// <summary>
            /// Gets or sets the list of bets linked to this outcome.
            /// </summary>
            public List<ProjectSpeedy.Models.General.ListItem> LinkedBets { get; set; }
        }
    }
}
