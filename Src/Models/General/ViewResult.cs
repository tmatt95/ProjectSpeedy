namespace ProjectSpeedy.Models.General
{
    /// <summary>
    /// Used to display brief information on an item (generally used to populate the tiles).
    /// </summary>
    public class ViewResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the item.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the items key.
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Gets or sets the items value.
        /// </summary>
        public string value { get; set; }
    }
}
