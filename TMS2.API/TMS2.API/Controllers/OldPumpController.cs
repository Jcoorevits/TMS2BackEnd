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
    public class OldPumpController : ControllerBase
    {
        private readonly Tms2Context _context;

        public OldPumpController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/OldPump
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OldPump>>> GetOldPumps()
        {
            if (_context.OldPumps == null)
            {
                return NotFound();
            }

            return await _context.OldPumps.Include(x => x.OldPumpValues).Include(x => x.PumpLogs).ToListAsync();
        }

        // GET: api/OldPump/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OldPump>> GetOldPump(long id)
        {
            if (_context.OldPumps == null)
            {
                return NotFound();
            }

            var oldPump = await _context.OldPumps.Include(x => x.OldPumpValues).Include(x => x.PumpLogs)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (oldPump == null)
            {
                return NotFound();
            }

            return oldPump;
        }

        // PUT: api/OldPump/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOldPump(long id, OldPump oldPump)
        {
            if (id != oldPump.Id)
            {
                return BadRequest();
            }

            if (oldPump.SiteChange)
            {
           
                var oldPumpLogController = new OldPumpLogController(_context);
                var sensorController = new SensorController(_context);
                var sensor = await sensorController.GetOnlySensorById(Convert.ToInt32(oldPump.SensorId));
                oldPump.SiteChange = false;

                var oldPumpLog = new OldPumpLog
                {
                    OldPumpId = oldPump.Id,
                    Date = DateTime.Now,
                    Error = $"{oldPump.User} added {oldPump.Name} to {sensor.Name}",
                    OldPumpValueId = null,
                    IsDefective = false
                };
                await oldPumpLogController.PostOldPumpLog(oldPumpLog);
                oldPump.User = null;
                _context.Entry(oldPump).State = EntityState.Modified;
            }

            if (oldPump.SiteDelete)
            {
            
                var oldPumpLogController = new OldPumpLogController(_context);
                var sensorController = new SensorController(_context);
                var sensor = await sensorController.GetOnlySensorById(Convert.ToInt32(oldPump.SensorId));
                oldPump.SiteDelete = false;
                oldPump.SensorId = null;

                var oldPumpLog = new OldPumpLog
                {
                    OldPumpId = oldPump.Id,
                    Date = DateTime.Now,
                    Error = $"{oldPump.User} removed {oldPump.Name} from {sensor.Name}",
                    OldPumpValueId = null,
                    IsDefective = false
                };
                await oldPumpLogController.PostOldPumpLog(oldPumpLog);
                oldPump.User = null;
                _context.Entry(oldPump).State = EntityState.Modified;
            }

            if (oldPump.IsUserInput)
            {
                var oldPumpLogController = new OldPumpLogController(_context);
                var onOff = "off";
                if (oldPump.InputValue)
                {
                    onOff = "on";
                }

                var oldPumpLog = new OldPumpLog
                {
                    OldPumpId = oldPump.Id,
                    Date = DateTime.Now,
                    Error = $"{oldPump.User} changed input of {oldPump.Name} to {onOff}",
                    OldPumpValueId = null,
                    IsDefective = false
                };

                await oldPumpLogController.PostOldPumpLog(oldPumpLog);
                oldPump.User = null;
                _context.Entry(oldPump).State = EntityState.Modified;
            }

            if (oldPump.Repair)
            {
                oldPump.Repair = false;
                var oldPumpLogController = new OldPumpLogController(_context);
                var oldPumpLog = new OldPumpLog
                {
                    OldPumpId = oldPump.Id,
                    Date = DateTime.Now,
                    Error = $"{oldPump.User} repaired {oldPump.Name}",
                    OldPumpValueId = null,
                    IsDefective = false
                };

                await oldPumpLogController.PostOldPumpLog(oldPumpLog);
                oldPump.User = null;
                _context.Entry(oldPump).State = EntityState.Modified;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OldPumpExists(id))
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<OldPump> GetOldPumpById(long id) => await _context.OldPumps.Include(x => x.OldPumpValues)
            .Include(x => x.OldPumpValues)
            .FirstOrDefaultAsync(x => x.Id == id);

        // POST: api/OldPump
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OldPump>> PostOldPump(OldPump oldPump)
        {
            if (_context.OldPumps == null)
            {
                return Problem("Entity set 'Tms2Context.OldPumps'  is null.");
            }

            _context.OldPumps.Add(oldPump);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOldPump", new {id = oldPump.Id}, oldPump);
        }

        // // DELETE: api/OldPump/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteOldPump(long id)
        // {
        //     if (_context.OldPumps == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var oldPump = await _context.OldPumps.FindAsync(id);
        //     if (oldPump == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.OldPumps.Remove(oldPump);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }

        private bool OldPumpExists(long id)
        {
            return (_context.OldPumps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}