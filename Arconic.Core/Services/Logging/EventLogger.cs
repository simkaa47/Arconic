using Arconic.Core.Models.Event;
using Arconic.Core.Services.Events;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Logging;

public class EventLogger:ILogger, IDisposable
{
    private  EventMainService? _eventMainService;
    private readonly Func<EventMainService?> _getEventService;
    public EventLogger(Func<EventMainService?> getEventService)
    {
        _getEventService = getEventService;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    public void Dispose()
    {
        
    }
    
    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (eventId != 0 || state is null) return;
        if (_eventMainService == null)
        {
            _eventMainService = _getEventService?.Invoke();
            if (_eventMainService is null) return;
        }

        await _eventMainService.OnEventOccure(new EventHistoryItem()
        {
            Message = state.ToString(),
            EventType = logLevel == LogLevel.Information ? EventType.HmiEvent : EventType.HmiError,
            Description = exception?.ToString() ?? ""
        });
    }
}