using FuryRent.Core.Models.Car;
using FuryRent.Infrastructure.Data.Models;
using FuryRent.Infrastructure.Enumerators.Car;

namespace FuryRent.Core.Contracts
{
    public interface ICarService
    {
        public CarQueryServiceModel All(string? make,
            CarSorting sorting = CarSorting.Make,
            int currentPage = 1, int carsPerPage = 3);
       
        public Task Delete(int id);
       
        public Task<DetailsViewModel> Details(int id);

        public Task<IEnumerable<CategoryViewModel>> GetCategories();

        public Task<IEnumerable<EngineTypeViewModel>> GetEngineTypes();

        public Task<IEnumerable<GearboxTypeViewModel>> GetGearboxTypes();

        public Task Add(AddCarViewModel carModel);

        public Task<EditCarFormModel> Edit(int id);

        public Task Edit(EditCarFormModel formModel, int id);

        public Task<Car?> GetById(int? id);
    }
}
