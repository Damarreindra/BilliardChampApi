using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Models;

namespace billiardchamps.Dtos.MatchDto
{
    public class MatchDto
    {
        public int Id { get; set; }
        public string Winner { get; set; } = string.Empty;

     
        public List<MatchPlayerDto> Players { get; set; }
    }
}