namespace ProjectSpeedy.Problem
{
  /**
  * Used to add a new problem to the application.
  **/
  public class ProblemNew
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
  }
}
