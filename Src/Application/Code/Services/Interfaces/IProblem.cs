namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All problem related services.
    /// </summary>
    public interface IProblem
    {
        /// <summary>
        /// This will get all the information on the problem (including a list of linked bets).
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <returns>Information on a problem.</returns>
        System.Threading.Tasks.Task<Models.Problem.Problem> GetAsync(string projectId, string problemId);

        /// <summary>
        /// This will let you add a problem to a project. You will then be able to place bets against
        /// the problem and try and solve it.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <returns>If the problem was added successfully.</returns>
        System.Threading.Tasks.Task<bool> CreateAsync(string projectId, ProjectSpeedy.Models.Problem.ProblemNew form);

        /// <summary>
        /// Ideally before adding bets, this API will be used to add as much information as possible to the 
        /// problem. All the bets made will try and solve this. Try and make the desciption as clear as possible 
        /// and the success criteria unambigous so it is clear when the project has been solved.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <param name="form">Form containing updated data</param>
        /// <returns>If the problem was updated successfully.</returns>
        System.Threading.Tasks.Task<bool> UpdateAsync(string projectId, string problemId, ProjectSpeedy.Models.Problem.ProblemUpdate form);

        /// <summary>
        /// This action will delete the problem and all linked bets.
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <param name="problemId">Problem identifier</param>
        /// <returns>If the problem was deleted successfully.</returns>
        bool Delete(string projectId, string problemId);
    }
}