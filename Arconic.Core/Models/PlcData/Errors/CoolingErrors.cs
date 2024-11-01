using Arconic.Core.Infrastructure.Extentions;
using Arconic.Core.Models.Event;

namespace Arconic.Core.Models.PlcData.Errors;

public class CoolingErrors
{
    public CoolingErrors()
    {
        for (int i = 0; i < Errors.Count; i++)
        {
            Errors[i].SetTag(PlcError.CoolingTag);    
            Errors[i].SetCode((i+500).ToString("0000"));
        }
    }
    
    private List<PlcError> Errors { get; } =
    [
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Cработал защитный автомат чиллера", 2, 740, 0),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Cработало аварийное реле чиллера", 2, 740, 1),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Ошибка срабатывания контактора чиллера", 2, 740, 2),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Ошибка протока охлаждающей жидкости на баки", 2, 740, 3),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Ошибка протока охлаждающей жидкости из баков", 2, 740, 4),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Ошибка протока охлаждающей жидкости с трубки генератора", 2, 740, 5),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Ошибка протока охлаждающей жидкости на трубку генератора", 2, 740, 6),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Ошибка протока охлаждающей жидкости на баки (гидрошкаф)", 2, 740, 7),
        new PlcError($"{PlcError.CoolingTag.ToTitleCase()}: Ошибка протока охлаждающей жидкости на трубки (гидрошкаф)", 2, 741, 0)
    ];
}