using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStore.Core.Domain;

namespace MyStore.Infrastructure.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).HasAggregateIdConversion();
            builder.Property(p => p.Username).IsRequired();
            builder.HasIndex(p => p.Username);
        }
    }
}