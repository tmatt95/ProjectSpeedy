using ProjectSpeedy.Bet;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public class Bet : BetInterface
    {
        /// <inheritdoc />
        public bool Create(string projectId, string problemId, BetNew form)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string betId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public BetView Get(string projectId, string problemId, string betId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, BetNew form)
        {
            throw new System.NotImplementedException();
        }
    }
}