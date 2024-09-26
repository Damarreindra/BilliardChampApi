using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Models;

namespace billiardchamps.Model
{
    public class Match
    {
        public int Id {get; set;} 
        public string Winner {get; set;} = string.Empty;

            public List<MatchPlayer> MatchPlayers { get; set; }

    }
}