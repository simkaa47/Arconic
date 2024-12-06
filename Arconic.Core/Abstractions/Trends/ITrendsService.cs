using Arconic.Core.Models.Trends;

namespace Arconic.Core.Abstractions.Trends;

public interface ITrendsService
{
    public Task<bool> StripExist(Strip? strip);
    public Task AddPointToStrip(ThickPoint point, long stripId);
    public Task SaveStripAsync(Strip? strip);
    public Task AddStripAsync(Strip? strip);
    public void RecalculateScan(Scan scan, Strip parent);
    public List<ITrendUserDto>? GetScansFromStrip(Strip source);
    public Task<Strip?> GetExtendedStrip(long stripId); 
    public Task<List<Strip>?> GetArchieveStrips(DateTime start, DateTime end);
    

}