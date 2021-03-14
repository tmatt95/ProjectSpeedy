using System;
using System.Collections.Generic;

namespace ProjectSpeedy.BetFeedback
{
    /**
    * A piece of feedback linked to a bet.
    **/
    public class BetFeedback
    {
        /**
         * Gets or sets the comment.
         **/
        public string Comment { get; set; }

        /**
        * Gets or sets the date the feedback was created (added).
        **/
        public DateTime Created { get; set; }

        /**
        * Gets or sets the name of the person who added the feedback.
        **/
        public string CreatedBy { get; set; }

        /**
        * Gets or sets the list of bets linked to this feedback.
        **/
        public List<ProjectSpeedy.General.ListItem> LinkedBets { get; set; }
    }
}
