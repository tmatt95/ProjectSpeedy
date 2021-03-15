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
        /// Create a new document
        /// </summary>
        /// <param name="document">document object to create</param>
        /// <param name="partition">the partition to create the document in</param>
        /// <returns>id of the newly created document.</returns>
        Task<string> DocumetCreate(object document, string partition);
    }
}