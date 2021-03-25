using System.Threading.Tasks;
using ProjectSpeedy.Models.Projects;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    /// <summary>
    /// A project with dummy data
    /// </summary>
    public class ProjectData : IProject
    {
        /// <inheritdoc />
        public async Task<bool> CreateAsync(Models.Project.ProjectNew form)
        {
            return await Task.FromResult(true);
        }

        /// <inheritdoc />
        public bool Delete(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ProjectsView> GetAll()
        {
            return await Task.FromResult(new ProjectsView(){
                rows = new System.Collections.Generic.List<Models.General.ListItem>(){
                    new Models.General.ListItem(){
                        Name = "Project Name",
                        Id = "project:ProjectId"
                    }
                }
            });
        }

        /// <inheritdoc />
        public async Task<ProjectSpeedy.Models.Project.Project> Get(string projectId)
        {
            return await Task.FromResult(new Models.Project.Project()
            {
                Name = "Project Name",
                Problems = new System.Collections.Generic.List<Models.General.ListItem>(){
                    new Models.General.ListItem(){
                         Name = "Problem Name",
                         Id="problem:ProblemId",
                    }
                }
            });
        }

        /// <inheritdoc />
        public Task<bool> Update(string projectId, Models.Project.ProjectUpdate form)
        {
            return Task.FromResult(true);
        }
    }
}