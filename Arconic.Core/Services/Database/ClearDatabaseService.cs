using System.Text.Json.Nodes;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Arconic.Core.Services.Database;

public class ClearDatabaseService(ArconicDbContext dbContext,
    ILogger<ClearDatabaseService> logger, IOptionsMonitor<DbClearOption> option) : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Today;
            var measuresLastDt = now.AddDays(option.CurrentValue.Measures * (-1));
            var historyLastDt = now.AddDays(option.CurrentValue.EventHistory * (-1));
            logger.LogInformation("Вызов сервиса очистки архивных данных");
            try
            {
                logger.LogInformation("Удаление данных измерений ранее, чем {time}", measuresLastDt);
                var result = await dbContext.Strips
                    .Where(s => s.StartTime < measuresLastDt)
                    .ExecuteDeleteAsync(cancellationToken: stoppingToken);
                logger.LogInformation("{num} данных измерений было удалено", result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Ошибка удаления данных измерений");
            }
            
            try
            {
                logger.LogInformation("Удаление данных истории событий ранее, чем {time}", historyLastDt);
                var result = await dbContext.EventHistoryItems
                    .Where(s => s.Date < historyLastDt)
                    .ExecuteDeleteAsync(cancellationToken: stoppingToken);
                logger.LogInformation("{num} записей истории событий было удалено", result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Ошибка удаления данных истории событий");
            }
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            
        }
        await Task.CompletedTask;
    }
}