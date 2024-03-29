﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FuryRent.Core.CarConstants;
using static FuryRent.Infrastructure.Constants.DataConstants;

namespace FuryRent.Core.Models.Car
{
    public class EditCarFormModel
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
        [StringLength(CarColorMaxLength, MinimumLength = CarColorMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        public int Kilometers { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int EngineTypeId { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int Horsepower { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int GearboxTypeId { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [DisplayFormat(DataFormatString = CarConstants.DateFormat)]
        public DateTime YearOfProduction { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int CategoryId { get; set; }

        public bool IsVipOnly { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public IEnumerable<EngineTypeViewModel> EngineTypes { get; set; } = new List<EngineTypeViewModel>();

        public IEnumerable<GearboxTypeViewModel> GearboxTypes { get; set; } = new List<GearboxTypeViewModel>();
    }
}
