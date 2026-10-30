using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PredictionConfiguration : IEntityTypeConfiguration<Prediction>
{
    public void Configure(EntityTypeBuilder<Prediction> builder)
    {
        // один прогноз пользователя на одну сессию
        builder.HasIndex(p => new { p.UserId, p.SessionId }).IsUnique();

        builder.HasOne(p => p.User).WithMany(u => u.Predictions)
            .HasForeignKey(p => p.UserId);
        builder.HasOne(p => p.Session).WithMany()
            .HasForeignKey(p => p.SessionId);

        // три связи на Driver; Restrict запрещает удалять пилота, пока на него есть прогнозы
        builder.HasOne(p => p.DriverTopOne).WithMany()
            .HasForeignKey(p => p.DriverTopOneId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.DriverTopTwo).WithMany()
            .HasForeignKey(p => p.DriverTopTwoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.DriverTopThree).WithMany()
            .HasForeignKey(p => p.DriverTopThreeId).OnDelete(DeleteBehavior.Restrict);
    }
}