using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData;

public class DiscreteOutputs
{
    #region Готовность к измерениям
    public Parameter<bool> MeasReady { get; } = new Parameter<bool>("Флаг готовности к измерениям", false, true,
        DataType.Output, 0, 0, 0);
    #endregion
    #region Идет измерение
    public Parameter<bool> MeasuringFlag { get; } = new Parameter<bool>("Идет измерение", false, true,
        DataType.Output,  0, 0, 1);
    #endregion
    #region Рама движется вперед
    public Parameter<bool> RackForw { get; } = new Parameter<bool>("Рама движется вперед", false, true,
        DataType.Output,  0, 10, 0);
    #endregion
    #region Рама движется назад
    public Parameter<bool> RackRev { get; } = new Parameter<bool>("Рама движется назад", false, true,
        DataType.Output,  0, 10, 1);
    #endregion
    #region Контактор чиллера
    public Parameter<bool> ChillerPowerOn { get; } = new Parameter<bool>("Контактор чиллера", false, true,
        DataType.Output,  0, 10, 3);
    #endregion
    #region Контактор генератора
    public Parameter<bool> GeneratorPowerOn { get; } = new Parameter<bool>("Контактор генератора", false, true,
        DataType.Output,  0, 10, 4);
    #endregion
    #region Контактор ПЧ
    public Parameter<bool> FrequenceConverterPowerOn { get; } = new Parameter<bool>("Контактор ПЧ", false, true,
        DataType.Output, 0, 10, 5);
    #endregion
    #region Лампа "Рама в положении измерения"
    public Parameter<bool> HlRackInMeasurePosition { get; } = new Parameter<bool>("Рама в положении измерения", false, true,
        DataType.Output,  0, 11, 0);
    #endregion
    #region Лампа "Рама в положении парковки"
    public Parameter<bool> HlRackInParkingPosition { get; } = new Parameter<bool>("Рама в положении парковки", false, true,
        DataType.Output,  0, 11, 1);
    #endregion
    #region Лампа "Рама в режиме аварийного стопа"
    public Parameter<bool> HlRackInAbortMode { get; } = new Parameter<bool>("Рама в режиме аварийного стопа", false, true,
        DataType.Output,  0, 11, 2);
    #endregion
    #region Лампа "Затвор открыт"
    public Parameter<bool> HlGateOpened { get; } = new Parameter<bool>("Затвор открыт", false, true,
        DataType.Output,  0, 11, 3);
    #endregion
    #region Лампа "Затвор закрыт"
    public Parameter<bool> HlGateClosed { get; } = new Parameter<bool>("Затвор закрыт", false, true,
        DataType.Output,  0, 11, 4);
    #endregion
    #region Лампа "Сервисный режим"
    public Parameter<bool> HlServiceMode { get; } = new Parameter<bool>("Сервисный режим", false, true,
        DataType.Output,  0, 11, 5);
    #endregion
    #region Лампа "Состояние генератора"
    public Parameter<bool> HlGeneratorState { get; } = new Parameter<bool>("Состояние генератора", false, true,
        DataType.Output,  0, 11, 6);
    #endregion
    #region Светофор - зеленый
    public Parameter<bool> TrafficLightsGreen { get; } = new Parameter<bool>("Светофор - зеленый", false, true,
        DataType.Output,  0, 12, 0);
    #endregion
    #region Светофор - красный
    public Parameter<bool> TrafficLightsRed { get; } = new Parameter<bool>("Светофор - красный", false, true,
        DataType.Output,  0, 12, 1);
    #endregion
    #region Светофор - желтый
    public Parameter<bool> TrafficLightsYellow { get; } = new Parameter<bool>("Светофор - желтый", false, true,
        DataType.Output,  0, 12, 2);
    #endregion
    #region Сигнал основного завтора
    public Parameter<bool> Gate0 { get; } = new Parameter<bool>("Сигнал основного завтора", false, true,
        DataType.Output,  0, 12, 3);
    #endregion
    #region Сигнал дополнительного затвора 1
    public Parameter<bool> Gate1 { get; } = new Parameter<bool>("Сигнал дополнительного затвора 1", false, true,
        DataType.Output,  0, 12, 4);
    #endregion
    #region Сигнал дополнительного затвора 2
    public Parameter<bool> Gate2 { get; } = new Parameter<bool>("Сигнал дополнительного затвора 2", false, true,
        DataType.Output,  0, 12, 5);
    #endregion
}