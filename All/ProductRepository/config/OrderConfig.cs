using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductData.Entites.orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.config
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.shippingAddress, o => o.WithOwner());
            builder.HasOne(o => o.DeliveryMethod).WithOne().OnDelete(DeleteBehavior.NoAction);

            builder.Property(o => o.status)
                   .HasConversion(o => o.ToString(), o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
            builder.HasOne(e => e.DeliveryMethod)
                .WithOne();
            builder.Property(e => e.SubTotal)
                    .HasColumnType("decimal(18,2)");
;        }
    }
}
