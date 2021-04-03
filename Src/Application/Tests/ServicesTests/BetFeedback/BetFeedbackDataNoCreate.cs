using System.Threading.Tasks;
using ProjectSpeedy.Models.BetFeedback;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    public class BetFeedbackData : IBetFeedback
    {
        /// <inheritdoc />
        public Task<bool> CreateAsync(string projectId, string problemId, string betId, BetFeedbackNewUpdate form)
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId, string betId, string commentId)
        {
            return true;
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, BetFeedbackNewUpdate form)
        {
            return true;
        }
    }
}