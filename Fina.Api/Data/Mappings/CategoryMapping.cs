using Fina.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired(true)
                .HasColumnType("nvarchar")
                .HasMaxLength(80);

            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(c => c.UserId)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(160);
        }
    }
}
