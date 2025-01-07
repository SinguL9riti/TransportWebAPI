﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportWebAPI.Models;
using TransportWebAPI.Data;


namespace TransportWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesAPIController : ControllerBase
    {
        private readonly TransportDbContext _context;

        public SchedulesAPIController(TransportDbContext context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            return await _context.Schedules
                .Include(s => s.Route)
                .Include(s => s.Stop)
                .ToListAsync();
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules
                .Include(s => s.Route)
                .Include(s => s.Stop)
                .FirstOrDefaultAsync(s => s.ScheduleId == id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        // POST: api/Schedules
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.ScheduleId }, schedule);
        }

        // PUT: api/Schedules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
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

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.ScheduleId == id);
        }
        
    }
}
