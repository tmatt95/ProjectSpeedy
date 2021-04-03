using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProjectSpeedy.Services;

namespace ProjectSpeedy.Services
{
    public class HttpHandler : IHttpHandler
    {
        /// <summary>
        /// This will make the API calls without using a third party library.
        /// </summary>

        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Needed to read the CouchDB settings for the application from appsettings.
        /// </summary>
        private readonly IConfiguration _configuration;

        public HttpHandler(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            this._configuration = configuration;
            this._clientFactory = clientFactory;
        }

        public Task<HttpResponseMessage> DeleteAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._configuration["couchdb:authentication"]);
            var client = _clientFactory.CreateClient();
            return client.SendAsync(request);
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = content;
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._configuration["couchdb:authentication"]);
            var client = _clientFactory.CreateClient();
            return client.SendAsync(request);
        }

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = content;
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._configuration["couchdb:authentication"]);
            var client = _clientFactory.CreateClient();
            return client.SendAsync(request);
        }
    }
}