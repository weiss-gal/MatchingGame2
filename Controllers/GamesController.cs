using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchingGame2.database;
using MatchingGame2.models.game;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MatchingGame2.Controllers
{
    [Route("api/games")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<ActionResult<IEnumerable<GameView>>> GetGames()
        {
            return await _context.View_ActiveGames.AsNoTracking().ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameView>> GetGame(int id)
        {
            var game = await _context.View_ActiveGames.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PATCH: api/games/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id, [FromBody] JsonPatchDocument<GamePatchDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var gameEntity= await _context.View_ActiveGames.FindAsync(id);

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
        public async Task<ActionResult<GameView>> PostGame([FromBody]GameCreateDto game)
        {
            var gameToAdd = _mapper.Map<Game>(game);

            _context.Games.Add(gameToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = gameToAdd.Id }, _mapper.Map<GameView>(gameToAdd));
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            game.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.View_ActiveGames.Any(e => e.Id == id);
        }
    }
}
