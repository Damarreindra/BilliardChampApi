using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billiardchamps.Dtos.MatchDto
{
   public class MatchPlayerDto
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }

    public string PhotoUrl {get; set;}
    public int Points { get; set; }
}

}