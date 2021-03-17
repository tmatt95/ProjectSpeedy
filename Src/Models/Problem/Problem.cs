using System;
using System.Collections.Generic;

namespace ProjectSpeedy.Models.Problem
{
    /// <summary>
    /// Used to display information on a problem / list of basic information on bets linked to the problem.
    /// </summary>
    public class Problem
    {
        /// <summary>
        /// Gets or sets the id of the project the problem is linked to.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name of the problem.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Gets or sets the description of the problem.
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Gets or sets the criteria that will determine when we have solved the problem.
        /// </summary>
        public string SuccessCriteria { get; set; } = "";

        /// <summary>
        /// Gets or sets a link to the bet the problem was created from.
        /// </summary>
        public string creeatedFromPreviousBet { get; set; }

        /// <summary>
        /// Gets or sets the date and time the problem was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets a list of bets linked to the problem.
        /// </summary>
        public List<General.ListItem> Bets { get; set; } = new List<General.ListItem>();
    }
}
