using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Dtos;
using billiardchamps.Model;

namespace billiardchamps.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerDto ToPlayerDto(this Player playerModel){
            return new PlayerDto{
                Id = playerModel.Id,
                Username = playerModel.Username,
                Win = playerModel.Win,
                PhotoUrl = playerModel.PhotoUrl
            };
        }

        public static Player ToPlayerCreateDto(this CreatePlayerDto createPlayerDto){
            return new Player{
                Username  = createPlayerDto.Username,
                PhotoUrl = createPlayerDto.PhotoUrl
            };
        }

        
    }
}