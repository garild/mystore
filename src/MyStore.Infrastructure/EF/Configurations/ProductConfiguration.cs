using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStore.Core.Domain;

namespace MyStore.Infrastructure.EF.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).HasAggregateIdConversion();
            builder.Property(p => p.CategoryId).HasAggregateIdConversion();
            builder.HasOne<Category>().WithMany().HasForeignKey(p => p.CategoryId);
        }
    }
}