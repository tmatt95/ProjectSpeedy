namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public class Bet : IBet
    {
        /// <inheritdoc />
        public bool Create(string projectId, string problemId, Models.Bet.BetNew form)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string betId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Models.Bet.BetView Get(string projectId, string problemId, string betId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, Models.Bet.BetNew form)
        {
            throw new System.NotImplementedException();
        }
    }
}