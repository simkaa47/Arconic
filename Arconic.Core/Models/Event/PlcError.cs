namespace Arconic.Core.Models.Event;

public class PlcError : BaseEvent
{
    
    public const string SourceTag = "БЛОК ИСТОЧНИКА";
    public const string DetectorsTag = "БЛОК ДЕТЕКТИРОВАНИЯ";
    public const string SafetyTag = "СИСТЕМА БЕЗОПАСНОСТИ";
    public const string DbBoardsTag = "ДИАГНОСТИЧЕСКИЕ ПЛАТЫ";
    public const string DriveTag = "ПРИВОД";
    public const string OtherTag = "ПРОЧИЕ СИСТЕМЫ";
    public const string CoolingTag = "СИСТЕМА ОХЛАЖДЕНИЯ";
    
    public string Tag { get; private set; } = string.Empty;
    public void SetTag(string tag) => Tag = tag;

    public static readonly List<PlcError> Errors = [];

    public PlcError(string activeMessage,
        int dbNum,
        int byteNum,
        int bitNum) : base(activeMessage,
        dbNum,
        byteNum,
        bitNum, 
        EventType.PlcError)
    {
        Errors.Add(this);
    }
}