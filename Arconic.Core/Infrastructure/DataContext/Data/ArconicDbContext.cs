using Arconic.Core.Models.AccessControl;
using Arconic.Core.Models.Event;
using Microsoft.EntityFrameworkCore;

namespace Arconic.Core.Infrastructure.DataContext.Data;

public class ArconicDbContext(DbContextOptions<ArconicDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<EventHistoryItem> EventHistoryItems => Set<EventHistoryItem>();
}

//dotnet ef migrations add Initial -p .\Arconic.Core\ -s .\Arconic.View\ -c ArconicDbContext -o Infrastructure/DataContext/Data/Migrations