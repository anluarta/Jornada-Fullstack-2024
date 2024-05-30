using Fina.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired(true)
                .HasColumnType("nvarchar")
                .HasMaxLength(80);

            builder.Property(t => t.Type)
                .IsRequired(true)
                .HasColumnType("smallint");

            builder.Property(t => t.Amount)
                .IsRequired(true)
                .HasColumnType("money");

            builder.Property(t => t.CreateAt)
                .IsRequired(true);

            builder.Property(t => t.PaidOrReceivedAt)
                .IsRequired(false);

            builder.Property(t => t.UserId)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(160);
        }
    }
}
