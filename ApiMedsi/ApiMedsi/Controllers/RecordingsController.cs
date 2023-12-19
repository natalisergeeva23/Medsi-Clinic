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
    public class RecordingsController : ControllerBase
    {
        private readonly MedsiContext _context;

        public RecordingsController(MedsiContext context)
        {
            _context = context;
        }

        // GET: api/Recordings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recording>>> GetRecordings()
        {
          if (_context.Recordings == null)
          {
              return NotFound();
          }
            return await _context.Recordings.ToListAsync();
        }

        // GET: api/Recordings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recording>> GetRecording(int id)
        {
          if (_context.Recordings == null)
          {
              return NotFound();
          }
            var recording = await _context.Recordings.FindAsync(id);

            if (recording == null)
            {
                return NotFound();
            }

            return recording;
        }

        // PUT: api/Recordings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecording(int id, Recording recording)
        {
            if (id != recording.IdRecording)
            {
                return BadRequest();
            }

            _context.Entry(recording).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordingExists(id))
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

        // POST: api/Recordings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recording>> PostRecording(Recording recording)
        {
          if (_context.Recordings == null)
          {
              return Problem("Entity set 'MedsiContext.Recordings'  is null.");
          }
            _context.Recordings.Add(recording);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecording", new { id = recording.IdRecording }, recording);
        }

        // DELETE: api/Recordings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecording(int id)
        {
            if (_context.Recordings == null)
            {
                return NotFound();
            }
            var recording = await _context.Recordings.FindAsync(id);
            if (recording == null)
            {
                return NotFound();
            }

            _context.Recordings.Remove(recording);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordingExists(int id)
        {
            return (_context.Recordings?.Any(e => e.IdRecording == id)).GetValueOrDefault();
        }
    }
}
