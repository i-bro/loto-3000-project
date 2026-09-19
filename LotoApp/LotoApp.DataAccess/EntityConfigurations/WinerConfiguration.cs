using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace LotoApp.DataAccess.EntityConfigurations
{
    public class WinerConfiguration : IEntityTypeConfiguration<Winner>
    {
        public void Configure(EntityTypeBuilder<Winner> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.PlayerFullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.PrizeWon)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(w => w.TicketNumbers)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions?)null) ?? new List<int>()
                )
                .IsRequired();

            builder.HasOne(w => w.Ticket)
                .WithMany()
                .HasForeignKey(w => w.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.Draw)
                .WithMany()
                .HasForeignKey(w => w.DrawId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
