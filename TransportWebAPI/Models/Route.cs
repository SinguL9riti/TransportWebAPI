﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransportWebAPI.Models
{
    public partial class Route
    {
        [Display(Name = "Код маршрута")]
        public int RouteId { get; set; }

        [Display(Name = "Маршрут")]
        public string Name { get; set; } = null!;

        [Display(Name = "Тип транспорта")]
        public string TransportType { get; set; } = null!;

        [Display(Name = "Продолжительность")]
        public int PlannedTravelTime { get; set; }

        [Display(Name = "Дистанция")]
        public decimal Distance { get; set; }

        [Display(Name = "Экспресс")]
        public bool IsExpress { get; set; }

        [JsonIgnore]
        public virtual ICollection<Personnel> Personnel { get; set; } = new List<Personnel>();

        [JsonIgnore]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
