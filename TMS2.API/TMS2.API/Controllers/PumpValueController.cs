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

            var pumpController = new PumpController(_context);
            var pumpLogController = new PumpLogController(_context);
            var pumpLog = new PumpLog();
            var pump = await pumpController.GetPumpById(pumpValue.PumpId);
            long pumpValueId = 0;
            int calibration = pump.Calibration;
            double sum = 0;
            if (pump.IsUserInput)
            {
                pump.Calibration = 5;
                pump.IsUserInput = false;
                await pumpController.PutPump(Convert.ToInt32(pump.Id), pump);
                _context.PumpValues.Add(pumpValue);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPumpValue", new {id = pumpValue.Id}, pumpValue);
            }

            if (pump.Calibration > 1)
            {
                calibration -= 1;
                pump.Calibration = calibration;
                await pumpController.PutPump(Convert.ToInt32(pump.Id), pump);
                _context.PumpValues.Add(pumpValue);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPumpValue", new {id = pumpValue.Id}, pumpValue);
            }

            if (pump.PumpValues != null && pump.PumpValues.Count > 10)
            {
                var pumpValueList = pump.PumpValues.OrderByDescending(x => x.Id).Take(4);
                foreach (var value in pumpValueList)
                {
                    if (pumpValueId < value.Id)
                    {
                        pumpValueId = value.Id;
                    }

                    sum += value.Value;
                }

                if (pumpValue.FlowRate == 0 && pump.InputValue > 0.0)
                {
                    var sendmail = new SendMail.SendMail();
                    var sensorController = new SensorController(_context);
                    var siteController = new SiteController(_context);
                    var sensor = await sensorController.GetOnlySensorById(pumpValue.PumpId);
                    var site = await siteController.GetSiteById(Convert.ToInt32(sensor.SiteId));
                    await sendmail.SendError(site.Email, $"An error has occured at {site.Name}",
                        $"Error detected in {pump.Name}, pump shut down out of precaution because water flow rate has stopped, please check the logs for more information on this problem.");
                    pump.InputValue = 0.0;
                    pump.IsDefective = true;
                    pump.Calibration = 5;
                    await pumpController.PutPump(Convert.ToInt32(pump.Id), pump);
                    pumpLog.PumpId = pump.Id;
                    pumpLog.Date = DateTime.Now;
                    pumpLog.Error = $"Water flow rate has stopped, {pump.Name} shut down out of precaution";
                    pumpLog.IsDefective = true;
                    pumpLog.PumpValueId = pumpValueId + 1;
                    await pumpLogController.PostPumpLog(pumpLog);
                }

                var average = sum / pumpValueList.Count();
                if (pumpValue.Value > average * 1.2 || pumpValue.Value < average * 0.8)
                {
                    var sendmail = new SendMail.SendMail();
                    var sensorController = new SensorController(_context);
                    var siteController = new SiteController(_context);
                    var sensor = await sensorController.GetOnlySensorById(pumpValue.PumpId);
                    var site = await siteController.GetSiteById(Convert.ToInt32(sensor.SiteId));
                    await sendmail.SendError(site.Email, $"An error has occured at {site.Name}",
                        $"Error detected in {pump.Name}, please check the logs for more information on this problem.");
                    pump.InputValue = 0.0;
                    pump.IsDefective = true;
                    pump.Calibration = 5;
                    await pumpController.PutPump(Convert.ToInt32(pump.Id), pump);
                    pumpLog.PumpId = pump.Id;
                    pumpLog.Date = DateTime.Now;
                    pumpLog.Error = $"Error detected in {pump.Name}";
                    pumpLog.IsDefective = true;
                    pumpLog.PumpValueId = pumpValueId + 1;
                    await pumpLogController.PostPumpLog(pumpLog);
                }
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