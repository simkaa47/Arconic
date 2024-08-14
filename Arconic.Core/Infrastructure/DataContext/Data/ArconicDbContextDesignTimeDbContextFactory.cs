using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Arconic.Core.Infrastructure.DataContext.Data;

public class ArconicDbContextDesignTimeDbContextFactory:IDesignTimeDbContextFactory<ArconicDbContext>
{
    public ArconicDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ArconicDbContext>();
        builder.UseSqlite();
        return new ArconicDbContext(builder.Options);
    }
}