using Moq;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransportWebAPI.Controllers;
using TransportWebAPI.Models;
using TransportWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace TransportWebAPI.Tests
{
    public class SchedulesAPIControllerTests
    {
        private readonly TransportDbContext _context;
        private readonly SchedulesAPIController _controller;

        public SchedulesAPIControllerTests()
        {
            // Создаем in-memory базу данных для тестов
            var options = new DbContextOptionsBuilder<TransportDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTransportDb")
                .Options;

            _context = new TransportDbContext(options);

            // Добавляем тестовые данные
            if (!_context.Schedules.Any())
            {
                _context.Schedules.Add(new Schedule { ScheduleId = 1, RouteId = 1, StopId = 1, Weekday = "Monday", ArrivalTime = new TimeSpan(8, 0, 0), Year = 2025 });
                _context.Schedules.Add(new Schedule { ScheduleId = 2, RouteId = 2, StopId = 2, Weekday = "Tuesday", ArrivalTime = new TimeSpan(9, 0, 0), Year = 2025 });
                _context.SaveChanges();
            }

            _controller = new SchedulesAPIController(_context);
        }

        [Fact]
        public async Task GetSchedule_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetSchedule(999);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Schedule>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostSchedule_CreatesNewSchedule()
        {
            // Arrange
            var newSchedule = new Schedule { RouteId = 3, StopId = 3, Weekday = "Wednesday", ArrivalTime = new TimeSpan(10, 0, 0), Year = 2025 };

            // Act
            var result = await _controller.PostSchedule(newSchedule);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Schedule>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdSchedule = Assert.IsType<Schedule>(createdAtActionResult.Value);
            Assert.Equal(newSchedule.RouteId, createdSchedule.RouteId);
            Assert.Equal("Wednesday", createdSchedule.Weekday);
        }
    }
}
