using System.Collections.ObjectModel;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.Trends;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Trends;

public class TrendsService(ArconicDbContext dbContext, 
    ILogger<TrendsService> logger, 
    IServiceProvider serviceProvider) : ITrendsService
{
    

    public async Task<bool> StripExist(Strip? strip)
    {
        if (strip is null) return false;
        try
        {
            return (await dbContext.Strips.FindAsync(strip.Id)) != null;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при проверки наличия в базе данных полосы с ID = {strip.Id}");
            return false;
        }
    }

    public async Task AddPointToStrip(ThickPoint point, long stripId)
    {
        try
        {
            point.StripId = stripId;
            await dbContext.ThickPoints.AddAsync(point);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при добавлении точки измерения в данные полосы с ID = {stripId}");
        }
    }

    public async Task SaveStripAsync(Strip? strip)
    {
        if (strip is null) return;
        try
        {
            dbContext.Entry(strip).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при сохранении в базу данных данных полосы с ID = {strip.Id}");
        }
    }

    public async Task AddStripAsync(Strip? strip)
    {
        if (strip is null) return;
        try
        {
            await dbContext.Strips.AddAsync(strip);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при добавлении в базу данных данных полосы с ID = {strip.Id}");
        }
    }

    public void  RecalculateScan(Scan scan,  Strip parent)
    {
        if (scan.ThickPoints.Count > 0)
        {
            var minPos = scan.ThickPoints.MinBy(p=>p.Position)!;
            var maxPos = scan.ThickPoints.MaxBy(p=>p.Position)!;
            
            scan.Width = Math.Abs(maxPos.Position - minPos.Position);
            scan.CentralLineDeviation = (maxPos.Position + minPos.Position) / 2 - parent.CentralLinePosition;
            var centralIndex = scan.ThickPoints.Count / 2;
            scan.Klin = minPos.Thick - maxPos.Thick;
            scan.Chechewitsa = scan.ThickPoints[centralIndex].Thick -
                               (minPos.Thick + maxPos.Thick) / 2;

        }
    }

    public List<ITrendUserDto>? GetScansFromStrip(Strip source)
    {
        var dto = serviceProvider.GetService<ITrendUserDto>();
        if(dto is null) return null;
        dto.ReInit(mode:source.MeasMode, 
            expectedThick:source.ExpectedThick, 
            expectedWidth:source.ExpectedWidth, 
            leftBorder:source.CentralLinePosition - source.ExpectedWidth / 2,
            rightBorder:source.CentralLinePosition + source.ExpectedWidth / 2);
        
        
        if (dto.Mode == MeasModes.CentralLine)
        {
            dto.SetTimeCurve(source.ThickPoints);
            return [dto];
        }
        else
        {
            ITrendUserDto? scanInfo = null;
            return source.Scans.Where(s=>s.ThickPoints.Count>0)
                .Where(s=> (scanInfo = serviceProvider.GetService<ITrendUserDto>()) is not null)
                .Select(s =>
            {
                scanInfo!.ReInit(mode:source.MeasMode, 
                    expectedThick:source.ExpectedThick, 
                    expectedWidth:source.ExpectedWidth, 
                    leftBorder:source.CentralLinePosition - source.ExpectedWidth / 2,
                    rightBorder:source.CentralLinePosition + source.ExpectedWidth / 2);
                var left = s.ThickPoints.Min(tp => tp.Position);
                var right = s.ThickPoints.Max(tp => tp.Position);
                var klin = 0.0f;
                var chechevitsa = 0.0f;
                if (s.ThickPoints.Count > 1)
                {
                    var preLastIndex = s.ThickPoints.Count - 2;
                    var centralIndex = s.ThickPoints.Count / 2;
                    klin = s.ThickPoints[1].Thick - s.ThickPoints[preLastIndex].Thick;
                    chechevitsa = s.ThickPoints[centralIndex].Thick -
                                  (s.ThickPoints[1].Thick + s.ThickPoints[preLastIndex].Thick) / 2;
                }
                scanInfo!.Recalculate(stripDeviation:(right + left) / 2 - source.CentralLinePosition,
                    maxThick:s.ThickPoints.Where(tp=>tp.Thick>0).Max(tp => tp.Thick),
                    minThick:s.ThickPoints.Where(tp=>tp.Thick>0).Min(tp => tp.Thick),
                    actualWidth:right - left,
                    klin: klin,
                    chechevitsa: chechevitsa);
               scanInfo.SetActualScan(s.ThickPoints);
               
                return scanInfo;
            }).ToList();
        }
            
    }

    public async Task<Strip?> GetExtendedStrip(long stripId)
    {
        try
        {
            var strip =  await dbContext.Strips
                .Where(s => s.Id == stripId)
                .Include(s => s.Scans)
                .ThenInclude(s => s.ThickPoints)
                .Include(s => s.ThickPoints)
                .SingleOrDefaultAsync();
            if(strip  is not null)
                RecalculateStrip(strip);
            return strip;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при хагрузке из базы данных данных полосы с ID = {stripId}");
        }
        return null;
    }

    public async Task<List<Strip>?> GetArchieveStrips(DateTime start, DateTime end)
    {
        try
        {
            return await dbContext.Strips
                .Where(s => s.StartTime >= start && s.StartTime <= end)
                .ToListAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при загрузке в базу данных архивных трендов");
        }

        return null;
    }

    private void RecalculateStrip(Strip strip)
    {
        if (strip.MeasMode == MeasModes.CentralLine)
        {
            strip.Length = strip.ThickPoints.LastOrDefault()?.Lendth ?? 0;
            strip.AverageThick = strip.ThickPoints
                .Where(tp=>tp.Thick>0)
                .Average(tp => tp.Thick);
        }
        else
        {
            strip.Length = strip.Scans
                .LastOrDefault(s => s.ThickPoints.Count > 0)?
                .ThickPoints.LastOrDefault()?.Lendth ?? 0;

            strip.AverageThick = strip.Scans.SelectMany(s => s.ThickPoints)
                .Where(tp => tp.Thick > 0)
                .Average(tp => tp.Thick);
            strip.AverageWidth = strip.Scans
                .Where(s => s.ThickPoints.Count > 2)
                .Select(s => Math.Abs(s.ThickPoints.Last().Position - s.ThickPoints[0].Position))
                .Average();
            strip.AverageChehevitsa = strip.Scans
                .Where(s => s.ThickPoints.Count > 2)
                .Select(s => Math.Abs(s.ThickPoints.Last().Position - s.ThickPoints[0].Position))
                .Average();
            strip.AverageScan = GetAverageScan(strip);
            if (strip.AverageScan.Count > 3)
            {
                var centralIndex = strip.AverageScan.Count / 2;
                var preLastIndex = strip.AverageScan.Count - 2;
                strip.AverageKlin = strip.AverageScan[1].Thick - strip.AverageScan[preLastIndex].Thick;
                strip.AverageChehevitsa = strip.AverageScan[centralIndex].Thick -
                                           (strip.AverageScan[1].Thick + (strip.AverageScan[preLastIndex].Thick)) / 2;
                strip.AverageCentralLineDeviation = (strip.AverageScan[1].Position + strip.AverageScan[preLastIndex].Position) / 2 - strip.CentralLinePosition;
                
            }
            
            
        }
    }


    private List<ThickPoint> GetAverageScan(Strip strip)
    {
        if (strip.MeasMode != MeasModes.CentralLine 
            && strip.Scans.Any(s => s.ThickPoints.Count>2))
        {
            var leftPos = strip.Scans
                .Where(s=>s.ThickPoints.Count>2)
                .Select(s => s.ThickPoints.Min(tp => tp.Position))
                .Average();
            var rightPos = strip.Scans
                .Where(s=>s.ThickPoints.Count>2)
                .Select(s => s.ThickPoints.Max(tp => tp.Position))
                .Average();
            var allPoints = strip.Scans
                .Where(s => s.ThickPoints.Count > 2)
                .SelectMany(s => s.ThickPoints)
                .Where(th => th.Thick > 0)
                .OrderBy(th=>th.Position).ToList();
            var scan = GetAverage(allPoints).ToList();
            scan.Insert(0, new ThickPoint(){Position = scan[0].Position, Thick = 0});
            scan.Add(new ThickPoint(){Position = scan.Last().Position, Thick = 0});
            return scan;
        }

        IEnumerable<ThickPoint> GetAverage(List<ThickPoint> allPoints)
        {
            foreach (var point in allPoints)
            {
                var averageThick = allPoints
                    .Where(p => p.Position >= point.Position - 5 && p.Position <= point.Position + 5)
                    .Average(tp => tp.Thick);
                yield return new ThickPoint() { Position = point.Position, Thick = averageThick };
            }
        }
        
        
        return new List<ThickPoint>();
    }
    
    
    
    
}