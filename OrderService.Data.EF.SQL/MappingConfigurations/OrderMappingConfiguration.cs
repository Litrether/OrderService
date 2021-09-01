using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Data.Domain.Models;

namespace OrderService.Data.EF.SQL.MappingConfigurations
{
    public class OrderMappingConfiguration
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property("DeliveryCompany");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.HasOne(c => c.DeliveryCompany).WithOne();
        }
    }
}
