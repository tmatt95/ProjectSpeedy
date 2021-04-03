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
        /// Carries out all http requests
        /// </summary>
        private readonly IHttpHandler _httpHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientFactory">Lets us carry out http requests from the app</param>
        /// <param name="configuration">Used to access application settings</param>
        public ServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpHandler httpHandler)
        {
            this._clientFactory = clientFactory;
            this._configuration = configuration;
            this._httpHandler = httpHandler;
        }

        /// <inheritdoc />
        public async Task<string> DocumentCreate(object document, string partition)
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

                // Convert response to output
                var response = await this._httpHandler.PutAsync(this._configuration["couchdb:document_create"].Replace("{documentId}", partition + ":" + newId), new StringContent(content));

                // Ensures is has created ok.
                response.EnsureSuccessStatusCode();

                // Returns the Id of the newly created record.
                return document.GetType().Name + ":" + newId;
            }
        }

        /// <inheritdoc />
        public async Task<HttpContent> ViewGet(string partition, string designDocumentName, string viewName, string startKey = "", string endKey = "")
        {
            // Send the request to add the new document.
            string requestAddress = "";

            // Sets the start and end key if specified.
            if (string.IsNullOrWhiteSpace(startKey) || string.IsNullOrWhiteSpace(endKey))
            {
                requestAddress = this._configuration["couchdb:view_get"].Replace("{partition}", partition)
                    .Replace("{designDocumentName}", designDocumentName)
                    .Replace("{viewName}", viewName);
            }
            else
            {
                requestAddress = this._configuration["couchdb:view_get_keys"].Replace("{partition}", partition)
                    .Replace("{designDocumentName}", designDocumentName)
                    .Replace("{viewName}", viewName)
                    .Replace("{startKey}", startKey)
                    .Replace("{endKey}", endKey);
            }

            // Convert response to output.
            var response = await this._httpHandler.GetAsync(requestAddress);

            // Ensures is has created ok.
            response.EnsureSuccessStatusCode();

            // Returns the Id of the newly created record.
            return response.Content;
        }

        /// <inheritdoc />
        public async Task<string> GenerateId()
        {
            var response = await this._httpHandler.GetAsync(this._configuration["couchdb:base_url"] + "_uuids");
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var uUIDs = await JsonSerializer.DeserializeAsync
                <Models.CouchDb.UuiDs>(responseStream);
            return uUIDs.uuids.First();
        }

        /// <inheritdoc />
        public async Task<HttpContent> DocumentGet(string documentId)
        {
            // Get document
            var response = await this._httpHandler.GetAsync(this._configuration["couchdb:document_get"].Replace("{documentId}", documentId));

            // Ensures is has created ok.
            response.EnsureSuccessStatusCode();

            // Returns the Id of the newly created record.
            return response.Content;
        }

        /// <inheritdoc />
        public async Task<bool> DocumentUpdate(string documentId, object document)
        {
            using (var stream = new MemoryStream())
            {
                // Convert document to json 
                //Taken from https://stackoverflow.com/questions/58469794/c-net-core3-0-system-text-json-jsonserializer-serializeasync
                await JsonSerializer.SerializeAsync(stream, document);
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                string content = await reader.ReadToEndAsync();

                // Convert response to output
                var response = await this._httpHandler.PutAsync(this._configuration["couchdb:document_update"].Replace("{documentId}", documentId), new StringContent(content));

                // Ensures is has created ok.
                response.EnsureSuccessStatusCode();

                // Returns the Id of the newly created record.
                return true;
            }
        }
    }
}