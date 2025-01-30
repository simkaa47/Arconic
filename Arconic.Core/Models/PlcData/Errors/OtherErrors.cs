using Arconic.Core.Infrastructure.Extentions;
using Arconic.Core.Models.Event;
using Arconic.Core.Models.Parameters;

namespace Arconic.Core.Models.PlcData.Errors;

public class OtherErrors
{
    public OtherErrors()
    {
        for (int i = 0; i < Errors.Count; i++)
        {
            Errors[i].SetTag(PlcError.OtherTag);    
            Errors[i].SetCode((i+800).ToString("0000"));
        }
    }
    
    private List<PlcError> Errors { get; } =
    [
        new PlcError($"{PlcError.OtherTag.ToTitleCase()}: Нет связи с АСУ предприятия", ParameterBase.IndicationDbNum, 742, 0),
        new PlcError($"{PlcError.OtherTag.ToTitleCase()}: Необходимо произвести стандартизацию", ParameterBase.IndicationDbNum, 744, 0),
        new PlcError($"{PlcError.OtherTag.ToTitleCase()}: Нет связи с датчиком положения", ParameterBase.IndicationDbNum, 744, 1),
        new PlcError($"{PlcError.OtherTag.ToTitleCase()}: Нет сигнала с датчика давления в пневмосистеме", ParameterBase.IndicationDbNum, 744, 2)
        
    ];
}