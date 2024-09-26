using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Data;
using billiardchamps.Dtos;
using billiardchamps.Interface;
using billiardchamps.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace billiardchamps.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDBContext _context;
        public PlayerRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Player> CreatePlayer(Player playerModel)
        {   
        
           await _context.Players.AddAsync(playerModel);
           await _context.SaveChangesAsync();
           return playerModel;
        }

        public async Task<Player?> DeletePlayer(int id)
        {
           var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
           if(player == null){
            return null;
           }
           _context.Players.Remove(player);
           _context.SaveChangesAsync();
           return player;
        }

        public async Task<Player?> GetPlayer(int id)
        {
           var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
           if(player == null){
            return null;
           }
           return player;
        }

        public async Task<List<Player?>> GetPlayers()
        {
            var players = await _context.Players.ToListAsync();
            if(players == null){
                return null;
            }
            return players;
        }

        public async Task<List<Player>> GetPlayersByIds(List<int> playerIds)
        {
            return await _context.Players.Where(p => playerIds.Contains(p.Id)).ToListAsync();
        }

        public async Task<Player?> Update(int id, UpdatePlayerDto playerDto)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            if(player == null){
                return null;
            }
            player.PhotoUrl = playerDto.PhotoUrl;
            player.Username = playerDto.Username;
            await _context.SaveChangesAsync();
            return player;
        }
    }
}