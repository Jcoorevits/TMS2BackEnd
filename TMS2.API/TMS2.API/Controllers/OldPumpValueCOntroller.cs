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

            var oldPumpController = new OldPumpController(_context);
            var oldPumpLogController = new OldPumpLogController(_context);
            var oldPumpLog = new OldPumpLog();
            var oldPump = await oldPumpController.GetOldPumpById(oldPumpValue.OldPumpId);
            long oldPumpValueId = 0;
            int calibration = oldPump.Calibration;
            double sum = 0;
            if (oldPump.IsUserInput)
            {
                oldPump.Calibration = 5;
                oldPump.IsUserInput = false;
                await oldPumpController.PutOldPump(Convert.ToInt32(oldPump.Id), oldPump);
                _context.OldPumpValues.Add(oldPumpValue);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetOldPumpValue", new {id = oldPumpValue.Id}, oldPumpValue);
            }

            if (oldPump.Calibration > 1)
            {
                calibration -= 1;
                oldPump.Calibration = calibration;
                await oldPumpController.PutOldPump(Convert.ToInt32(oldPump.Id), oldPump);
                _context.OldPumpValues.Add(oldPumpValue);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetOldPumpValue", new {id = oldPumpValue.Id}, oldPumpValue);
            }

            if (oldPump.OldPumpValues != null && oldPump.OldPumpValues.Count > 10)
            {
                var oldPumpValueList = oldPump.OldPumpValues.OrderByDescending(x => x.Id).Take(4);
                foreach (var value in oldPumpValueList)
                {
                    if (oldPumpValueId < value.Id)
                    {
                        oldPumpValueId = value.Id;
                    }

                    sum += value.Value;
                }

                if (oldPumpValue.FlowRate == 0 && oldPump.InputValue == true)
                {
                    var sendmail = new SendMail.SendMail();
                    var sensorController = new SensorController(_context);
                    var siteController = new SiteController(_context);
                    var sensor = await sensorController.GetOnlySensorById(oldPumpValue.OldPumpId);
                    var site = await siteController.GetSiteById(Convert.ToInt32(sensor.SiteId));
                    await sendmail.SendError(site.Email, $"An error has occured at {site.Name}",
                        $"Error detected in {oldPump.Name}, pump shut down out of precaution because water flow rate has stopped please check the logs for more information on this problem.");
                    oldPump.InputValue = false;
                    oldPump.IsDefective = true;
                    oldPump.Calibration = 5;
                    await oldPumpController.PutOldPump(Convert.ToInt32(oldPump.Id), oldPump);
                    oldPumpLog.OldPumpId = oldPump.Id;
                    oldPumpLog.Date = DateTime.Now;
                    oldPumpLog.Error = $"Water flow rate has stopped, {oldPump.Name} shut down out of precaution";
                    oldPumpLog.IsDefective = true;
                    oldPumpLog.OldPumpValueId = oldPumpValueId + 1;
                    await oldPumpLogController.PostOldPumpLog(oldPumpLog);
                }

                var average = sum / oldPumpValueList.Count();
                if (oldPumpValue.Value > average * 1.2 || oldPumpValue.Value < average * 0.8)
                {
                    var sendmail = new SendMail.SendMail();
                    var sensorController = new SensorController(_context);
                    var siteController = new SiteController(_context);
                    var sensor = await sensorController.GetOnlySensorById(oldPumpValue.OldPumpId);
                    var site = await siteController.GetSiteById(Convert.ToInt32(sensor.SiteId));
                    await sendmail.SendError(site.Email, $"An error has occured at {site.Name}",
                        $"Error detected in {oldPump.Name}, please check the logs for more information on this problem.");
                    oldPump.InputValue = false;
                    oldPump.IsDefective = true;
                    oldPump.Calibration = 5;
                    await oldPumpController.PutOldPump(Convert.ToInt32(oldPump.Id), oldPump);
                    oldPumpLog.OldPumpId = oldPump.Id;
                    oldPumpLog.Date = DateTime.Now;
                    oldPumpLog.Error = $"Error detected in {oldPump.Name}";
                    oldPumpLog.IsDefective = true;
                    oldPumpLog.OldPumpValueId = oldPumpValueId + 1;
                    await oldPumpLogController.PostOldPumpLog(oldPumpLog);
                }
            }

            _context.OldPumpValues.Add(oldPumpValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOldPumpValue", new {id = oldPumpValue.Id}, oldPumpValue);
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