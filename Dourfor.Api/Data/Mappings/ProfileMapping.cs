using Dourfor.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dourfor.Api.Data.Mappings;

public class ProfileMapping : IEntityTypeConfiguration<Profile>
{
    public void Configure(
        EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profile");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.UpdatedAt)
            .IsRequired(true)
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");
    }
}