using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UsersChampionshipConfiguration : IEntityTypeConfiguration<UsersChampionship>
{
    public void Configure(EntityTypeBuilder<UsersChampionship> builder)
    {
        builder.Property(c => c.InviteCode).HasMaxLength(6);
        builder.HasIndex(c => c.InviteCode).IsUnique();

        builder.HasOne(c => c.CreatedByUser).WithMany()
            .HasForeignKey(c => c.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
    }
}