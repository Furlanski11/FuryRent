﻿using System.ComponentModel.DataAnnotations;
using static FuryRent.Core.CarConstants;

namespace FuryRent.Core.Models.Car
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public int Kilometers { get; set; }

        public string Category { get; set; } = string.Empty;

        public string EngineType { get; set; } = string.Empty;

        public int Horsepower { get; set; }

        public string GearboxType { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = DateFormat)]
        public DateTime YearOfProduction { get; set; }

        public string PricePerDay { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;
    }
}
