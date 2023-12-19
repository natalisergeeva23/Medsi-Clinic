using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMedsi.Models;

namespace ApiMedsi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynopsesController : ControllerBase
    {
        private readonly MedsiContext _context;

        public SynopsesController(MedsiContext context)
        {
            _context = context;
        }

        // GET: api/Synopses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Synopsis>>> GetSynopses()
        {
          if (_context.Synopses == null)
          {
              return NotFound();
          }
            return await _context.Synopses.ToListAsync();
        }

        // GET: api/Synopses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Synopsis>> GetSynopsis(int id)
        {
          if (_context.Synopses == null)
          {
              return NotFound();
          }
            var synopsis = await _context.Synopses.FindAsync(id);

            if (synopsis == null)
            {
                return NotFound();
            }

            return synopsis;
        }

        // PUT: api/Synopses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSynopsis(int id, Synopsis synopsis)
        {
            if (id != synopsis.IdSynopsis)
            {
                return BadRequest();
            }

            _context.Entry(synopsis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SynopsisExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Synopses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Synopsis>> PostSynopsis(Synopsis synopsis)
        {
          if (_context.Synopses == null)
          {
              return Problem("Entity set 'MedsiContext.Synopses'  is null.");
          }
            _context.Synopses.Add(synopsis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSynopsis", new { id = synopsis.IdSynopsis }, synopsis);
        }

        // DELETE: api/Synopses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSynopsis(int id)
        {
            if (_context.Synopses == null)
            {
                return NotFound();
            }
            var synopsis = await _context.Synopses.FindAsync(id);
            if (synopsis == null)
            {
                return NotFound();
            }

            _context.Synopses.Remove(synopsis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SynopsisExists(int id)
        {
            return (_context.Synopses?.Any(e => e.IdSynopsis == id)).GetValueOrDefault();
        }
    }
}
