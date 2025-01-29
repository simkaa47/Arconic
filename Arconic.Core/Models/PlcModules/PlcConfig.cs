namespace Arconic.Core.Models.PlcModules;

public class PlcConfig
{
    public List<IPlcModule> Modules { get; } = new List<IPlcModule>();
    
    public Di16Module DiA2 { get; }

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
    }
}