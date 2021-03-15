namespace ProjectSpeedy.Models.Problem
{
    /// <summary>
    /// Used to add a new problem to the application.
    /// </summary>
    public class ProblemNew
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
    }
}
