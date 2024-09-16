using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.SteelLabel;

public class SteelMagazineIndication
{
    public SteelMagazineItem Steel { get; } = new SteelMagazineItem(2, 8200);
    public Parameter<short> Index { get; } 
        = new Parameter<short>("", 0, 30, DataType.DataBlock, 2, 8362);

    public Parameter<bool> PrepareToAddCommand { get; } =
        new Parameter<bool>("", false, true, DataType.DataBlock, 2, 8363, 0);
    public Parameter<bool> AddCommand { get; } =
        new Parameter<bool>("", false, true, DataType.DataBlock, 2, 8363, 1);
    public Parameter<bool> EditCommand { get; } =
        new Parameter<bool>("", false, true, DataType.DataBlock, 2, 8363, 2);
    public Parameter<bool> DeleteCommand { get; } =
        new Parameter<bool>("", false, true, DataType.DataBlock, 2, 8363, 3);
    public Parameter<bool> SetDim { get; } =
        new Parameter<bool>("", false, true, DataType.DataBlock, 2, 8363, 4);
    public Parameter<bool> SetSteelCoeffCommand { get; } =
        new Parameter<bool>("", false, true, DataType.DataBlock, 2, 8363, 5);
    public Parameter<bool> RecalcSteelCoeffCommand { get; } =
        new Parameter<bool>("", false, true, DataType.DataBlock, 2, 8363, 6);

}