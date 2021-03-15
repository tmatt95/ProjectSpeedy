namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All problem related services.
    /// </summary>
    public class Problem : IProblem
    {
        /// <summary>
        /// Contains helper functions needed for all services to work
        /// </summary>
        private IServiceBase _serviceBase;

        /// <summary>
        /// All problem related services.
        /// </summary>
        /// <param name="serviceBase">Contains helper functions needed for all services to work.</param>
        public Problem(IServiceBase serviceBase)
        {
            this._serviceBase = serviceBase;
        }

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