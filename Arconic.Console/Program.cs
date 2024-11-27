using Arconic.Console.WriteAbsorbtions;
using Arconic.Core;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.Security;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Infrastructure.Security;
using Arconic.Core.Models.Trends;
using Arconic.Core.Services.Plc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection()
    .AddLogging((logging) =>
    {
        
    })
    .AddTransient<WriteAbsorbtionService>()
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

// Write Absorbtion Service
var absService = serviceProvider.GetRequiredService<WriteAbsorbtionService>();

//await absService.WriteElementInfo("Al",13, 0, 2.71f);

