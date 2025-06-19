using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpilAPI.Data;
using SpilAPI.DTO;
using SpilAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpilAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoreDto>>> GetScores()
        {
            return await _context.Scores
                .Include(s => s.Bruger)
                .Include(s => s.Spil)
                .Select(s => new ScoreDto
                {
                    ScoreId = s.ScoreId,
                    BrugerId = s.BrugerId,
                    Brugernavn = s.Bruger.Brugernavn,
                    SpilId = s.SpilId,
                    Spilnavn = s.Spil.Navn,
                    Point = s.Point,
                    Dato = s.Dato
                })
                .ToListAsync();
        }

        // GET: api/scores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreDto>> GetScore(int id)
        {
            var score = await _context.Scores
                .Include(s => s.Bruger)
                .Include(s => s.Spil)
                .Where(s => s.ScoreId == id)
                .Select(s => new ScoreDto
                {
                    ScoreId = s.ScoreId,
                    BrugerId = s.BrugerId,
                    Brugernavn = s.Bruger.Brugernavn,
                    SpilId = s.SpilId,
                    Spilnavn = s.Spil.Navn,
                    Point = s.Point,
                    Dato = s.Dato
                })
                .FirstOrDefaultAsync();

            if (score == null)
                return NotFound();

            return score;
        }

        // POST: api/scores
        [HttpPost]
        public async Task<ActionResult<ScoreDto>> PostScore(ScoreCreateDto dto)
        {
            // Valider evt. at bruger og spil findes
            var bruger = await _context.Brugere.FindAsync(dto.BrugerId);
            var spil = await _context.Spil.FindAsync(dto.SpilId);

            if (bruger == null || spil == null)
                return BadRequest("Ugyldig BrugerId eller SpilId.");

            var score = new Score
            {
                BrugerId = dto.BrugerId,
                SpilId = dto.SpilId,
                Point = dto.Point,
                Dato = dto.Dato
            };

            _context.Scores.Add(score);
            await _context.SaveChangesAsync();

            // Returnér fx en DTO uden navigation properties
            var resultDto = new ScoreDto
            {
                ScoreId = score.ScoreId,
                BrugerId = score.BrugerId,
                SpilId = score.SpilId,
                Point = score.Point,
                Dato = score.Dato
                // evt. tilføj Brugernavn/Spilnavn hvis du vil
            };

            return CreatedAtAction(nameof(GetScore), new { id = score.ScoreId }, resultDto);
        }

        // PUT: api/scores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, Score score)
        {
            if (id != score.ScoreId)
                return BadRequest();

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Scores.Any(e => e.ScoreId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/scores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null)
                return NotFound();

            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
