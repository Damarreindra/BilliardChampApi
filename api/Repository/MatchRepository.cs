using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Data;
using billiardchamps.Dtos.MatchDto;
using billiardchamps.Interface;
using billiardchamps.Model;
using billiardchamps.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace billiardchamps.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDBContext _context;
        public MatchRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Match> CreateMatch(Match match)
        {
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task<MatchPlayer> CreateMatchPlayer(MatchPlayer matchPlayer)
        {
            await _context.MatchPlayers.AddAsync(matchPlayer);
            await _context.SaveChangesAsync();
            return matchPlayer;
        }

        public async Task<bool> DeleteMatch(int gameId)
        {
            var match = await _context.Matches.Include(m => m.MatchPlayers).FirstOrDefaultAsync(m => m.Id == gameId);
            if (match == null)
            {
                return false;
            }
            _context.MatchPlayers.RemoveRange(match.MatchPlayers);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Match>> GetAllMatches()
        {
            return await _context.Matches
            .Include(m => m.MatchPlayers)
            .ThenInclude(mp => mp.Player)
            .ToListAsync();
        }

        public async Task<Match> GetMatchById(int matchId)
        {
            return await _context.Matches
            .Include(m => m.MatchPlayers)
            .ThenInclude(mp => mp.Player)
            .FirstOrDefaultAsync(m => m.Id == matchId);
        }

        public async Task<MatchPlayer?> UpdatePoint(int playerId, int matchId, UpdateMatchPlayerDto updateMatchPlayerDto)
        {

            var matchPlayer = await _context.MatchPlayers.FirstOrDefaultAsync(mp => mp.MatchId == matchId && mp.PlayerId == playerId);

            if (matchPlayer == null) return null;

            matchPlayer.Points = updateMatchPlayerDto.Points;

            await _context.SaveChangesAsync();

            return matchPlayer;
        }

        public async Task<MatchPlayer> UpdateWinner(int matchId)
        {
           var match = await _context.Matches
           .Include(m => m.MatchPlayers)
           .ThenInclude(mp => mp.Player)
           .FirstOrDefaultAsync(m => m.Id == matchId);


           if(match == null) return null;

           var winner = match.MatchPlayers.OrderByDescending(mp => mp.Points).FirstOrDefault();

           if(winner != null){
            match.Winner = winner.Player.Username;
            await _context.SaveChangesAsync();
            return winner;
           }

           return null;
        }
    }
}