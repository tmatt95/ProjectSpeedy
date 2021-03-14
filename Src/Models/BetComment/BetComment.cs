using System;
using System.Collections.Generic;

namespace ProjectSpeedy.BetComment
{
  /**
  * A comment linked to a bet.
  **/
  public class BetComment
  {
     /**
     * Gets or sets the comment.
     **/
     public string Comment {get; set;}

     /**
     * Gets or sets the date the comment was created (added).
     **/
     public DateTime Created {get; set;}

     /**
     * Gets or sets the name of the person who added the comment.
     **/
     public string CreatedBy {get; set;}

     /**
     * Gets or sets the list of bets linked to this comment.
     **/
     public List<ProjectSpeedy.General.ListItem> LinkedBets {get; set;}
  }
}
