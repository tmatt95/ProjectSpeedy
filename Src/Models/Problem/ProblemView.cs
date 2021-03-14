using System;
using System.Collections.Generic;

namespace ProjectSpeedy.Problem
{
    /**
    * Used to display information on a problem / list of basic information on bets linked to the problem.
    **/
    public class ProblemView
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
        * Gets or sets the description of the bet.
        **/
        public string Description { get; set; }

        /**
        * Gets or sets the criteria that will determine when we have solved the problem.
        **/
        public string SuccessCriteria { get; set; }

        /**
        * Gets or sets a link to the bet the problem was created from.
        **/
        public string creeatedFromPreviousBet { get; set; }

        /**
        * Gets or sets the date and time the problem was created.
        **/
        public DateTime Created { get; set; }

        /**
        * Gets or sets a list of bets linked to the problem.
        **/
        public List<General.ListItem> Bets { get; set; }
    }
}
