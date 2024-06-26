﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FuryRent.Infrastructure.Constants.DataConstants;
using static FuryRent.Core.CarConstants;

namespace FuryRent.Core.Models.Car
{
    public class AddCarViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(CarMakeMaxLength, MinimumLength = CarMakeMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(CarModelMaxLength, MinimumLength = CarModeleMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Color must consist only of words")]
        [StringLength(CarColorMaxLength, MinimumLength = CarColorMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [Range(1, 1000000)]
        public int Kilometers { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int EngineTypeId { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [Range( 70, 1500)]
        public int Horsepower { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int GearboxTypeId { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [DisplayFormat(DataFormatString = CarConstants.DateFormat)]
        public DateTime YearOfProduction { get; set; } 


        [Required(ErrorMessage = RequireErrorMessage)]
        [Range(typeof(decimal),
            PricePerDayMinimum,
            PricePerDayMaximum,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = "Price per day must be a positive number and less than {2}")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int CategoryId { get; set; }
        public bool IsVipOnly { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public IEnumerable<EngineTypeViewModel> EngineTypes { get; set; } = new List<EngineTypeViewModel>();

        public IEnumerable<GearboxTypeViewModel> GearboxTypes { get; set; } = new List<GearboxTypeViewModel>();
    }
}
