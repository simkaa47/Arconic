using System.Runtime.InteropServices.JavaScript;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Abstractions.Trends;
using Arconic.Core.Models.Trends;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Trends;

public class TrendsService(IRepository<Strip> stripRepository, 
    ILogger<TrendsService> logger) : ITrendsService
{
    public async Task<bool> StripExist(Strip? strip)
    {
        if (strip is null) return false;
        try
        {
            return (await stripRepository.GetByIdAsync(strip.Id)) != null;
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при проверки наличия в базе данных полосы с ID = {strip.Id}");
            return false;
        }
    }

    public async Task SaveStripAsync(Strip? strip)
    {
        if (strip is null) return;
        try
        {
            await stripRepository.UpdateAsync(strip);
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
            strip.Scans = strip.Scans
                .Where(s=>s.ThickPoints.Count>0)
                .ToList();
            strip.EndTime = DateTime.Now;
            await stripRepository.AddAsync(strip);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при добавлении в базу данных данных полосы с ID = {strip.Id}");
        }
    }

    public void  AddEdgesAndRecalculate(Scan scan, float leftEdge, float rightEdge, Strip parent)
    {
        if (scan.ThickPoints.Count > 0)
        {
            var minPos = scan.ThickPoints.MinBy(p=>p.Position)!;
            var maxPos = scan.ThickPoints.MaxBy(p=>p.Position)!;;
            var leftPoint = new ThickPoint()
            {
                Lendth = minPos.Lendth,
                Thick = minPos.Thick,
                Position = minPos.Position
            };
            var rightPoint = new ThickPoint()
            {
                Lendth = maxPos.Lendth,
                Thick = maxPos.Position,
                Position = maxPos.Position
            };
            scan.ThickPoints.AddRange([leftPoint, rightPoint]);
            scan.OrderPoints();
            scan.Width = Math.Abs(rightPoint.Position - leftPoint.Position);
            scan.CentralLineDeviation = (rightEdge + leftEdge) / 2 - parent.CentralLinePosition;
            var preLastIndex = scan.ThickPoints.Count - 2;
            var centralIndex = scan.ThickPoints.Count / 2;
            scan.Klin = scan.ThickPoints[1].Thick - scan.ThickPoints[preLastIndex].Thick;
            scan.Chechewitsa = scan.ThickPoints[centralIndex].Thick -
                               (scan.ThickPoints[1].Thick + scan.ThickPoints[preLastIndex].Thick) / 2;

        }
    }
}