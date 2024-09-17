namespace Arconic.Core.Models.PlcData.SteelLabel;

public class SteelMagazineSettings
{
    public List<SteelMagazineItem> SteelItems { get; } =
        Enumerable.Range(0, 29)
            .Select(i => new SteelMagazineItem(33, i * 162, i))
            .ToList();
}