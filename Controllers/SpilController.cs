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
    public class SpilController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SpilController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/spil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpilDto>>> GetSpil()
        {
            return await _context.Spil
                .Select(s => new SpilDto
                {
                    SpilId = s.SpilId,
                    Navn = s.Navn
                   
                })
                .ToListAsync();
        }

        // GET: api/spil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpilDto>> GetSpil(int id)
        {
            var spil = await _context.Spil.FindAsync(id);

            if (spil == null)
                return NotFound();

            return new SpilDto
            {
                SpilId = spil.SpilId,
                Navn = spil.Navn
            };
        }

        // POST: api/spil
        [HttpPost]
        public async Task<ActionResult<SpilDto>> PostSpil(SpilCreateDto dto)
        {
            var spil = new Spil
            {
                Navn = dto.Navn
            };

            _context.Spil.Add(spil);
            await _context.SaveChangesAsync();

            var resultDto = new SpilDto
            {
                SpilId = spil.SpilId,
                Navn = spil.Navn
            };

            return CreatedAtAction(nameof(GetSpil), new { id = spil.SpilId }, resultDto);
        }

        // PUT: api/spil/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpil(int id, Spil spil)
        {
            if (id != spil.SpilId)
                return BadRequest();

            _context.Entry(spil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpilExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/spil/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpil(int id)
        {
            var spil = await _context.Spil.FindAsync(id);
            if (spil == null)
                return NotFound();

            _context.Spil.Remove(spil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpilExists(int id)
        {
            return _context.Spil.Any(e => e.SpilId == id);
        }
    }
}
