using Arconic.Core.Models.Trends;

namespace Arconic.Core.Abstractions.Trends;

public interface ITrendsService
{
    public Task<bool> StripExist(Strip? strip);
    public Task SaveStripAsync(Strip? strip);
    public Task AddStripAsync(Strip? strip);
    public void AddEdgesAndRecalculate(Scan scan, float startEdge, float endEdge, Strip parent);
    public List<TrendUserDto>? GetScansFromStrip(Strip source);
    public Task<Strip?> GetExtendedStrip(long stripId); 
    public Task<List<Strip>?> GetArchieveStrips(DateTime start, DateTime end);
    

}