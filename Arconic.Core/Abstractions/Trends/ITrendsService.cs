using Arconic.Core.Models.Trends;

namespace Arconic.Core.Abstractions.Trends;

public interface ITrendsService
{
    public Task<bool> StripExist(Strip? strip);
    public Task SaveStripAsync(Strip? strip);
    public Task AddStripAsync(Strip? strip);
    public void AddEdgesAndRecalculate(Scan scan, float leftEdge, float rightEdge, Strip parent);

}