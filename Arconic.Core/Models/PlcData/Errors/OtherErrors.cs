using Arconic.Core.Infrastructure.Extentions;
using Arconic.Core.Models.Event;

namespace Arconic.Core.Models.PlcData.Errors;

public class OtherErrors
{
    public OtherErrors()
    {
        for (int i = 0; i < Errors.Count; i++)
        {
            Errors[i].SetTag(PlcError.OtherTag);    
            Errors[i].SetCode((i+900).ToString("0000"));
        }
    }
    
    private List<PlcError> Errors { get; } =
    [
        new PlcError($"{PlcError.OtherTag.ToTitleCase()}: Нет связи с АСУ предприятия", 2, 742, 0)
        
    ];
}