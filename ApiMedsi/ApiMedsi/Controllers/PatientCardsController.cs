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
    public class PatientCardsController : ControllerBase
    {
        private readonly MedsiContext _context;

        public PatientCardsController(MedsiContext context)
        {
            _context = context;
        }

        // GET: api/PatientCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientCard>>> GetPatientCards()
        {
          if (_context.PatientCards == null)
          {
              return NotFound();
          }
            return await _context.PatientCards.ToListAsync();
        }

        // GET: api/PatientCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientCard>> GetPatientCard(int id)
        {
          if (_context.PatientCards == null)
          {
              return NotFound();
          }
            var patientCard = await _context.PatientCards.FindAsync(id);

            if (patientCard == null)
            {
                return NotFound();
            }

            return patientCard;
        }

        // PUT: api/PatientCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientCard(int id, PatientCard patientCard)
        {
            if (id != patientCard.IdPatientCard)
            {
                return BadRequest();
            }

            _context.Entry(patientCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientCardExists(id))
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

        // POST: api/PatientCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientCard>> PostPatientCard(PatientCard patientCard)
        {
          if (_context.PatientCards == null)
          {
              return Problem("Entity set 'MedsiContext.PatientCards'  is null.");
          }
            _context.PatientCards.Add(patientCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientCard", new { id = patientCard.IdPatientCard }, patientCard);
        }

        // DELETE: api/PatientCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientCard(int id)
        {
            if (_context.PatientCards == null)
            {
                return NotFound();
            }
            var patientCard = await _context.PatientCards.FindAsync(id);
            if (patientCard == null)
            {
                return NotFound();
            }

            _context.PatientCards.Remove(patientCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientCardExists(int id)
        {
            return (_context.PatientCards?.Any(e => e.IdPatientCard == id)).GetValueOrDefault();
        }
    }
}
