using System.Threading.Tasks;

using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// Contains dummy data wich returns what is expected when functions work ok.
    /// </summary>
    public class ProblemDataNoCreate : IProblem
    {
        /// <inheritdoc />
        public async System.Threading.Tasks.Task<bool> CreateAsync(string projectId, Models.Problem.ProblemNew form)
        {
            return await Task.FromResult(false);
        }

        /// <inheritdoc />
        public bool Delete(string projectId, string problemId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task<Models.Problem.Problem> GetAsync(string projectId, string problemId)
        {
            return await Task.FromResult(new Models.Problem.Problem(){
                Name = "Problem Name",
                Description = "Problem Description",
                Bets = new System.Collections.Generic.List<Models.General.ListItem>(){
                    new Models.General.ListItem(){
                        Name = "Bet Name",
                        Id = "BetId",
                        Status = "New"
                    }
                }
            });
        }

        /// <inheritdoc />
        public bool Update(string projectId, Models.Problem.ProblemUpdate form)
        {
            throw new System.NotImplementedException();
        }
    }
}