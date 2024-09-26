using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Dtos;
using billiardchamps.Model;

namespace billiardchamps.Interface
{
    public interface IPlayerRepository
    {
        public Task<List<Player?>> GetPlayers(); 

        public Task<Player?> Update(int id, UpdatePlayerDto playerDto);

        public Task<Player> CreatePlayer(Player playerModel);
        public Task<Player?> GetPlayer(int id);

        public Task<Player?> DeletePlayer(int id);

        public Task<List<Player>> GetPlayersByIds(List<int> playerIds);
    }
}