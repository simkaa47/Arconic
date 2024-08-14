using Arconic.Core.Services.Events;
using Microsoft.Extensions.Logging;

namespace Arconic.Core.Services.Logging;

public static class LoggerExtensions
{
    public static ILoggingBuilder AddDatabaseLogging(this ILoggingBuilder builder, Func<EventMainService?> getEventService)
    {
        builder.AddProvider(new EventLoggerProvider(getEventService));
        return builder;
    }
}