using Arconic.Core.Models.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using S7.Net;

namespace Arconic.Core.Models.Event;

public  abstract partial class BaseEvent : ObservableObject
{
    [ObservableProperty]
    private bool _isActive;
    protected BaseEvent(string activeMessage,
        int dbNum,
        int byteNum,
        int bitNum, 
        EventType eventType)
    {
        ActiveMessage = activeMessage;
        EventType = eventType;
        Parameter = new(activeMessage, false, true, DataType.DataBlock, dbNum, byteNum, bitNum)
            { IsReadOnly = true };
        Parameter.PropertyChanged += (s, args) =>
        {
            if (args.PropertyName == "Value")
            {
                IsActive = Parameter.Value;
                _lastExecTime = DateTime.Now;
            }
        };
    }

    public string ActiveMessage { get;}
    [ObservableProperty]
    private DateTime _lastExecTime;
    public string Code { get; private set; } = string.Empty;
    public EventType EventType { get; }

    private Parameter<bool> Parameter { get; }
    public void SetCode(string code) => Code = code;
}