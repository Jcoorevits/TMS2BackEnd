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
    public class PumpLogController : ControllerBase
    {
        private readonly Tms2Context _context;

        public PumpLogController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/PumpLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PumpLog>>> GetPumpLogs()
        {
          if (_context.PumpLogs == null)
          {
              return NotFound();
          }
            return await _context.PumpLogs.ToListAsync();
        }

        // GET: api/PumpLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PumpLog>> GetPumpLog(long id)
        {
          if (_context.PumpLogs == null)
          {
              return NotFound();
          }
            var pumpLog = await _context.PumpLogs.FindAsync(id);

            if (pumpLog == null)
            {
                return NotFound();
            }

            return pumpLog;
        }

        // PUT: api/PumpLog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPumpLog(long id, PumpLog pumpLog)
        {
            if (id != pumpLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(pumpLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PumpLogExists(id))
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

        // POST: api/PumpLog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PumpLog>> PostPumpLog(PumpLog pumpLog)
        {
          if (_context.PumpLogs == null)
          {
              return Problem("Entity set 'Tms2Context.PumpLogs'  is null.");
          }
            _context.PumpLogs.Add(pumpLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPumpLog", new { id = pumpLog.Id }, pumpLog);
        }

        // DELETE: api/PumpLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePumpLog(long id)
        {
            if (_context.PumpLogs == null)
            {
                return NotFound();
            }
            var pumpLog = await _context.PumpLogs.FindAsync(id);
            if (pumpLog == null)
            {
                return NotFound();
            }

            _context.PumpLogs.Remove(pumpLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PumpLogExists(long id)
        {
            return (_context.PumpLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
