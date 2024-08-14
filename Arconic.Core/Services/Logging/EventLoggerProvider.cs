using Arconic.Core.Services.Events;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Logging;

public class EventLoggerProvider(Func<EventMainService?> getEventService) : ILoggerProvider
{
    public void Dispose()
    {
        
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new EventLogger(getEventService);
    }
}