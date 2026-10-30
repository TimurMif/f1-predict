using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserInChampionshipConfiguration : IEntityTypeConfiguration<UserInChampionship>
{
    public void Configure(EntityTypeBuilder<UserInChampionship> builder)
    {
        // один пользователь = не больше одного чемпионата (WithOne сам делает UserId уникальным)
        builder.HasOne(x => x.User).WithOne(u => u.UserInChampionship)
            .HasForeignKey<UserInChampionship>(x => x.UserId);

        builder.HasOne(x => x.Championship).WithMany(c => c.Users)
            .HasForeignKey(x => x.ChampionshipId);
    }
}