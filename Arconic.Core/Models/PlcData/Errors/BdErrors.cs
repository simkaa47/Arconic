using Arconic.Core.Infrastructure.Extentions;
using Arconic.Core.Models.Event;

namespace Arconic.Core.Models.PlcData.Errors;
/// <summary>
/// Ошибки блока детектора
/// </summary>
public class BdErrors
{
    public BdErrors()
    {
        for (int i = 0; i < Errors.Count; i++)
        {
            Errors[i].SetTag(PlcError.DetectorsTag);    
            Errors[i].SetCode((i+100).ToString("0000"));
        }
    }
    private List<PlcError> Errors { get; } =
    [
        new PlcError($"{PlcError.DetectorsTag.ToTitleCase()}: Нет связи с платой контроллера", 2, 732, 0),
        new PlcError($"{PlcError.DetectorsTag.ToTitleCase()}: Ошибка одного из детекторов", 2, 732, 1)
    ];
}