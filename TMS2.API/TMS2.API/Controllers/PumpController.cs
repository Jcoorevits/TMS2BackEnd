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
    public class PumpController : ControllerBase
    {
        private readonly Tms2Context _context;

        public PumpController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/Pump
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pump>>> GetPumps()
        {
          if (_context.Pumps == null)
          {
              return NotFound();
          }
            return await _context.Pumps.ToListAsync();
        }

        // GET: api/Pump/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pump>> GetPump(long id)
        {
          if (_context.Pumps == null)
          {
              return NotFound();
          }
            var pump = await _context.Pumps.FindAsync(id);

            if (pump == null)
            {
                return NotFound();
            }

            return pump;
        }

        // PUT: api/Pump/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPump(long id, Pump pump)
        {
            if (id != pump.Id)
            {
                return BadRequest();
            }

            _context.Entry(pump).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PumpExists(id))
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

        // POST: api/Pump
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pump>> PostPump(Pump pump)
        {
          if (_context.Pumps == null)
          {
              return Problem("Entity set 'Tms2Context.Pumps'  is null.");
          }
            _context.Pumps.Add(pump);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPump", new { id = pump.Id }, pump);
        }

        // DELETE: api/Pump/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePump(long id)
        {
            if (_context.Pumps == null)
            {
                return NotFound();
            }
            var pump = await _context.Pumps.FindAsync(id);
            if (pump == null)
            {
                return NotFound();
            }

            _context.Pumps.Remove(pump);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PumpExists(long id)
        {
            return (_context.Pumps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
