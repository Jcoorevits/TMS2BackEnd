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
    public class PumpValueController : ControllerBase
    {
        private readonly Tms2Context _context;

        public PumpValueController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/PumpValue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PumpValue>>> GetPumpValues()
        {
            if (_context.PumpValues == null)
            {
                return NotFound();
            }

            return await _context.PumpValues.ToListAsync();
        }

        // GET: api/PumpValue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PumpValue>> GetPumpValue(long id)
        {
            if (_context.PumpValues == null)
            {
                return NotFound();
            }

            var pumpValue = await _context.PumpValues.FindAsync(id);

            if (pumpValue == null)
            {
                return NotFound();
            }

            return pumpValue;
        }

        // PUT: api/PumpValue/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPumpValue(long id, PumpValue pumpValue)
        {
            if (id != pumpValue.Id)
            {
                return BadRequest();
            }

            _context.Entry(pumpValue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PumpValueExists(id))
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

        // POST: api/PumpValue
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PumpValue>> PostPumpValue(PumpValue pumpValue)
        {
            if (_context.PumpValues == null)
            {
                return Problem("Entity set 'Tms2Context.PumpValues'  is null.");
            }

            if (pumpValue.Value > 40.0)
            {
                var pumpController = new PumpController(_context);
                var pump = new Pump();
                pump = await pumpController.GetPumpById(pumpValue.PumpId);
                pump.InputValue = 0.0;
                pump.IsDefective = true;
                await pumpController.PutPump(pump.Id, pump);
            }

            _context.PumpValues.Add(pumpValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPumpValue", new {id = pumpValue.Id}, pumpValue);
        }

        // DELETE: api/PumpValue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePumpValue(long id)
        {
            if (_context.PumpValues == null)
            {
                return NotFound();
            }

            var pumpValue = await _context.PumpValues.FindAsync(id);
            if (pumpValue == null)
            {
                return NotFound();
            }

            _context.PumpValues.Remove(pumpValue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PumpValueExists(long id)
        {
            return (_context.PumpValues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}