using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS2.DAL.Data;
using TMS2.DAL.Models;

namespace TMS2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldPumpValueCOntroller : ControllerBase
    {
        private readonly Tms2Context _context;

        public OldPumpValueCOntroller(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/OldPumpValueCOntroller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OldPumpValue>>> GetOldPumpValues()
        {
          if (_context.OldPumpValues == null)
          {
              return NotFound();
          }
            return await _context.OldPumpValues.ToListAsync();
        }

        // GET: api/OldPumpValueCOntroller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OldPumpValue>> GetOldPumpValue(long id)
        {
          if (_context.OldPumpValues == null)
          {
              return NotFound();
          }
            var oldPumpValue = await _context.OldPumpValues.FindAsync(id);

            if (oldPumpValue == null)
            {
                return NotFound();
            }

            return oldPumpValue;
        }

        // PUT: api/OldPumpValueCOntroller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOldPumpValue(long id, OldPumpValue oldPumpValue)
        {
            if (id != oldPumpValue.Id)
            {
                return BadRequest();
            }

            _context.Entry(oldPumpValue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OldPumpValueExists(id))
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

        // POST: api/OldPumpValueCOntroller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OldPumpValue>> PostOldPumpValue(OldPumpValue oldPumpValue)
        {
          if (_context.OldPumpValues == null)
          {
              return Problem("Entity set 'Tms2Context.OldPumpValues'  is null.");
          }
            _context.OldPumpValues.Add(oldPumpValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOldPumpValue", new { id = oldPumpValue.Id }, oldPumpValue);
        }

        // DELETE: api/OldPumpValueCOntroller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOldPumpValue(long id)
        {
            if (_context.OldPumpValues == null)
            {
                return NotFound();
            }
            var oldPumpValue = await _context.OldPumpValues.FindAsync(id);
            if (oldPumpValue == null)
            {
                return NotFound();
            }

            _context.OldPumpValues.Remove(oldPumpValue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OldPumpValueExists(long id)
        {
            return (_context.OldPumpValues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
