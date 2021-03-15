namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All problem related services.
    /// </summary>
    public class Problem : IProblem
    {
        /// <inheritdoc />
        public bool Create(string projectId, string problemId, Models.Problem.ProblemNew form)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Models.Problem.ProblemView Get(string projectId, string problemId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Problem.ProblemUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}