namespace Arconic.Core.Models.PlcModules;

public class PlcConfig
{
    public List<IPlcModule> Modules { get; } = new List<IPlcModule>();
    
    public Di16Module DiA2 { get; }
    private Do16Module DoA2 { get; }
    public AoModule AoA4 { get; }

    public PlcConfig()
    {
        // A2 - Di
        DiA2 = new Di16Module("A2 - Дискретные входы",
            0, 300, 200, 100);
        DiA2.Sensors[0].Description = "UPS  FALL";
        DiA2.Sensors[1].Description = "ALARM";
        DiA2.Sensors[2].Description = "BYPASS";
        DiA2.Sensors[3].Description = "BATTERY LOW";
        DiA2.Sensors[4].Description = "UPS ON";
        DiA2.Sensors[5].Description = "LINE LOSS";
        DiA2.Sensors[8].Description = "Начать профилирование";
        DiA2.Sensors[9].Description = "Открыть затвор";
        DiA2.Sensors[10].Description = "Аварийный стоп";
        Modules.Add(DiA2);
        // A2 - D0
        DoA2 = new Do16Module("A2 - Дискретные выходы", 
            50, 
            302, 
            202, 
            0, initaialSchemePosition:16);
        DoA2.Sensors[0].Description = "Готовность системы к работе";
        DoA2.Sensors[1].Description = "Идет измерение";
        Modules.Add(DoA2);
        
        // A4 - AO
        AoA4 = new AoModule("A4 - Аналоговые выходы",
            52,
            304,
            204,
            2);
        AoA4.Sensors[0].Description = "Отклонение толщины от заданной";
        Modules.Add(AoA4);
    }
}