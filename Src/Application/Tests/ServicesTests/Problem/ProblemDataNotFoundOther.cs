using System.Net.Http;

using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// Contains dummy data wich returns what is expected when functions work ok.
    /// </summary>
    public class ProblemDataNotFoundOther : IProblem
    {
        /// <inheritdoc />
        public System.Threading.Tasks.Task<bool> CreateAsync(string projectId, Models.Problem.ProblemNew form)
        {
            throw new HttpRequestException("Http error other",new System.Exception("Http error other"), System.Net.HttpStatusCode.Forbidden);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId)
        {
             throw new HttpRequestException("Http error other",new System.Exception("Http error other"), System.Net.HttpStatusCode.Forbidden);
        }

        /// <inheritdoc />
        public System.Threading.Tasks.Task<Models.Problem.Problem> GetAsync(string projectId, string problemId)
        {
            throw new HttpRequestException("Http error other",new System.Exception("Http error other"), System.Net.HttpStatusCode.Forbidden);
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Problem.ProblemUpdate form)
        {
             throw new HttpRequestException("Http error other",new System.Exception("Http error other"), System.Net.HttpStatusCode.Forbidden);
        }
    }
}