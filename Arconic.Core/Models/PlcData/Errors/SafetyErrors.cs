using Arconic.Core.Infrastructure.Extentions;
using Arconic.Core.Models.Event;

namespace Arconic.Core.Models.PlcData.Errors;

public class SafetyErrors
{
    public SafetyErrors()
    {
        for (int i = 0; i < Errors.Count; i++)
        {
            Errors[i].SetTag(PlcError.SafetyTag);    
            Errors[i].SetCode((i+200).ToString("0000"));
        }
    }
    
    private List<PlcError> Errors { get; } =
    [
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Нажата кнопка \"Аварийный стоп на ЛПУ\"", 2, 734, 0),
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Нажата кнопка \"Аварийный стоп на шкафу электроники\"", 2, 734, 1),
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Сработал аварийный концевик люка к трубке генератора", 2, 734, 2),
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Сработал аварийный концевик к генератору 1", 2, 734, 3),
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Сработал аварийный концевик к генератору 2", 2, 734, 4),
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Сработал аварийный концевик рамы", 2, 734, 5),
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Сработал аварийный концевик блока детектирования", 2, 734, 6),
        new PlcError($"{PlcError.SafetyTag.ToTitleCase()}: Нажата кнопка \"СТОП\" на HMI", 2, 734, 7)
    ];
}