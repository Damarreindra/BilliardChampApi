using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Dtos.MatchDto;
using billiardchamps.Model;
using billiardchamps.Models;

namespace billiardchamps.Mappers
{
    public static class MatchMapper
    {   

        public static MatchPlayerDto ToMatchPlayerDto(this MatchPlayer matchPlayer){
            return new MatchPlayerDto{
                PlayerId = matchPlayer.PlayerId,
                PlayerName = matchPlayer.Player.Username,
                Points = matchPlayer.Points,
                PhotoUrl = matchPlayer.Player.PhotoUrl
                
            };
        }
       public static MatchDto ToMatchDto(this Match matchModel){
        return new MatchDto{
            Id = matchModel.Id,
            Winner = matchModel.Winner,
            Players = matchModel.MatchPlayers.Select(p => p.ToMatchPlayerDto()).ToList()
        };
       }
    }
}