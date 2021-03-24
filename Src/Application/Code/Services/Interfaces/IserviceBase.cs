using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectSpeedy.Services
{
    /// <summary>
    /// Contains an interface for a base class that all services should inherrit
    /// </summary>
    public interface IServiceBase
    {
        /// <summary>
        /// Generates a unique id.
        /// </summary>
        /// <returns>Unique Id</returns>
        Task<string> GenerateId();

        /// <summary>
        /// Gets an individual document.
        /// TODO Add caching to stop this call calling the API on every request.
        /// </summary>
        /// <param name="documentId">The id of the document</param>
        /// <returns>Http content containing the document.</returns>
        Task<HttpContent> GetDocument(string documentId);

        /// <summary>
        /// Updates an individual document.
        /// </summary>
        /// <param name="documentId">The id of the document</param>
        /// <param name="partition">The partition to update the document in</param>
        /// <param name="document">Document object to create</param>
        /// <returns>Http content containing the document.</returns>
        Task<bool> UpdateDocument(string documentId, string partition, object document);

        /// <summary>
        /// Call a view and return the JSON to convert into an object.
        /// TODO Add caching to stop this call calling the API on every request.
        /// </summary>
        /// <param name="partition">The partition to create the document in</param>
        /// <param name="designDocumentName">Name of the design document the view is attached to.</param>
        /// <param name="viewName">Name of the view to call</param>
        /// <param name="startKey">Start key for view search</param>
        /// <param name="endKey">End key for view search</param>
        /// <returns>Http content containing the view.</returns>
        Task<HttpContent> GetView(string partition, string designDocumentName, string viewName, string startKey = "", string endKey = "");

        /// <summary>
        /// Create a new document
        /// </summary>
        /// <param name="document">Document object to create</param>
        /// <param name="partition">The partition to create the document in</param>
        /// <returns>Id of the newly created document.</returns>
        Task<string> DocumetCreate(object document, string partition);
    }
}