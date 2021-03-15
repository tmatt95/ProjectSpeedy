using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// All bet related services.
    /// </summary>
    public class ServiceBase : IServiceBase
    {
        private readonly IHttpClientFactory _clientFactory;

        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientFactory">Lets us carry out http requests from the app</param>
        /// <param name="configuration">Used to access application settings</param>
        public ServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this._clientFactory = clientFactory;
            this._configuration = configuration;
        }

        /// <inheritdoc />
        public async Task<string> DocumetCreate(object document, string partition)
        {
            string json = string.Empty;
            using (var stream = new MemoryStream())
            {
                //The Id of the new document
                string newId = await GenerateId();

                // Convert document to json 
                //Taken from https://stackoverflow.com/questions/58469794/c-net-core3-0-system-text-json-jsonserializer-serializeasync
                await JsonSerializer.SerializeAsync(stream, document);
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                string content = await reader.ReadToEndAsync();

                // Send the request to add the new document
                var request = new HttpRequestMessage(HttpMethod.Put, this._configuration["couchdb:url"] + partition + ":" + newId);
                request.Content = new StringContent(content);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._configuration["couchdb:authentication"]);
                var client = _clientFactory.CreateClient();

                // Convert response to output
                var response = await client.SendAsync(request);
                var test = await response.Content.ReadAsStringAsync();

                // Ensures is has created ok.
                response.EnsureSuccessStatusCode();

                // Returns the Id of the newly created record.
                return document.GetType().Name + ":" + newId;
            }
        }

        /// <inheritdoc />
        public async Task<string> GenerateId()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:5984/_uuids");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var uUIDs = await JsonSerializer.DeserializeAsync
                <Models.CouchDb.UUIDs>(responseStream);
            return uUIDs.uuids.First();
        }
    }
}