using TransportWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Route = TransportWebAPI.Models.Route;
using Microsoft.EntityFrameworkCore;

namespace TransportWebAPI.Data
{
    public class TransportDbContext : DbContext
    {
        public TransportDbContext(DbContextOptions<TransportDbContext> options) : base(options) { }

        public DbSet<Route> Routes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>()
                .HasKey(r => r.RouteId);

            modelBuilder.Entity<Schedule>()
                .HasKey(s => s.ScheduleId);

            modelBuilder.Entity<Stop>()
                .HasKey(st => st.StopId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Route)
                .WithMany(r => r.Schedules)
                .HasForeignKey(s => s.RouteId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Stop)
                .WithMany()
                .HasForeignKey(s => s.StopId);
        }
    }
}

