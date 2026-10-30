using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(100);
        builder.Property(u => u.LastName).HasMaxLength(100);

        // в PostgreSQL уникальный индекс допускает несколько NULL, так что это безопасно
        builder.HasIndex(u => u.VkId).IsUnique();
        builder.HasIndex(u => u.TelegramId).IsUnique();

        builder.HasMany(u => u.Titles).WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}