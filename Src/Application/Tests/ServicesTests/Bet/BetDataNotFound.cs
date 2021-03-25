using System.Net.Http;
using System.Threading.Tasks;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public class BetDataNotFound : IBet
    {
        /// <inheritdoc />
        public System.Threading.Tasks.Task<bool> CreateAsync(string projectId, string problemId, Models.Bet.BetNew form)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string betId)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public System.Threading.Tasks.Task<Models.Bet.Bet> GetAsync(string projectId, string problemId, string betId)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, Models.Bet.BetUpdate form)
        {
            throw new HttpRequestException("Document not found",new System.Exception("Document not found"), System.Net.HttpStatusCode.NotFound);
        }
    }
}