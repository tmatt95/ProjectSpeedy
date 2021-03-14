namespace ProjectSpeedy.Bet
{
    /// <summary>
    /// Used to update a bet before it has started.
    /// </summary>
    public class BetUpdate
    {
        /// <summary>
        /// Gets or sets the name of the bet.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the bet.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the measures of success the bet will be judged against.
        /// </summary>
        public string MeasuresOfSuccess { get; set; }

        /// <summary>
        /// Gets or sets the time in days that will be allocated for the bet.
        /// </summary>
        public int TimeTotal { get; set; }
    }
}
