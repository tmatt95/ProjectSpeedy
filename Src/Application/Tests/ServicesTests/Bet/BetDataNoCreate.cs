using System.Threading.Tasks;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public class BetDataNoCreate : IBet
    {
        /// <inheritdoc />
        public async System.Threading.Tasks.Task<bool> CreateAsync(string projectId, string problemId, Models.Bet.BetNew form)
        {
            return await Task.FromResult(false);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string betId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task<Models.Bet.Bet> GetAsync(string projectId, string problemId, string betId)
        {
            return await Task.FromResult(new ProjectSpeedy.Models.Bet.Bet()
            {
                ProjectId = "ProjectId",
                ProblemId = "ProblemId",
                Name = "Bet Name"
            });
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, Models.Bet.BetUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}