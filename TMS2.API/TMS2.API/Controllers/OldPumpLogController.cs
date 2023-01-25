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
    public class OldPumpLogController : ControllerBase
    {
        private readonly Tms2Context _context;

        public OldPumpLogController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/OldPumpLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OldPumpLog>>> GetOldPumpLogs()
        {
          if (_context.OldPumpLogs == null)
          {
              return NotFound();
          }
            return await _context.OldPumpLogs.ToListAsync();
        }

        // GET: api/OldPumpLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OldPumpLog>> GetOldPumpLog(long id)
        {
          if (_context.OldPumpLogs == null)
          {
              return NotFound();
          }
            var oldPumpLog = await _context.OldPumpLogs.FindAsync(id);

            if (oldPumpLog == null)
            {
                return NotFound();
            }

            return oldPumpLog;
        }

        // PUT: api/OldPumpLog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOldPumpLog(long id, OldPumpLog oldPumpLog)
        {
            if (id != oldPumpLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(oldPumpLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OldPumpLogExists(id))
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

        // POST: api/OldPumpLog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OldPumpLog>> PostOldPumpLog(OldPumpLog oldPumpLog)
        {
          if (_context.OldPumpLogs == null)
          {
              return Problem("Entity set 'Tms2Context.OldPumpLogs'  is null.");
          }
            _context.OldPumpLogs.Add(oldPumpLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOldPumpLog", new { id = oldPumpLog.Id }, oldPumpLog);
        }

        // DELETE: api/OldPumpLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOldPumpLog(long id)
        {
            if (_context.OldPumpLogs == null)
            {
                return NotFound();
            }
            var oldPumpLog = await _context.OldPumpLogs.FindAsync(id);
            if (oldPumpLog == null)
            {
                return NotFound();
            }

            _context.OldPumpLogs.Remove(oldPumpLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OldPumpLogExists(long id)
        {
            return (_context.OldPumpLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
