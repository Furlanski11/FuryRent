﻿
namespace FuryRent.Infrastructure.Constants
{
    public static class DataConstants
    {
        public const string RequiredFieldMessage = "The field is required";

        public const int CarMakeMinLength = 3;
        public const int CarMakeMaxLength = 15;

        public const int CarModeleMinLength = 1;
        public const int CarModelMaxLength = 50;

        public const int CarColorMinLength = 3;
        public const int CarColorMaxLength = 30;

        public const int ImageUrlMaxLength = 200;
		public const int ImageUrlMinLength = 20;

        public const int EngineTypeNameMaxLength = 20;
		public const int GearboxTypeNameMaxLength = 20;

        public const string PricePerDayMinimum = "1";
		public const string PricePerDayMaximum = "2000";

        public const int UserFirstNameMinLength = 2;
        public const int UserFirstNameMaxLength = 10;

        public const int UserLastNameMinLength = 2;
        public const int UserLastNameMaxLength = 10;
    }
}
