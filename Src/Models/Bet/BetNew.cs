namespace ProjectSpeedy
{
   /**
   * Used to create a new bet.
   **/
   class BetNew
   {
      /**
      * Gets or sets the name of the bet.
      **/
      public string Name {get; set;}

      /**
      * Gets or sets the description of the bet.
      **/
      public string Description {get; set;}

      /**
      * Gets or sets the measures of success the bet will be judged against.
      **/
      public string MeasuresOfSuccess {get; set;}
   }
}