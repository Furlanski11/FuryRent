using FuryRent.Core.Models.Car;

namespace FuryRent.Core.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<AllCarsQueryModel>> All();

        Task<DetailsViewModel> Details(int id);

        public Task<IEnumerable<CategoryViewModel>> GetCategories();

        public Task<IEnumerable<EngineTypeViewModel>> GetEngineTypes();

        public Task<IEnumerable<GearboxTypeViewModel>> GetGearboxTypes();
    }
}
