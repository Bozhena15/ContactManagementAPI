using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManagementAPI.Entities.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .HasMaxLength(50);

        builder.Property(c => c.Phone)
            .HasMaxLength(10);

        builder.Property(c => c.CreateTime)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
