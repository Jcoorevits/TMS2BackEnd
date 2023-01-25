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
    public class SensorLogController : ControllerBase
    {
        private readonly Tms2Context _context;

        public SensorLogController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/SensorLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorLog>>> GetSensorLogs()
        {
            if (_context.SensorLogs == null)
            {
                return NotFound();
            }

            return await _context.SensorLogs.ToListAsync();
        }

        // GET: api/SensorLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorLog>> GetSensorLog(long id)
        {
            if (_context.SensorLogs == null)
            {
                return NotFound();
            }

            var sensorLog = await _context.SensorLogs.FindAsync(id);

            if (sensorLog == null)
            {
                return NotFound();
            }

            return sensorLog;
        }
        

        // PUT: api/SensorLog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorLog(long id, SensorLog sensorLog)
        {
            if (id != sensorLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensorLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorLogExists(id))
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

        // POST: api/SensorLog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SensorLog>> PostSensorLog(SensorLog sensorLog)
        {
            if (_context.SensorLogs == null)
            {
                return Problem("Entity set 'Tms2Context.SensorLogs'  is null.");
            }

            _context.SensorLogs.Add(sensorLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensorLog", new {id = sensorLog.Id}, sensorLog);
        }

        // DELETE: api/SensorLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorLog(long id)
        {
            if (_context.SensorLogs == null)
            {
                return NotFound();
            }

            var sensorLog = await _context.SensorLogs.FindAsync(id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            _context.SensorLogs.Remove(sensorLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorLogExists(long id)
        {
            return (_context.SensorLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}