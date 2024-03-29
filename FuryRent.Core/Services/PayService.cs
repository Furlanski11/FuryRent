using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Pay;
using FuryRent.Infrastructure.Data;
using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FuryRent.Core.Services
{
	public class PayService : IPayService
	{
		private readonly FuryRentDbContext db;

        public PayService(FuryRentDbContext _db)
        {
            db = _db;
        }

        public async Task Pay(PayServiceViewModel model)
        {
            var rent = await db.Rents
                .Where(r => r.RentId == model.RentId)
                .FirstOrDefaultAsync();

            if(rent == null)
            {
                throw new Exception("Invalid RentId");
            }

            Payment payment = new Payment()
            {
                
                RentId = model.RentId,
                PaymentAmount = model.PaymentAmount,
                PaymentDate = DateTime.Now,
                PaymentTypeId = model.PaymentTypeId,
            };

            await db.Payments.AddAsync(payment);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentTypeViewModel>> GetPaymentTypes()
        {
            return await db.PaymentTypes
                .AsNoTracking()
                .Select(t => new PaymentTypeViewModel
                {
                    Id = t.Id,
                    Name = t.TypeName,
                }).ToListAsync();
        }

        public async Task<PayServiceViewModel> GetRentById(int id)
        {
            return await db.Rents
                .AsNoTracking()
                .Where(r => r.RentId == id)
                .Select(r => new PayServiceViewModel()
                {
                    RentId = r.RentId,
                    PaymentAmount = r.TotalCost,
                })
                .FirstAsync();
        }
    }
}
