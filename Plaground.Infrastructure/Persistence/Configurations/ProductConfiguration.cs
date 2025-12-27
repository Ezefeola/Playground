using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Domain.Entities;

namespace Plaground.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Sku)
               .HasMaxLength(Product.Rules.SKU_MAX_LENGTH);

        builder.Property(x => x.Description)
               .HasMaxLength(Product.Rules.DESCRIPTION_MAX_LENGTH);

        builder.HasOne(x => x.CodeBar)
               .WithOne()
               .HasForeignKey<Product>(x => x.CodeBarId);
    }
}