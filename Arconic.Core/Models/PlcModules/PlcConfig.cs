namespace Arconic.Core.Models.PlcModules;

public class PlcConfig
{
    public List<IPlcModule> Modules { get; } = new List<IPlcModule>();
    
    public Di16Module DiA2 { get; }
    private Do16Module DoA2 { get; }
    private AoModule AoA4 { get; }
    
    public Do16Module DoA3 { get; }
    public Do16Module DoA4 { get; }

    public Di16Module DiA5 { get; }
    public Di16Module DiA6 { get; }
    public Di16Module DiA7 { get; }
    public Di16Module DiA8 { get; }
    private AiModule AiA9 { get; }
    private AiModule AiA10 { get; }

    public PlcConfig()
    {
        // A2 - Di
        DiA2 = new Di16Module("ШЭ - A2 - Дискретные входы",
            0, 300, 200, 
            100, diModuleType:DiModuleType.Dio32);
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
        DoA2 = new Do16Module("ШЭ - A2 - Дискретные выходы", 
            50, 
            302, 
            202, 
            0, doModuleType:DoModuleType.Dio32);
        DoA2.Sensors[0].Description = "Готовность системы к работе";
        DoA2.Sensors[1].Description = "Идет измерение";
        Modules.Add(DoA2);
        
        // A4 - AO
        AoA4 = new AoModule("ШЭ - A4 - Аналоговые выходы",
            52,
            304,
            204,
            2);
        AoA4.Sensors[0].Description = "Отклонение толщины от заданной";
        Modules.Add(AoA4);
        
        // A3 - D0
        DoA3 = new Do16Module("ШСД - A3 - Дискретные выходы", 
            60, 
            312, 
            212, 
            10);
        DoA3.Sensors[0].Description = "Движение вперед";
        DoA3.Sensors[1].Description = "Движение назад";
        DoA3.Sensors[2].Description = "Стоп";
        DoA3.Sensors[3].Description = "Чиллер";
        DoA3.Sensors[4].Description = "Генератор";
        DoA3.Sensors[5].Description = "Двигатель";
        DoA3.Sensors[8].Description = "Рама в положении \"измерение\"";
        DoA3.Sensors[9].Description = "Рама в положении \"парковка\"";
        DoA3.Sensors[10].Description = "Рама в режиме  \"Аварийный СТОП\"";
        DoA3.Sensors[11].Description = "Затвор - открыт";
        DoA3.Sensors[12].Description = "Затвор - закрыт";
        DoA3.Sensors[13].Description = "Рама в  сервисном режиме";
        DoA3.Sensors[14].Description = "Состояние рентгена";
        Modules.Add(DoA3);
        
        // A4 - D0
        DoA4 = new Do16Module("ШСД - A4 - Дискретные выходы", 
            62, 
            314, 
            214, 
            12);
        DoA4.Sensors[0].Description = "Красная лампа";
        DoA4.Sensors[1].Description = "Желтая лампа";
        DoA4.Sensors[2].Description = "Зеленая лампа";
        DoA4.Sensors[3].Description = "Затвор";
        DoA4.Sensors[4].Description = "Образец 1";
        DoA4.Sensors[5].Description = "Образец 2";
        DoA4.Sensors[7].Description = "Синяя лампа";
        DoA4.Sensors[15].Description = "Дистанционное включение чиллера";
        Modules.Add(DoA4);
        
        // A5 - Di
        DiA5 = new Di16Module("ШCД - A5 - Дискретные входы",
            10, 316, 216, 
            110);
        DiA5.Sensors[0].Description = "Рама - движение в \"измерение\"";
        DiA5.Sensors[1].Description = "Рама - движение в \"парковка\"";
        DiA5.Sensors[2].Description = "Рама  \"Аварийный СТОП\"";
        DiA5.Sensors[3].Description = "Затвор - открыть";
        DiA5.Sensors[4].Description = "Затвор - закрыть";
        DiA5.Sensors[5].Description = "Рама - пререход в сервисный режим";
        DiA5.Sensors[6].Description = "Включить генератор";
        DiA5.Sensors[7].Description = "Выключить генератор";
        DiA5.Sensors[8].Description = "Аварйное реле ПЧ";
        DiA5.Sensors[9].Description = "Автомат чиллера включен";
        DiA5.Sensors[10].Description = "Автомат чиллера включен";
        DiA5.Sensors[11].Description = "Автомат двигателя включен";
        DiA5.Sensors[12].Description = "Автомат резерв включен";
        DiA5.Sensors[13].Description = "Пускатель чиллера включен";
        DiA5.Sensors[14].Description = "Пускатель генератора включен";
        DiA5.Sensors[15].Description = "Пускатель двигателя включен";
        Modules.Add(DiA5);
        
        // A6 - Di
        DiA6 = new Di16Module("ШCД - A6 - Дискретные входы",
            12, 318, 218, 
            112);
        DiA6.Sensors[0].Description = "Рама - авария в позиции \"измерение\"";
        DiA6.Sensors[1].Description = "Рама - в позиции  \"парковка\"";
        DiA6.Sensors[2].Description = "Рама - авария в позиции \"парковка\"";
        DiA6.Sensors[6].Description = "Поток с баков есть";
        Modules.Add(DiA6);
        
        // A7 - Di
        DiA7 = new Di16Module("ШCД - A7 - Дискретные входы",
            14, 320, 220, 
            114);
        DiA7.Sensors[0].Description = "Люк трубки закрыт";
        DiA7.Sensors[1].Description = "Люк генератора 1 закрыт";
        DiA7.Sensors[2].Description = "Люк генератора 1 закрыт";
        DiA7.Sensors[3].Description = "Люк рамы 1 закрыт";
        DiA7.Sensors[4].Description = "Люк БД закрыт";
        DiA7.Sensors[8].Description = "Затвор открыт";
        DiA7.Sensors[9].Description = "Затвор закрыт";
        DiA7.Sensors[10].Description = "Образец 1 открыт";
        DiA7.Sensors[11].Description = "Образец 1  закрыт";
        DiA7.Sensors[12].Description = "Образец 2 открыт";
        DiA7.Sensors[13].Description = "Образец 2  закрыт";
        Modules.Add(DiA7);
        
        // A8 - Di
        DiA8 = new Di16Module("ШCД - A8 - Дискретные входы",
            16, 322, 222, 
            116);
        DiA8.Sensors[0].Description = "Аварийное  реле чиллера";
        DiA8.Sensors[1].Description = "Реле давления  сжатого воздуха";
        DiA8.Sensors[2].Description = "Реле протока ОЖ от баков";
        DiA8.Sensors[3].Description = "Реле протока ОЖ от трубки";
        
        Modules.Add(DiA8);
        
        // A9 - Ai
        AiA9 = new AiModule("ШCД - A9 - аналоговые входы",
            18, 324, 224, 
            118);
        Modules.Add(AiA9);
        
        // A10 - Ai
        AiA10 = new AiModule("ШCД - A10 - аналоговые входы",
            26, 332, 232, 
            126, sensorsNum:8);
        AiA10.Sensors[0].Description = "Т. корпуса трубки";
        AiA10.Sensors[1].Description = "Т. шланга трубки";
        Modules.Add(AiA10);
    }
}