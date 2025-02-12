using Arconic.Core.Models.AccessControl;
using Arconic.Core.Models.Trends;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Infrastructure.DataContext.Data;

public class ArconicDbContextSeed
{
    public static void Seed(ArconicDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            SeedUsers(context);
            SeedTrendSettings(context);
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<ArconicDbContextSeed>();
            logger.LogError(e.Message);
        }
    }

    private static void SeedTrendSettings(ArconicDbContext context)
    {
        var setts = context.TrendSettings.FirstOrDefault();
        if (setts == null)
        {
            setts = new TrendSettings();
            context.TrendSettings.Add(setts);
            context.SaveChanges();
        }
        
    }

    private static void SeedUsers(ArconicDbContext context)
    {
        var testUser = context.Users.FirstOrDefault(u => u.Level == AccessLevel.Admin);
        if (testUser == null)
        {
            var users = new List<User>
            {
                new()
                {
                    FirstName = "Иван", 
                    LastName = "Жуковский", 
                    Password = "3xQwVVQqdMZVZqZf64Vx7A==:KkUUIafFL49ctAHmjbUPg5PJK5xWzZV6e1AeKephD8E=", 
                    Login = "admin",
                    Level = AccessLevel.Admin
                }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}