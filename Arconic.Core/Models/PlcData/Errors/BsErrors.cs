using Arconic.Core.Infrastructure.Extentions;
using Arconic.Core.Models.Event;

namespace Arconic.Core.Models.PlcData.Errors;

public class BsErrors
{
    public BsErrors()
    {
        for (int i = 0; i < Errors.Count; i++)
        {
            Errors[i].SetTag(PlcError.SourceTag);    
            Errors[i].SetCode(i.ToString("0000"));
        }
    }
    
    public List<PlcError> Errors { get; } = new List<PlcError>
    {
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Ошибка открытия основного затвора",  2, 730, 0),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Ошибка закрытия основного затвора",  2, 730, 1),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Ошибка открытия дополнительного затвора 1",  2, 730, 2),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Ошибка закрытия дополнительного затвора 1",  2, 730, 3),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Ошибка открытия дополнительного затвора 2",  2, 730, 4),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Ошибка закрытия дополнительного затвора 2",  2, 730, 5),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Нет готовности от системы охлаждения",  2, 730, 6),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Нет готовности от системы безопасности",  2, 730, 7),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Нет готовности от системы контроля диагностических плат",  2, 731, 0),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Разомкнута цепь безопасности генератора",  2, 731, 1),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Нет готовности генератора",  2, 731, 2),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Сработал защитный автомат генератора",  2, 731, 3),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Ошибка срабатывания концевика подачи питания на генератор",  2, 731, 4),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Нет связи с генератором",  2, 731, 5),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Снято питание из-за отстутствия связи генератора и ПЛК",  2, 731, 6),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Необходимо произвести кондиционирование",  2, 731, 7),
        new PlcError($"{PlcError.SourceTag.ToTitleCase()}: Нажата кнопка \"ВКЛ/ОТКЛ Питания HV\" на ЛПУ",  2, 746, 0)
    };
}