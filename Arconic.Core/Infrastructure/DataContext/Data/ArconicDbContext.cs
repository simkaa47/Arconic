using System.Reflection;
using Arconic.Core.Models.AccessControl;
using Arconic.Core.Models.Event;
using Arconic.Core.Models.Trends;
using Microsoft.EntityFrameworkCore;

namespace Arconic.Core.Infrastructure.DataContext.Data;

public class ArconicDbContext(DbContextOptions<ArconicDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Scan>()
            .HasOne(s => s.Strip)
            .WithMany(s => s.Scans)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<ThickPoint>()
            .HasOne(tp => tp.Strip)
            .WithMany(s => s.ThickPoints)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<ThickPoint>()
            .HasOne(tp => tp.Scan)
            .WithMany(s => s.ThickPoints)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    
    public DbSet<TrendSettings> TrendSettings => Set<TrendSettings>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Strip> Strips => Set<Strip>();
    public DbSet<Scan> Scans => Set<Scan>();
    public DbSet<ThickPoint> ThickPoints => Set<ThickPoint>();
    public DbSet<EventHistoryItem> EventHistoryItems => Set<EventHistoryItem>();
}

//dotnet ef migrations add Initial -p .\Arconic.Core\ -s .\Arconic.View\ -c ArconicDbContext -o Infrastructure/DataContext/Data/Migrations
//dotnet ef migrations add Initial -p .\Arconic.Core\  -c ArconicDbContext -o Infrastructure/DataContext/Data/Migrations