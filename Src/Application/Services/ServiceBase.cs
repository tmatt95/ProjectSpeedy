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
        /// <summary>
        /// This will make the API calls without using a third party library.
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Needed to read the CouchDB settings for the application from appsettings.
        /// </summary>
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
                var request = new HttpRequestMessage(HttpMethod.Put, this._configuration["couchdb:base_url"] + this._configuration["couchdb:database_name"] + "/" + partition + ":" + newId);
                request.Content = new StringContent(content);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._configuration["couchdb:authentication"]);
                var client = _clientFactory.CreateClient();

                // Convert response to output
                var response = await client.SendAsync(request);

                // Ensures is has created ok.
                response.EnsureSuccessStatusCode();

                // Returns the Id of the newly created record.
                return document.GetType().Name + ":" + newId;
            }
        }

        /// <inheritdoc />
        public async Task<HttpContent> GetView(string partition, string designDocumentName, string viewName, string startKey = "", string endKey = "")
        {
            // Send the request to add the new document
            string requestAddress = "";

            // Sets the start and end key if specified
            if (string.IsNullOrWhiteSpace(startKey) || string.IsNullOrWhiteSpace(endKey))
            {
                requestAddress = this._configuration["couchdb:base_url"] + this._configuration["couchdb:database_name"] + "/_partition/" + partition + "/_design/" + designDocumentName + "/_view/" + viewName;
            }
            else
            {
                requestAddress = this._configuration["couchdb:base_url"] +
                    this._configuration["couchdb:database_name"] +
                    "/_partition/" +
                    partition +
                    "/_design/" + designDocumentName + "/_view/" +
                    viewName + "?startkey=%22" + startKey + "%22&endkey=%22" + endKey + "%22";
            }

            // Makes the request
            var request = new HttpRequestMessage(HttpMethod.Get, requestAddress);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._configuration["couchdb:authentication"]);
            var client = _clientFactory.CreateClient();

            // Convert response to output
            var response = await client.SendAsync(request);

            // TODO we need some couchdb view classes and then we can convert the views into output which links to swagger files / makes more sense.

            // Ensures is has created ok.
            response.EnsureSuccessStatusCode();

            // Returns the Id of the newly created record.
            return response.Content;
        }

        /// <inheritdoc />
        public async Task<string> GenerateId()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, this._configuration["couchdb:base_url"] + "_uuids");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var uUIDs = await JsonSerializer.DeserializeAsync
                <Models.CouchDb.UUIDs>(responseStream);
            return uUIDs.uuids.First();
        }

        /// <inheritdoc />
        public async Task<HttpContent> GetDocument(string documentId)
        {
            // Send the request to add the new document
            var request = new HttpRequestMessage(HttpMethod.Get, this._configuration["couchdb:base_url"] + this._configuration["couchdb:database_name"] + "/" + documentId);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._configuration["couchdb:authentication"]);
            var client = _clientFactory.CreateClient();

            // Convert response to output
            var response = await client.SendAsync(request);

            // TODO If 404 then throw not found

            // Ensures is has created ok.
            response.EnsureSuccessStatusCode();

            // Returns the Id of the newly created record.
            return response.Content;
        }
    }
}