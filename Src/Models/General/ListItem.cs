namespace ProjectSpeedy.Models.General
{
    /// <summary>
    /// Used to display brief information on an item (generally used to populate the tiles).
    /// </summary>
    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ListItemValue value { get; set; }
    }
}
