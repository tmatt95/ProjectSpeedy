using System;
using System.Collections.Generic;

namespace ProjectSpeedy.BetOutcome
{
    /**
    * An outcome linked to a bet.
    **/
    public class BetOutcome
    {
        /**
        * A comment linked to a bet.
        **/
        public class BetComment
        {
            /**
            * Gets or sets the comment.
            **/
            public string Comment { get; set; }

            /**
            * Gets or sets the date the outcome was created (added).
            **/
            public DateTime Created { get; set; }

            /**
            * Gets or sets the name of the person who added the outcome.
            **/
            public string CreatedBy { get; set; }

            /**
            * Gets or sets the list of bets linked to this outcome.
            **/
            public List<ProjectSpeedy.General.ListItem> LinkedBets { get; set; }
        }
    }
}
