using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportWebAPI.Models;
using TransportWebAPI.Data;

namespace TransportWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopsAPIController : ControllerBase
    {
        private readonly TransportDbContext _context;

        public StopsAPIController(TransportDbContext context)
        {
            _context = context;
        }

        // GET: api/Stops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stop>>> GetStops()
        {
            return await _context.Stops.ToListAsync();
        }

        // GET: api/Stops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stop>> GetStop(int id)
        {
            var stop = await _context.Stops.FindAsync(id);

            if (stop == null)
            {
                return NotFound();
            }

            return stop;
        }

        // POST: api/Stops
        [HttpPost]
        public async Task<ActionResult<Stop>> PostStop(Stop stop)
        {
            _context.Stops.Add(stop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStop), new { id = stop.StopId }, stop);
        }

        // PUT: api/Stops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStop(int id, Stop stop)
        {
            if (id != stop.StopId)
            {
                return BadRequest();
            }

            _context.Entry(stop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopExists(id))
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

        // DELETE: api/Stops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStop(int id)
        {
            var stop = await _context.Stops.FindAsync(id);
            if (stop == null)
            {
                return NotFound();
            }

            _context.Stops.Remove(stop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StopExists(int id)
        {
            return _context.Stops.Any(e => e.StopId == id);
        }
    }
}
