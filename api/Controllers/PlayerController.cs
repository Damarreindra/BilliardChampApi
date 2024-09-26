using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billiardchamps.Data;
using billiardchamps.Dtos;
using billiardchamps.Interface;
using billiardchamps.Mappers;
using billiardchamps.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace billiardchamps.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase

    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ApplicationDBContext _context;
        public PlayerController(IPlayerRepository playerRepository, ApplicationDBContext context)
        {
            _context = context;
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayer()
        {
            var players = await _playerRepository.GetPlayers();
            if (players == null)
            {
                return BadRequest(new { message = "Get Players Error" });
            }
            var playersList = players.Select(p => p.ToPlayerDto());
            return Ok(playersList);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] CreatePlayerDto createPlayerDto)
        {
            var existedPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Username == createPlayerDto.Username);
            if (existedPlayer != null)
            {
                return BadRequest(new { message = "Username already taken!" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playerModel = createPlayerDto.ToPlayerCreateDto();
            await _playerRepository.CreatePlayer(playerModel);
            return CreatedAtAction(nameof(GetById), new { id = playerModel.Id }, playerModel.ToPlayerDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existedPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            if(existedPlayer == null)
            return BadRequest(new{message = "Player not found"});
            await _playerRepository.DeletePlayer(id);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
            {
                return BadRequest(new { message = "Player Not Found" });
            }
            return Ok(player.ToPlayerDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePlayer([FromRoute] int id, [FromBody] UpdatePlayerDto playerDto)
        {
            var player = await _playerRepository.Update(id, playerDto);
            if (player == null)
            {
                return BadRequest(new { message = "Player not found" });
            }
            return Ok(player.ToPlayerDto());
        }

    }
}