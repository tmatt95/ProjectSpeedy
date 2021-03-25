using System.Net.Http;
using System.Threading.Tasks;

using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// Contains dummy data wich returns what is expected when functions work ok.
    /// </summary>
    public class ProblemDataNotFound : IProblem
    {
        /// <inheritdoc />
        public System.Threading.Tasks.Task<bool> CreateAsync(string projectId, Models.Problem.ProblemNew form)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public System.Threading.Tasks.Task<Models.Problem.Problem> GetAsync(string projectId, string problemId)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Problem.ProblemUpdate form)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }
    }
}