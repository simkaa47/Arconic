using Arconic.Core.Infrastructure.Extentions;
using Arconic.Core.Models.Event;

namespace Arconic.Core.Models.PlcData.Errors;

public class DriveErrors
{
    
    public DriveErrors()
    {
        for (int i = 0; i < Errors.Count; i++)
        {
            Errors[i].SetTag(PlcError.DriveTag);    
            Errors[i].SetCode((i+400).ToString("0000"));
        }
    }
    
    private List<PlcError> Errors { get; } =
    [
        new PlcError($"{PlcError.DriveTag.ToTitleCase()}: Сработал аварийный концевик слева", 2, 738, 0),
        new PlcError($"{PlcError.DriveTag.ToTitleCase()}: Сработал аварийный концевик справа", 2, 738, 1),
        new PlcError($"{PlcError.DriveTag.ToTitleCase()}: Ошибка преобразователя частоты", 2, 738, 2),
        new PlcError($"{PlcError.DriveTag.ToTitleCase()}: Нет связи с преобразователем частоты", 2, 738, 3)
    ];
}