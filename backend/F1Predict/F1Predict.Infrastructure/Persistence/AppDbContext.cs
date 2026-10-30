using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Predict.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Title> Titles => Set<Title>();
    public DbSet<UsersChampionship> Championships => Set<UsersChampionship>();
    public DbSet<UserInChampionship> UserInChampionships => Set<UserInChampionship>();
    public DbSet<Constructor> Constructors => Set<Constructor>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<GrandPrix> GrandPrixes => Set<GrandPrix>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<SessionResult> SessionResults => Set<SessionResult>();
    public DbSet<Prediction> Predictions => Set<Prediction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // подхватывает все классы конфигураций из этой сборки
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}