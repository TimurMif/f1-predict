using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GrandPrixConfiguration : IEntityTypeConfiguration<GrandPrix>
{
    public void Configure(EntityTypeBuilder<GrandPrix> builder)
    {
        builder.HasIndex(g => new { g.Year, g.RoundNumber }).IsUnique();
    }
}