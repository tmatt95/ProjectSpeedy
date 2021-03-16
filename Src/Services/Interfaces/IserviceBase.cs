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
        /// Call a view and return the JSON to convert into a 
        /// </summary>
        /// <param name="viewName">Name of the view to call</param>
        /// <param name="partition">The partition to create the document in</param>
        /// <returns>Http content containing the view.</returns>
        Task<HttpContent> GetView(string viewName, string partition);

        /// <summary>
        /// Create a new document
        /// </summary>
        /// <param name="document">Document object to create</param>
        /// <param name="partition">The partition to create the document in</param>
        /// <returns>Id of the newly created document.</returns>
        Task<string> DocumetCreate(object document, string partition);
    }
}