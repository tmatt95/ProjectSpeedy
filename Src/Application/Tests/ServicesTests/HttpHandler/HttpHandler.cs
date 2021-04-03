using System.Net.Http;
using System.Threading.Tasks;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    public class HttpHandler : IHttpHandler
    {
        public Task<HttpResponseMessage> DeleteAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.Accepted));
        }

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.Accepted));
        }
    }
}