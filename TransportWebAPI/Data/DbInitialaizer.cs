using TransportWebAPI.Models;
using Route = TransportWebAPI.Models.Route;

namespace TransportWebAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TransportDbContext db)
        {
            db.Database.EnsureCreated();

            if (db.Stops.Any()) return;

            int numberOfStops = 20;
            int numberOfRoutes = 20;
            int numberOfSchedules = 20;
            
            Random rand = new Random();

            // Добавляем остановки
            for (int i = 1; i <= numberOfStops; i++)
            {
                db.Stops.Add(new Stop
                {
                    Name = "Остановка_" + i,
                    IsTerminal = rand.Next(2) == 1,
                    HasDispatcher = rand.Next(2) == 1
                });
            }
            db.SaveChanges(); // Сохраняем остановки

            // Добавляем маршруты
            for (int i = 1; i <= numberOfRoutes; i++)
            {
                db.Routes.Add(new Route
                {
                    Name = "Маршрут_" + i,
                    TransportType = rand.Next(2) == 1 ? "Автобус" : "Троллейбус",
                    PlannedTravelTime = 30 + rand.Next(120),
                    Distance = (decimal)Math.Round(5 + rand.NextDouble() * 45, 2),
                    IsExpress = rand.Next(2) == 1
                });
            }
            db.SaveChanges(); // Сохраняем маршруты

            string[] daysOfWeek = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

            // Добавляем расписания
            for (int i = 1; i <= numberOfSchedules; i++)
            {
                db.Schedules.Add(new Schedule
                {
                    RouteId = rand.Next(1, numberOfRoutes + 1),
                    StopId = rand.Next(1, numberOfStops + 1),  // Убедись, что StopId существует в таблице Stops
                    Weekday = daysOfWeek[rand.Next(daysOfWeek.Length)],
                    ArrivalTime = TimeSpan.FromMinutes(rand.Next(1440)),
                    Year = DateTime.Now.Year - rand.Next(2)
                });
            }
            db.SaveChanges(); // Сохраняем расписания

          
        }

    }
}

