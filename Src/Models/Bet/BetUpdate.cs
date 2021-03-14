using System.Collections.Generic;

namespace ProjectSpeedy.Bet
{
    /**
    * Used to update a bet before it has started.
    **/
    public class BetUpdate
    {
        /**
        * Gets or sets the name of the bet.
        **/
        public string Name { get; set; }

        /**
        * Gets or sets the description of the bet.
        **/
        public string Description { get; set; }

        /**
        * Gets or sets the measures of success the bet will be judged against.
        **/
        public string MeasuresOfSuccess { get; set; }

        /**
        * Gets or sets the time in days that will be allocated for the bet.
        **/
        public int TimeTotal { get; set; }
    }
}
