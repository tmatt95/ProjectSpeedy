using System;
using System.Threading.Tasks;
using ProjectSpeedy.Models.BetComment;
using ProjectSpeedy.Models.BetFeedback;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet feedback related services.
    /// </summary>
    public class BetFeedback : IBetFeedback
    {
        /// <summary>
        /// Contains helper functions needed for all services to work
        /// </summary>
        private readonly IServiceBase _serviceBase;

        /// <summary>
        /// Prefix of id's in couchdb.
        /// </summary>
        public const string PREFIX = "bet_feedback:";

        /// <summary>
        /// Partitions allow CouchDB to scale better.
        /// </summary>
        public const string PARTITION = "bet_feedback";

        /// <summary>
        /// All bet feedback related services.
        /// </summary>
        /// <param name="serviceBase">Contains helper functions needed for all services to work.</param>
        public BetFeedback(IServiceBase serviceBase)
        {
            this._serviceBase = serviceBase;
        }

        /// <inheritdoc />
        public async Task<bool> CreateAsync(string projectId, string problemId, string betId, BetFeedbackNewUpdate form)
        {
            // The new feedback object
            var newBet = new ProjectSpeedy.Models.BetFeedback.BetFeedbackNewUpdate()
            {
                ProjectId = Project.PREFIX + projectId,
                ProblemId = Problem.PREFIX + problemId,
                BetId = Bet.PREFIX + betId,
                Comment = form.Comment,
                Created = DateTime.UtcNow
            };

            // Creates the project and checks if the id is returned.
            var newId = await this._serviceBase.DocumetCreate(newBet, BetFeedback.PARTITION);
            return !string.IsNullOrWhiteSpace(newId);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId, string betId, string commentId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, BetFeedbackNewUpdate form)
        {
            throw new NotImplementedException();
        }
    }
}