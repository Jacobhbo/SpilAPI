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
    public class BrugereController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrugereController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/brugere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrugerDto>>> GetBrugere()
        {
            return await _context.Brugere
                .Select(b => new BrugerDto { Id = b.BrugerId, Brugernavn = b.Brugernavn })
                .ToListAsync();
        }

        // GET: api/brugere/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrugerDto>> GetBruger(int id)
        {
            var bruger = await _context.Brugere.FindAsync(id);

            if (bruger == null)
                return NotFound();

            return new BrugerDto { Id = bruger.BrugerId, Brugernavn = bruger.Brugernavn };
        }

        // POST: api/brugere
        [HttpPost]
        public async Task<ActionResult<BrugerDto>> PostBruger(BrugerCreateDto brugerDto)
        {
            // Tjek om brugernavn allerede findes
            if (await _context.Brugere.AnyAsync(b => b.Brugernavn == brugerDto.Brugernavn))
                return BadRequest("Brugernavn er allerede taget");

            // Lav en Bruger model ud fra DTO
            var bruger = new Bruger
            {
                Brugernavn = brugerDto.Brugernavn,
                Password = brugerDto.Password // (husk hashing i production)
            };

            _context.Brugere.Add(bruger);
            await _context.SaveChangesAsync();

            // Returner en DTO uden password
            var dto = new BrugerDto
            {
                Id = bruger.BrugerId,
                Brugernavn = bruger.Brugernavn
            };

            return CreatedAtAction(nameof(GetBruger), new { id = bruger.BrugerId }, dto);
        }

        // PUT: api/brugere/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBruger(int id, Bruger bruger)
        {
            if (id != bruger.BrugerId)
                return BadRequest();

            _context.Entry(bruger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrugerExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/brugere/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBruger(int id)
        {
            var bruger = await _context.Brugere.FindAsync(id);
            if (bruger == null)
                return NotFound();

            _context.Brugere.Remove(bruger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrugerExists(int id)
        {
            return _context.Brugere.Any(e => e.BrugerId == id);
        }
    }
}
