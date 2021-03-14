namespace ProjectSpeedy.General
{
    /**
    * Used to display brief information on an item (generally used to populate the tiles).
    **/
    public class ListItem
    {
        /**
        * Gets or sets the unique identifier of the item.
        **/
        public string Id { get; set; }

        /**
        * Gets or sets the name of the item.
        **/
        public string Name { get; set; }

        /**
        * Gets or sets the status of the item.
        **/
        public string Status { get; set; }
    }
}
