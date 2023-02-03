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
    public class SensorController : ControllerBase
    {
        private readonly Tms2Context _context;

        public SensorController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/Sensor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensors()
        {
            if (_context.Sensors == null)
            {
                return NotFound();
            }
            // return await _context.Sensors.ToListAsync();

            return await _context.Sensors.Include(x => x.SensorValues)
                .Include(x => x.SensorLogs).Include(x => x.Pumps).Include(x => x.OldPumps)
                .ToListAsync();
        }

        // GET: api/Sensor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            if (_context.Sensors == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors.Include(x => x.SensorValues).Include(x => x.SensorLogs)
                .Include(x => x.Pumps).Include(x => x.OldPumps)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (sensor == null)
            {
                return NotFound();
            }

            return sensor;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Sensor> GetSensorById(long id) => await _context.Sensors.Include(x => x.SensorValues)
            .Include(x => x.SensorLogs)
            .Include(x => x.Pumps).Include(x => x.OldPumps)
            .FirstOrDefaultAsync(x => x.Id == id);

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Sensor> GetOnlySensorById(long id) =>
            await _context.Sensors.FirstOrDefaultAsync(x => x.Id == id);

        // PUT: api/Sensor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest();
            }

            if (sensor.SiteChange)
            {
                sensor.SiteChange = false;
                var sensorLogController = new SensorLogController(_context);
                var siteController = new SiteController(_context);
                var site = await siteController.GetSiteById(Convert.ToInt32(sensor.SiteId));
                var sensorLog = new SensorLog
                {
                    SensorId = sensor.Id,
                    Date = DateTime.Now,
                    SensorValueId = null,
                    IsDefective = false,
                    Error = $"{sensor.User} added {sensor.Name} to {site.Name}"
                };
                await sensorLogController.PostSensorLog(sensorLog);
                sensor.User = null;
                _context.Entry(sensor).State = EntityState.Modified;
            }


            if (sensor.SiteDelete)
            {
                var sensorLogController = new SensorLogController(_context);
                var siteController = new SiteController(_context);
                var site = await siteController.GetSiteById(Convert.ToInt32(sensor.SiteId));

                sensor.SiteDelete = false;
                sensor.SiteId = null;
                var sensorLog = new SensorLog
                {
                    SensorId = sensor.Id,
                    Date = DateTime.Now,
                    SensorValueId = null,
                    IsDefective = false,
                    Error = $"{sensor.User} removed {sensor.Name} from {site.Name}"
                };
                await sensorLogController.PostSensorLog(sensorLog);
                sensor.User = null;
                _context.Entry(sensor).State = EntityState.Modified;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
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

        // POST: api/Sensor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensor>> PostSensor(Sensor sensor)
        {
            if (_context.Sensors == null)
            {
                return Problem("Entity set 'Tms2Context.Sensors'  is null.");
            }

            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensor", new {id = sensor.Id}, sensor);
        }

        // DELETE: api/Sensor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            if (_context.Sensors == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorExists(int id)
        {
            return (_context.Sensors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}