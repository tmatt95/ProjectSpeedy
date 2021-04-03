using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Tests.ServicesTests
{
    public class HttpHandlerCreate : IHttpHandler
    {
        public Task<HttpResponseMessage> DeleteAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            var responseContent = new Models.CouchDb.UuiDs()
            {
                uuids = new System.Collections.Generic.List<string>(){
                    "NewId"
                }
            };
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(JsonSerializer.Serialize(responseContent));
            return Task.FromResult(response);
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