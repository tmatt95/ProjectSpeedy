namespace ProjectSpeedy.Models.General
{
    /// <summary>
    /// Used to display brief information on an item (generally used to populate the tiles).
    /// </summary>
    public class ListItem
    {
        /// <summary>
        /// Gets or sets the unique identifier of the item.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status of the item.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the address of the item.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Used to store icon classes.
        /// </summary>
        public string IconClasses { get; set; }
    }
}