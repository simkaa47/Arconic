using System.Collections.ObjectModel;
using System.Diagnostics;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Models.PlcData.Drive;
using Arconic.Core.Models.Trends;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Trends;

public class TrendsService : ITrendsService
{
    private readonly ILogger<TrendsService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IRepository<TrendSettings> _trendSettingsRepository;

    public TrendsService(ILogger<TrendsService> logger, 
        IServiceScopeFactory scopeFactory, IRepository<TrendSettings> trendSettingsRepository)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _trendSettingsRepository = trendSettingsRepository;
        InitAsync();
    }

    private async void InitAsync()
    {
        Settings = await _trendSettingsRepository.GetFirstWhere(s => true) ?? new TrendSettings();
    }

    private const float _stain = 100;
    private const int _medianSize = 7;
    public TrendSettings Settings { get; private set; } = new TrendSettings();

    public async Task SaveTrendSettings()
    {
        try
        {
            await _trendSettingsRepository.UpdateAsync(Settings);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while saving trends settings.");
        }
    }

    public async Task<bool> StripExist(Strip? strip)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
        if (strip is null) return false;
        try
        {
            return (await dbContext.Strips.FindAsync(strip.Id)) != null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошибка при проверки наличия в базе данных полосы с ID = {strip.Id}");
            return false;
        }
    }

    public async Task AddPointToStrip(ThickPoint point, long stripId)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
        try
        {
            point.StripId = stripId;
            await dbContext.ThickPoints.AddAsync(point);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошибка при добавлении точки измерения в данные полосы с ID = {stripId}");
        }
    }

    public async Task AddScanToStrip(Scan scan, long stripId)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
        if(stripId<=0)return;
        try
        {
            scan.StripId = stripId;
            await dbContext.Scans.AddAsync(scan);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошибка при добавлении скана в данные полосы с ID = {stripId}");
        }
    }

    public async Task<List<ThickPoint>> GetAverageScan(Strip strip)
    {
        List<ThickPoint>? scan = null;
        await Task.Run(() =>
        {
            if (strip.Scans.Count > 0)
            {
                var leftBorder = (int)strip.Scans.
                    Where(s=>s.ThickPoints.Count>2).
                    Select(s=> Math.Min(s.ThickPoints.First().Position, s.ThickPoints.Last().Position))
                    .Average();
                var rightBorder = (int)strip.Scans.
                    Where(s=>s.ThickPoints.Count>2).
                    Select(s=> Math.Max(s.ThickPoints.First().Position, s.ThickPoints.Last().Position))
                    .Average();
                var dictionary = Enumerable.Range(leftBorder, rightBorder - leftBorder+1).ToDictionary(i=>i, i=>new List<float>());
                var points = strip.Scans
                    .SelectMany(s => s.ThickPoints)
                    .Where(p => p.Position >= leftBorder && p.Position <= rightBorder)
                    .ToList();

                foreach (var point in points)
                {
                    dictionary[(int)point.Position].Add(point.Thick);
                }
                var averPoints = dictionary.Where(p => p.Value.Count>0)
                    .Select(p=> new ThickPoint()
                    {
                        Position = p.Key,
                        Thick = p.Value.Average()
                    }).ToList();
                
                
                scan = SmoothScan(averPoints);

            }
        });
        return scan ?? new List<ThickPoint>();
    }

    public async Task SaveStripAsync(Strip? strip)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
        if (strip is null) return;
        try
        {
            dbContext.Entry(strip).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошибка при сохранении в базу данных данных полосы с ID = {strip.Id}");
        }
    }

    public async Task AddStripAsync(Strip? strip)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
        if (strip is null) return;
        try
        {
            await dbContext.Strips.AddAsync(strip);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошибка при добавлении в базу данных данных полосы с ID = {strip.Id}");
        }
    }

    public List<ITrendUserDto>? GetScansFromStrip(Strip source)
    {
        using var scope = _scopeFactory.CreateScope();
        var dto = scope.ServiceProvider.GetRequiredService<ITrendUserDto>();
        dto.ReInit(Settings,mode:source.MeasMode, 
            centralLine:source.CentralLinePosition,
            expectedThick:source.ExpectedThick, 
            expectedWidth:source.ExpectedWidth);
        
        
        if (dto.Mode == MeasModes.CentralLine)
        {
            dto.SetTimeCurve(source.ThickPoints);
            return [dto];
        }
        else
        {
            ITrendUserDto? scanInfo = null;
            return source.Scans.Where(s=>s.ThickPoints.Count>0)
                .Where(s=> (scanInfo = scope.ServiceProvider.GetService<ITrendUserDto>()) is not null)
                .Select(s =>
                {
                    //s.ThickPoints = SmoothScan(s.ThickPoints);
                scanInfo!.ReInit(Settings, mode:source.MeasMode, 
                    expectedThick:source.ExpectedThick, 
                    scanNumber:s.ScanNumber,
                    centralLine:source.CentralLinePosition,
                    expectedWidth:source.ExpectedWidth, 
                    leftBorder:source.CentralLinePosition - source.ExpectedWidth / 2,
                    rightBorder:source.CentralLinePosition + source.ExpectedWidth / 2, isArchieve:true);
                var left = s.ThickPoints.Min(tp => tp.Position);
                var right = s.ThickPoints.Max(tp => tp.Position);
                
               
                scanInfo!.Recalculate(stripDeviation:(right + left) / 2 - source.CentralLinePosition,
                    maxThick:s.ThickPoints.Max(tp => tp.Thick),
                    minThick:s.ThickPoints.Min(tp => tp.Thick),
                    actualWidth:right - left,
                    klin: s.Klin,
                    chechevitsa: s.Chechewitsa);
               scanInfo.SetActualScan(s.ThickPoints.ToList());
               //scanInfo.SetPreviousScan(source.AverageScan);
                return scanInfo;
            }).ToList();
        }
            
    }

    public async Task<Strip?> GetExtendedStrip(long stripId)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
        try
        {
            var strip =  await dbContext.Strips
                .Where(s => s.Id == stripId)
                .Include(s => s.Scans)
                .ThenInclude(s => s.ThickPoints)
                .Include(s => s.ThickPoints)
                .SingleOrDefaultAsync();
            Console.WriteLine($"{DateTime.Now} end downloading from db");
            if(strip  is not null)
                RecalculateStrip(strip);
            return strip;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошибка при загрузке из базы данных данных полосы с ID = {stripId}");
        }
        return null;
    }

    public async Task<List<Strip>?> GetArchieveStrips(DateTime start, DateTime end, string? stripNumber)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ArconicDbContext>();
        try
        {
            return await dbContext.Strips
                .Where(s => s.StartTime >= start && s.StartTime <= end)
                .Where(s=> s.MeasMode == MeasModes.CentralLine ? s.ThickPoints.Count>0 : s.Scans.Count>0)
                .Where(s=>string.IsNullOrEmpty(stripNumber) || s.StripNumber.Contains(stripNumber))
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошибка при загрузке в базу данных архивных трендов");
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
        else if(strip.Scans.Count>0)
        {
            strip.Length = strip.Scans
                .LastOrDefault(s => s.ThickPoints.Count > 0)?
                .ThickPoints.LastOrDefault()?.Lendth ?? 0;
            strip.AverageThick = strip.Scans.SelectMany(s => s.ThickPoints)
                .Where(tp => tp.Thick > 0)
                .Average(tp => tp.Thick);
            strip.AverageWidth = strip.Scans
                .Select(s => s.Width)
                .Average();
            strip.AverageChehevitsa = strip.Scans
                .Select(s => s.Chechewitsa)
                .Average();
            strip.AverageKlin = strip.Scans
                .Select(s => s.Klin)
                .Average();
        }
    }
    
    private float GetMedFromArr(List<ThickPoint> points, int index)
    {
        var startIndex = 0;
        var take = 0;
        // if (dir)
        // {
        //     startIndex = Math.Min(0, index - _medianSize / 2);
        //     take = Math.Min(_medianSize, points.Count - startIndex);
        // }
        // else
        // {
        //     var endIndex = Math.Min(points.Count-1, index + _medianSize / 2);
        // }
        
        if((index - _medianSize/2)<0)
            startIndex = 0;
        else if((index+ _medianSize/2) > (points.Count-1))
            startIndex = points.Count - _medianSize;
        else
        {
            startIndex = index - _medianSize/2;
        }
        take = Math.Min(_medianSize, points.Count - startIndex);
        
        var orderedPoints = points.Skip(startIndex)
            .Take(take)
            .Select(p=>p.Thick)
            .OrderBy(th=>th)
            .ToList();
        
        return orderedPoints[orderedPoints.Count/2];
    }
    
    private List<ThickPoint> SmoothScan(List<ThickPoint> points)
    {
        var medScan =  Enumerable.Range(0, points.Count)
            .Select(i => new ThickPoint() 
            { Position = points[i].Position, 
                Thick = GetMedFromArr(points, i) 
            })
            .ToList();
        
        return Enumerable.Range(0, points.Count)
            .Select(i => new ThickPoint() 
            { Position = points[i].Position, 
                Thick = medScan.Where(p=>p.Position >= medScan[i].Position - _stain && p.Position <= points[i].Position + _stain)
                    .Select(p=> p.Thick)
                    .Average()
            })
            .ToList();
    }
    
}