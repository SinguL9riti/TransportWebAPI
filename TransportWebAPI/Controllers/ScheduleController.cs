using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportWebAPI.Models;
using TransportWebAPI.Data;

namespace TransportWebAPI.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly TransportDbContext _context;

        public ScheduleController(TransportDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.Schedules
                .Include(s => s.Route)
                .Include(s => s.Stop)
                .ToListAsync();
            
            ViewBag.Routes = await _context.Routes.ToListAsync();
            ViewBag.Stops = await _context.Stops.ToListAsync();

            return View(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Schedules.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Перенаправляем на страницу списка расписаний
            }

            // Если модель не прошла валидацию, заново отобразим форму с ошибками
            ViewBag.Routes = await _context.Routes.ToListAsync();
            ViewBag.Stops = await _context.Stops.ToListAsync();
            return View("Index", await _context.Schedules.ToListAsync());
        }
    }
}
