namespace FuryRent.Core.Models.Car
{
    public class DeleteCarViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
