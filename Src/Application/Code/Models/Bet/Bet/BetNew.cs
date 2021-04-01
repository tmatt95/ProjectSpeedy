namespace ProjectSpeedy.Models.Bet
{
    /// <summary>
    /// Used to create a new bet.
    /// </summary>
    public class BetNew
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
        public string SuccessCriteria { get; set; }
    }
}
