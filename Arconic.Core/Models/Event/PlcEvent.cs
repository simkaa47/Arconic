namespace Arconic.Core.Models.Event;

public class PlcEvent : BaseEvent
{
    public static List<PlcEvent> Events { get; } = new List<PlcEvent>();
    public PlcEvent(string activeMessage,
        int dbNum,
        int byteNum,
        int bitNum):base(activeMessage, dbNum, byteNum, bitNum, EventType.PlcEvent)
    {
        Events.Add(this);
    }
}