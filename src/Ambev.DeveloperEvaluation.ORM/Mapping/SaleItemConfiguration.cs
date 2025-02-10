using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(saleItem => saleItem.Id);
        builder.Property(saleItem => saleItem.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(saleItem => saleItem.ProductId).IsRequired().HasColumnType("uuid");
        builder.Property(saleItem => saleItem.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(saleItem => saleItem.Quantity).IsRequired();
        builder.Property(saleItem => saleItem.DiscountedPrice).IsRequired();
        builder.Property(saleItem => saleItem.Price).IsRequired().HasColumnType("money");
        builder.Property(sale => sale.Created).IsRequired();
    }
}
