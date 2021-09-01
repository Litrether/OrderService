using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Data.Domain.Models;

namespace OrderService.Data.EF.SQL.MappingConfigurations
{
    public class DeliveryCompanyMappingConfigurations : IEntityTypeConfiguration<DeliveryCompany>
    {
        public void Configure(EntityTypeBuilder<DeliveryCompany> builder)
        {
            builder.Property("DeliveryCompany");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Rating).IsRequired();
        }
    }
}
