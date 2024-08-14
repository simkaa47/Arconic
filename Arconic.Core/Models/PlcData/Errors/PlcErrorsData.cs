namespace Arconic.Core.Models.PlcData.Errors;

public class PlcErrorsData
{
    public BsErrors BsErrors { get; } = new BsErrors();
    public BdErrors BdErrors { get; } = new BdErrors();
    public SafetyErrors SafetyErrors { get; } = new SafetyErrors();
    public DiagnBoardErrors DiagnBoardErrors { get; } = new DiagnBoardErrors();
    public DriveErrors DriveErrors { get; } = new DriveErrors();
    public CoolingErrors CoolingErrors { get; } = new CoolingErrors();
    public OtherErrors OtherErrors { get; } = new OtherErrors();
}