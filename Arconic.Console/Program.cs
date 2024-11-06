using Arconic.Core;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.Security;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Infrastructure.Security;
using Arconic.Core.Models.Trends;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;

var services = new ServiceCollection()
    .AddTransient<IPasswordHasher, PasswordHasher>();
var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
 
IConfiguration config = builder.Build();

services.AddCoreServices(config);
    
using var serviceProvider = services.BuildServiceProvider();

var hasher = serviceProvider.GetRequiredService<IPasswordHasher>();
Console.WriteLine(hasher.GetHash("konvels2024"));
try
{
    var context = serviceProvider.GetRequiredService<ArconicDbContext>();
    context.Database.Migrate();
}
catch (Exception ex)
{
    Console.WriteLine(ex);                  
}

var repo = serviceProvider.GetRequiredService<IRepository<Strip>>();
try
{
    var strip = new Strip();
    var scan = new Scan();
    await repo.AddAsync(strip);
    strip.Scans.Add(scan);
    await repo.UpdateAsync(strip);
    scan.ThickPoints.Add(new ThickPoint());
    await repo.UpdateAsync(strip);
    strip.ThickPoints.Add((new ThickPoint()));
    await repo.UpdateAsync(strip);

}
catch (Exception e)
{
    Console.WriteLine(e);
}