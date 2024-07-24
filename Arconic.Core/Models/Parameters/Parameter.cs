using System.Timers;
using Arconic.Core.Infrastructure.Attributes;
using S7.Net;

namespace Arconic.Core.Models.Parameters;

public  class Parameter<T>:ParameterBase where T:IComparable
{
    public Parameter(string description, T minValue, T maxValue, DataType memoryType, int dbNum,
        int byteNum, int bitNum)
    {
        Description = description;
        MinValue = minValue;
        MaxValue = maxValue;
        MemoryType = memoryType;
        DbNum = dbNum;
        ByteNum = byteNum;
        BitNum = bitNum;
        _timer = new System.Timers.Timer(5000);
        _timer.Elapsed += OnTimerElapsed; 
        ParameterBase.Parameters.Add(this);
    }
    
    public T MinValue { get; init; }
    public T MaxValue { get; init; }
   
    
    #region Value
    private T? _value;
    public T? Value
    {
        get => _value;
        set
        {
            if (SetProperty(ref _value, value))
                WriteValue = value;
            IsWriting = false;
        }
    }
    #endregion   
    
    #region WriteValue
    T? _writeValue;

    [InRange(nameof(MinValue), nameof(MaxValue))]
    public T? WriteValue
    {
        get => _writeValue;
        set
        {
            if (value != null)
            {
                ValidateProperty(value, nameof(WriteValue));                   
                if (value.CompareTo(Value) != 0)
                {
                    RestartTimer();
                }
                SetProperty(ref _writeValue, value);
                ValidationOk = value.CompareTo(MinValue) >= 0 && value.CompareTo(MaxValue) <= 0;
            }
        }
    }
    #endregion     
    
    #region Таймер
    private readonly System.Timers.Timer _timer;

    void RestartTimer()
    {
        if (_timer.Enabled) _timer.Stop();
        _timer.Start();
    }

    void OnTimerElapsed(object? source, ElapsedEventArgs e)
    {
        _timer.Stop();
        WriteValue = Value;
    }


    #endregion

}