using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Core.Dto;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GamesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            var games = _mapper.Map<IEnumerable<GameDto>>(await _unitOfWork.GameRepository.GetAllAsync());
            return Ok(games);
        }
        
        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _unitOfWork.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }
            var gameDto = _mapper.Map<GameDto>(game);

            return Ok(gameDto);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDto gameDto)
        {
            if (gameDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var game = _mapper.Map<Game>(gameDto);// Map DTO to entity
            // Set the ID from the URL
            game.Id = id;
            //_context.Entry(game).State = EntityState.Modified;
            _unitOfWork.GameRepository.Update(game);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.TournamentRepository.AnyAsync(id))//checks if the game exists
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "An Error occured while updating tournament database");
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var tournamentExists = await _unitOfWork.TournamentRepository.AnyAsync(gameDto)

            var game = _mapper.Map<Game>(gameDto);
            _unitOfWork.GameRepository.Add(game);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while saving the Game: {ex.Message}");
            }

            var createdGameDto = _mapper.Map<GameDto>(game);

            return CreatedAtAction("GetGame", new { id = createdGameDto.Id }, createdGameDto);
        }


        
        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _unitOfWork.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _unitOfWork.GameRepository.Remove(game);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while deleting the game: {ex.Message}");
            }

            return NoContent();
        }
    }
}
