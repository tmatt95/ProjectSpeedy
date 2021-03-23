namespace ProjectSpeedy.Models.CouchDb.View
{
    /// <summary>
    /// Used to display brief information on an item (generally used to populate the tiles).
    /// </summary>
    public class ListItem
    {
        /// <summary>
        /// Id of the record
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Key of the view
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Values stored against the view
        /// </summary>
        public ListItemValue value { get; set; }
    }
}
