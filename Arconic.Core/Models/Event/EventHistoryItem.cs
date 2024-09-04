using System.ComponentModel.DataAnnotations;
using Arconic.Core.Infrastructure.DataContext;
using Arconic.Core.Models.AccessControl;

namespace Arconic.Core.Models.Event;

public class EventHistoryItem:Entity
{
    public DateTime Date { get; set; }
    public User? User { get; set; } 
    public long? UserId { get; init; }
    [MaxLength(200)]
    public string? Message { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    [MaxLength(4)]
    public string? EventCode { get; init; }
    public EventType EventType { get; init; }
}