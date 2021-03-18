using System.Collections.Generic;

namespace ProjectSpeedy.Models.CouchDb.View
{
    /// <summary>
    /// Used to display brief information on an item (generally used to populate the tiles).
    /// </summary>
    public class ViewResult
    {
        /// <summary>
        /// Tiotal number of rows in the view result
        /// </summary>
        public int total_rows { get; set; }

        /// <summary>
        /// offset applied to list
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// Gets or sets the items value.
        /// </summary>
        public List<ProjectSpeedy.Models.CouchDb.View.ListItem> rows { get; set; }
    }
}
