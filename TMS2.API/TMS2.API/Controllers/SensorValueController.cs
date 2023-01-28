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
    public class SensorValueController : ControllerBase
    {
        private readonly Tms2Context _context;

        public SensorValueController(Tms2Context context)
        {
            _context = context;
        }

        // GET: api/SensorValue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorValue>>> GetSensorValues()
        {
            if (_context.SensorValues == null)
            {
                return NotFound();
            }

            return await _context.SensorValues.ToListAsync();
        }

        // GET: api/SensorValue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorValue>> GetSensorValue(long id)
        {
            if (_context.SensorValues == null)
            {
                return NotFound();
            }

            var sensorValue = await _context.SensorValues.FindAsync(id);

            if (sensorValue == null)
            {
                return NotFound();
            }

            return sensorValue;
        }

        // PUT: api/SensorValue/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorValue(long id, SensorValue sensorValue)
        {
            if (id != sensorValue.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensorValue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorValueExists(id))
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

        // POST: api/SensorValue
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SensorValue>> PostSensorValue(SensorValue sensorValue)
        {
            if (_context.SensorValues == null)
            {
                return Problem("Entity set 'Tms2Context.SensorValues'  is null.");
            }

            var sensorController = new SensorController(_context);
            var sensorLogController = new SensorLogController(_context);
            var sensorLog = new SensorLog();
            var sensor = await sensorController.GetSensorById(sensorValue.SensorId);
            double sum = 0;
            long sensorValueId = 0;
            int calibration = sensor.Calibration;
            if (sensor.Calibration > 1)
            {
                calibration -= 1;
                sensor.Calibration = calibration;
                await sensorController.PutSensor(Convert.ToInt32(sensor.Id), sensor);
                _context.SensorValues.Add(sensorValue);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetSensorValue", new {id = sensorValue.Id}, sensorValue);
            }

            if (sensor.SensorValues != null && sensor.SensorValues.Count > 10)

            {
                var sensorValueList = sensor.SensorValues.OrderByDescending(x => x.Id).Take(4);
                foreach (var value in sensorValueList)
                {
                    if (sensorValueId < value.Id)
                    {
                        sensorValueId = value.Id;
                    }

                    sum += value.Value;
                }

                var average = sum / sensorValueList.Count();
                sensorValue.Average = average;

                if (sensorValue.Value > average * 1.5 || sensorValue.Value < average * 0.5)
                {
                    sensor.Calibration = 10;
                    await sensorController.PutSensor(Convert.ToInt32(sensor.Id), sensor);
                    sensorLog.SensorId = sensor.Id;
                    sensorLog.Date = DateTime.Now;
                    sensorLog.Error = $"Error detected in {sensor.Name}";
                    sensorLog.IsDefective = true;
                    sensorLog.SensorValueId = sensorValueId + 1;
                    await sensorLogController.PostSensorLog(sensorLog);
                }
            }

            _context.SensorValues.Add(sensorValue);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSensorValue", new {id = sensorValue.Id}, sensorValue);
        }

        // DELETE: api/SensorValue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorValue(long id)
        {
            if (_context.SensorValues == null)
            {
                return NotFound();
            }

            var sensorValue = await _context.SensorValues.FindAsync(id);
            if (sensorValue == null)
            {
                return NotFound();
            }

            _context.SensorValues.Remove(sensorValue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorValueExists(long id)
        {
            return (_context.SensorValues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}