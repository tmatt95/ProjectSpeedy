namespace ProjectSpeedy.Problem
{

  /**
  * Used to update problem details.
  **/
  public class ProblemUpdate
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
      * Gets or sets the criteria that will determine when we have solved the problem.
      **/
      public string SuccessCriteria {get; set;}

      /**
      * Gets or sets a link to the bet the problem was created from.
      **/
      public string creeatedFromPreviousBet {get; set;}
  }
}
