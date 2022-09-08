using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<Technology> Technologies { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        => (Configuration) = (configuration);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //    base.OnConfiguring(
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>(p =>
        {
            p.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
            p.Property(p => p.Id).HasColumnName("Id");
            p.Property(p => p.Name).HasColumnName("Name");
            p.HasMany(p => p.Technologies);
        });

        modelBuilder.Entity<Technology>(p =>
        {
            p.ToTable("Technologies").HasKey(k => k.Id);
            p.Property(p => p.Id).HasColumnName("Id");
            p.Property(p => p.Name).HasColumnName("Name");
            p.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
            p.HasOne(p => p.ProgrammingLanguage);
        });

        ProgrammingLanguage[] programmingLanguagesSeeds = { new(1, "C#"), new(2, "Java"), new(3, "Dart") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesSeeds);

        Technology[] technologies = { new(1, 1, "ASP.NET CORE"), new(2, 1, ".NET MAUI"), new(3, 3, "Flutter")};
        modelBuilder.Entity<Technology>().HasData(technologies);
    }
}
