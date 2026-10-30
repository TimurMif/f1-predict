using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SessionResultConfiguration : IEntityTypeConfiguration<SessionResult>
{
    public void Configure(EntityTypeBuilder<SessionResult> builder)
    {
        builder.HasOne(r => r.Session).WithMany(s => s.Results)
            .HasForeignKey(r => r.SessionId);
        builder.HasOne(r => r.Driver).WithMany()
            .HasForeignKey(r => r.DriverId).OnDelete(DeleteBehavior.Restrict);

        // пилот не может быть в таблице сессии дважды, и позиция тоже уникальна
        builder.HasIndex(r => new { r.SessionId, r.DriverId }).IsUnique();
        builder.HasIndex(r => new { r.SessionId, r.Position }).IsUnique();
    }
}