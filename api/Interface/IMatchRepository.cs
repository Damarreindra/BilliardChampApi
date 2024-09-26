using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Dtos.MatchDto;
using billiardchamps.Model;
using billiardchamps.Models;

namespace billiardchamps.Interface
{
    public interface IMatchRepository
    {
        public Task<MatchPlayer> CreateMatchPlayer(MatchPlayer matchPlayer);

        public Task<Match> CreateMatch(Match match);

        public Task<bool> DeleteMatch(int gameId);

        public Task<Match> GetMatchById(int matchId);
        public Task<List<Match>> GetAllMatches();

        public Task<MatchPlayer> UpdatePoint(int matchId, int playerId, UpdateMatchPlayerDto updateMatchPlayerDto);

        public Task<MatchPlayer> UpdateWinner(int matchId);
    }
}