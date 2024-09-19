using Arconic.Core.Infrastructure.DataContext;
using Arconic.Core.Models.AccessControl;
using Microsoft.EntityFrameworkCore.Storage;

namespace Arconic.Core.Models.SingleMeasures;

public class SingleMeasure:Entity
{
    public DateTime DateTime { get; set; }
    public User? User { get; set; }
    public long? UserId { get; set; }
    public int Duration { get; set; }
    public float Result { get; set; }
}