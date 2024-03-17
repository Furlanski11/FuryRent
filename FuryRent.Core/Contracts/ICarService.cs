using FuryRent.Core.Models.Car;
using FuryRent.Infrastructure.Data.Models;

namespace FuryRent.Core.Contracts
{
    public interface ICarService
    {
        public Task<IEnumerable<AllCarsQueryModel>> All(string? make, string? criteria);
        public Task Delete(int id);
        public Task<IEnumerable<AllCarsQueryModel>> GetByMake(string make);
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
