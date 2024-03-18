﻿namespace FuryRent.Core
{
	public static class CarConstants
	{
		public const string DateFormat = "dd/MM/yyyy";

		public const string RequireErrorMessage = "The field {0} is required";

		public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1} characters long";

		public const int IsVipOnlyMinLength = 2;
        public const int IsVipOnlyMaxLength = 3;
        public const string IsVipOnlyErrorMessage = "The field {0} must be 'Yes' or 'No'";
    }
}
