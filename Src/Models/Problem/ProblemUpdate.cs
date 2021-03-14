namespace ProjectSpeedy.Problem
{

    /// <summary>
    /// Used to update problem details.
    /// </summary>
    public class ProblemUpdate
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
        /// Gets or sets the criteria that will determine when we have solved the problem.
        /// </summary>
        public string SuccessCriteria { get; set; }

        /// <summary>
        /// Gets or sets a link to the bet the problem was created from.
        /// </summary>
        public string creeatedFromPreviousBet { get; set; }
    }
}
