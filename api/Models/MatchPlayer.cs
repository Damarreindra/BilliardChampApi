using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Model;

namespace billiardchamps.Models
{
   public class MatchPlayer
{
    public int MatchId { get; set; }
    public Match Match { get; set; }  

    public int PlayerId { get; set; }
    public Player Player { get; set; } 

    public int Points { get; set; }  
}

}