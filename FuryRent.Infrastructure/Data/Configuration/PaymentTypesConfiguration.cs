using FuryRent.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuryRent.Infrastructure.Data.Configuration
{
    internal class PaymentTypesConfiguration : IEntityTypeConfiguration<PaymentTypes>
    {
        public void Configure(EntityTypeBuilder<PaymentTypes> builder)
        {
            builder.HasData(CreatePaymentTypes());
        }

        private List<PaymentTypes> CreatePaymentTypes()
        {
            var paymentTypes = new List<PaymentTypes>()
            {
                new PaymentTypes()
                {
                    Id = 1,
                    TypeName = "Cash"
                },
                new PaymentTypes()
                {
                    Id = 2,
                    TypeName = "Card"
                }
            };
            return paymentTypes;
        }

    }
}
