using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasOne(s => s.GrandPrix).WithMany(g => g.Sessions)
            .HasForeignKey(s => s.GrandPrixId);

        // в базе enum читается лучше строкой, чем числом
        builder.Property(s => s.Type).HasConversion<string>().HasMaxLength(32);
        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(32);
    }
}