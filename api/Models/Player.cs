using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Models;

namespace billiardchamps.Model
{
    public class Player
    {
        public int Id {get;set;}

        public string Username {get; set;} = string.Empty;

       
        public string PhotoUrl {get; set;} = string.Empty;
    
        [DefaultValue(0)]
        public int Win {get; set;} 
            public List<MatchPlayer> MatchPlayers { get; set; }


    }
}