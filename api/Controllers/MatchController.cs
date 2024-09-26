using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Dtos.MatchDto;
using billiardchamps.Interface;
using billiardchamps.Mappers;
using billiardchamps.Model;
using billiardchamps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace billiardchamps.Controllers
{
    [Route("api/match")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        public MatchController(IPlayerRepository playerRepository, IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        [HttpPost("AddMatch")]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchDto dto)
        {
            var match = new Match();
            await _matchRepository.CreateMatch(match);
            foreach (var playerId in dto.PlayerIds)
            {
                var matchPlayer = new MatchPlayer
                {
                    PlayerId = playerId,
                    MatchId = match.Id,
                    Points = 0
                };
                await _matchRepository.CreateMatchPlayer(matchPlayer);
            }
            return Created();

        }

        [HttpPut("update-winner/{matchId:int}/{userId:int}")]
        public async Task<IActionResult> UpdatePoint([FromBody] UpdateMatchPlayerDto updateMatchPlayerDto, [FromRoute] int userId, int matchId)
        {
            var result = await _matchRepository.UpdatePoint(userId, matchId, updateMatchPlayerDto);

            if (result == null)
            {
                return NotFound(new { message = "Match or Player not found" });
            }

            return Ok(new { message = "Player points updated successfully" });
        }

        [HttpPost("{matchId}/set-winner")]
        public async Task<IActionResult> SetWinner(int matchId)
        {
            var winner = await _matchRepository.UpdateWinner(matchId);

            if (winner == null)
                return NotFound(new { message = "Match not found or no players in the match." });

            return Ok(new { message = "Winner set successfully", winner.Player.Username });
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteMatch(int gameId)
        {
            var result = await _matchRepository.DeleteMatch(gameId);
            if (!result)
            {
                return BadRequest(new { message = "Game not found" });
            }

            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatch(int id)
        {
            var match = await _matchRepository.GetMatchById(id);

            if (match == null)
            {
                return NotFound(new { message = "Match not found" });
            }


            return Ok(match.ToMatchDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMatches()
        {
            var matches = await _matchRepository.GetAllMatches();
            return Ok(matches.Select(m => m.ToMatchDto()));
        }

    }
}