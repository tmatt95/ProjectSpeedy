using System.Threading.Tasks;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public class BetDataException : IBet
    {
        /// <inheritdoc />
        public System.Threading.Tasks.Task<bool> CreateAsync(string projectId, string problemId, Models.Bet.BetNew form)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string betId)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public System.Threading.Tasks.Task<Models.Bet.Bet> GetAsync(string projectId, string problemId, string betId)
        {
            throw new System.Exception("Exception");
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, Models.Bet.BetUpdate form)
        {
            throw new System.Exception("Exception");
        }
    }
}