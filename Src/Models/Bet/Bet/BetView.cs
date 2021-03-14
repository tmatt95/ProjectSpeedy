using System;
using System.Collections.Generic;

namespace ProjectSpeedy.Bet
{
    /**
    * All information on a bet.
    **/
    public class BetView
    {
        /**
        * Gets or sets the unique identifier of the record.
        **/
        public string Id { get; set; }

        /**
        * Gets or sets the name of the bet.
        **/
        public string Name { get; set; }

        /**
        * Gets or sets the date / time the bet was created.
        **/
        public DateTime Created { get; set; }

        /**
        * Gets or sets the current status of the bet.
        **/
        public string Status { get; set; }

        /**
        * Gets or sets the total time for the bet.
        **/
        public int TimeTotal { get; set; }

        /**
        * Gets or sets the current time allocated for a bet.
        * If not running this will be 0.
        **/
        public int TimeCurrent { get; set; }

        /**
        * Gets or sets the list of comments linked to the bet.
        **/
        public List<BetComment.BetComment> Comments { get; set; }

        /**
        * Gets or sets the feedback linked to the bet.
        **/
        public List<BetFeedback.BetFeedback> Feedback { get; set; }

        /**
        * Gets or sets the outcomes linked to the bet.
        **/
        public List<BetOutcome.BetOutcome> Outcomes {get; set;}

        /**
        * Gets or sets the outcome of the bet.
        **/
        public string Outcome {get; set;}
    }
}
