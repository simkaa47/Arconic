using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData.Measure;

public class MeasureSettings
{
    public PlanshetSettings PlanshetSettings { get; } = new PlanshetSettings();
    public Parameter<ushort> StanOpenTime { get; } =
        new Parameter<ushort>("Время стандартизации при открытом затворе, мс.", 1000, 20000, DataType.DataBlock, 1, 1200);
    public Parameter<ushort> StanCloseTime { get; } =
        new Parameter<ushort>("Время стандартизации при закрытом затворе, мс.", 1000, 20000, DataType.DataBlock, 1, 1202);
    public Parameter<DateTime> LastStandTime { get; } =
        new Parameter<DateTime>("Дата и время последней стандартизации", DateTime.MinValue, DateTime.MaxValue, DataType.DataBlock, 1, 1204);
    public Parameter<ushort> SingleMeasTime { get; } =
        new Parameter<ushort>("Время единичного измерения, с.", 1, 2000, DataType.DataBlock, 1, 1220);
    
    public Parameter<ushort> TemperatureSv { get; } =
        new Parameter<ushort>("Уставка температуры, С.", 25, 1600, DataType.DataBlock, 1, 1228);
    
    public Parameter<ushort> CloseGateTimeSv { get; } = new Parameter<ushort>("Время закрытия затвора после полосы, с",
        10, 600, DataType.DataBlock, ParameterBase.SettingsDbNum, 1222);
    public Parameter<ushort> AverageTime { get; } = new Parameter<ushort>("Время усреднения (режим ЦЛ), мс",
        100, 1000, DataType.DataBlock, ParameterBase.SettingsDbNum, 1224);
    public Parameter<ushort> SendDataToAsutpPeriod { get; } = new Parameter<ushort>("Период выдачи данных толщины в АСУ (режим ЦЛ), мс",
        50, 1000, DataType.DataBlock, ParameterBase.SettingsDbNum, 1226);
    
    public Parameter<short> AoThickDeviationMinAdc { get; } = new Parameter<short>("Аналоговый выход отклонения толщины, мин. значение АЦП",
        short.MinValue, short.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1230);
    public Parameter<short> AoThickDeviationMaxAdc { get; } = new Parameter<short>("Аналоговый выход отклонения толщины, макс. значение АЦП",
        short.MinValue, short.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1232);
    public Parameter<float> AoThickDeviationMinValue { get; } = new Parameter<float>("Аналоговый выход отклонения толщины, мин. значение отклонения, %",
        float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1234);
    public Parameter<float> AoThickDeviationMaxValue { get; } = new Parameter<float>("Аналоговый выход отклонения толщины, макс. значение отклонения, $",
        float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1238);
    public Parameter<float> LastStandTemperature { get; } =
        new Parameter<float>("Температура во время последней стандартизации, С", 
            float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1242){IsReadOnly = true};
    
    public Parameter<float> K1 { get; } =
        new Parameter<float>("К-т компенсации по температуре", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1246);
    public Parameter<float> K2 { get; } =
        new Parameter<float>("К-т постраивания под под тощиномер Thermo", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1250);
    public Parameter<float> K3 { get; } =
        new Parameter<float>("K3", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1254);
    public Parameter<float> K4 { get; } =
        new Parameter<float>("K4", float.MinValue, float.MaxValue, DataType.DataBlock, ParameterBase.SettingsDbNum, 1258);
    
}