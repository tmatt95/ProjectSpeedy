using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectSpeedy.Models.Bet
{
    /// <summary>
    /// All information on a bet.
    /// </summary>
    public class Bet
    {
        /// <summary>
        /// Gets or sets the unique identifier of the record.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the id of the project the problem is linked to.
        /// </summary>
        [Required]
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the id of the problem the bet is linked to.
        /// </summary>
        [Required]
        public string ProblemId { get; set; }

        /// <summary>
        /// Gets or sets the name of the bet.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date / time the bet was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the current status of the bet.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the total time for the bet.
        /// </summary>
        public int TimeTotal { get; set; }

        /// <summary>
        /// Gets or sets the current time allocated for a bet. 
        /// If not running this will be 0.
        /// </summary>
        public int TimeCurrent { get; set; }

        /// <summary>
        /// Gets or sets the list of comments linked to the bet.
        /// </summary>
        public List<BetComment.BetComment> Comments { get; set; } = new List<BetComment.BetComment>();

        /// <summary>
        /// Gets or sets the feedback linked to the bet.
        /// </summary>
        public List<BetFeedback.BetFeedback> Feedback { get; set; } = new List<BetFeedback.BetFeedback>();

        /// <summary>
        /// Gets or sets the outcomes linked to the bet.
        /// </summary>
        public List<BetOutcome.BetOutcome> Outcomes { get; set; } = new List<BetOutcome.BetOutcome>();

        /// <summary>
        /// Gets or sets the outcome of the bet.
        /// </summary>
        public string Outcome { get; set; }
    }
}
