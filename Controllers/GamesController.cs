using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchingGame2;
using MatchingGame2.database;
using MatchingGame2.models.game;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace MatchingGame2.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly AzureSqlDbContext _context;
        private readonly IMapper _mapper;

        public GamesController(AzureSqlDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.AsNoTracking().ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        //// PUT: api/Games/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game game)
        //{
        //    if (id != game.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // PATCH: api/games/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id, [FromBody] JsonPatchDocument<GamePatchDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var gameEntity= await _context.Games.FindAsync(id);

            if (gameEntity == null)
                return NotFound();

            var gameToPatch = _mapper.Map<GamePatchDto>(gameEntity);
            patchDocument.ApplyTo(gameToPatch, ModelState);

            if (!TryValidateModel(gameToPatch))
                return BadRequest(ModelState);

            _mapper.Map(gameToPatch, gameEntity);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame([FromBody]GameCreateDto game)
        {
            var gameToAdd = _mapper.Map<Game>(game);

            _context.Games.Add(gameToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = gameToAdd.Id }, gameToAdd);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
