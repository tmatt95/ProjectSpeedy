using System;
using System.Threading.Tasks;
using ProjectSpeedy.Models.BetComment;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public class BetComment : IBetComment
    {
        /// <summary>
        /// Contains helper functions needed for all services to work
        /// </summary>
        private readonly IServiceBase _serviceBase;

        /// <summary>
        /// Prefix of id's in couchdb.
        /// </summary>
        public const string PREFIX = "bet_comment:";

        /// <summary>
        /// Partitions allow CouchDB to scale better.
        /// </summary>
        public const string PARTITION = "bet_comment";

        /// <summary>
        /// All bet comment related services.
        /// </summary>
        /// <param name="serviceBase">Contains helper functions needed for all services to work.</param>
        public BetComment(IServiceBase serviceBase)
        {
            this._serviceBase = serviceBase;
        }

        /// <inheritdoc />
        public async Task<bool> CreateAsync(string projectId, string problemId, string betId, BetCommentNewUpdate form)
        {
            // The new project object
            var newBet = new ProjectSpeedy.Models.BetComment.BetCommentNewUpdate()
            {
                ProjectId = projectId,
                ProblemId = problemId,
                BetId = betId,
                Comment = form.Comment,
                Created = DateTime.UtcNow
            };

            // Creates the project and checks if the id is returned.
            var newId = await this._serviceBase.DocumetCreate(newBet, BetComment.PARTITION);
            return !string.IsNullOrWhiteSpace(newId);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId, string betId, string commentId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Update(string projectId, string problemId, string betId, BetCommentNewUpdate form)
        {
            throw new NotImplementedException();
        }
    }
}