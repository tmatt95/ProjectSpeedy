using System.Threading.Tasks;

using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// Contains dummy data wich returns what is expected when functions work ok.
    /// </summary>
    public class ProblemDataException : IProblem
    {
        /// <inheritdoc />
        public System.Threading.Tasks.Task<bool> CreateAsync(string projectId, Models.Problem.ProblemNew form)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public System.Threading.Tasks.Task<Models.Problem.Problem> GetAsync(string projectId, string problemId)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Problem.ProblemUpdate form)
        {
            throw new System.Exception("Exception");
        }
    }
}