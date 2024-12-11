using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Models.AccessControl;
using Arconic.Core.Models.Event;
using Arconic.Core.Services.Access;

namespace Arconic.Core.Services.Events;

public class EventMainService
{
    private readonly AccessService _accessService;
    private readonly IRepository<EventHistoryItem> _eventRepo;
    public event Action? ErrorOccuredEvent;
    public event Action<EventHistoryItem>? EventOccuredEvent;

    public EventMainService(AccessService accessService,
        IRepository<EventHistoryItem> eventRepo)
    {
        _accessService = accessService;
        _eventRepo = eventRepo;
        foreach (var err in PlcError.Errors)
        {
            DescribeOnEvent(err); 
        }
        foreach (var ev in PlcEvent.Events)
        {
            DescribeOnEvent(ev); 
        }
    }

    private void DescribeOnEvent(BaseEvent baseEvent)
    {
        baseEvent.PropertyChanged += async (s, args) =>
        {
            if (args.PropertyName == nameof(baseEvent.IsActive))
            {
                if (baseEvent.IsActive || baseEvent is PlcError)
                {
                    if (baseEvent.EventType == EventType.PlcError)
                    {
                        ErrorOccuredEvent?.Invoke();
                    }
                    await OnEventOccure(new EventHistoryItem
                    {
                        EventType = baseEvent.EventType,
                        IsActive = baseEvent.IsActive,
                        Message = baseEvent.ActiveMessage,
                        EventCode = baseEvent.Code
                    });
                }
            }
        };
    }

    public async Task OnEventOccure(EventHistoryItem item)
    {
        try
        {
            item.Date = DateTime.Now;
            
            if (_accessService.CurrentUser is not null && _accessService.CurrentUser.Id > 0)
            {
                item.UserId = _accessService.CurrentUser?.Id;
            }
            
            await _eventRepo.AddAsync(item);
            item.User = _accessService.CurrentUser;
            EventOccuredEvent?.Invoke(item);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}