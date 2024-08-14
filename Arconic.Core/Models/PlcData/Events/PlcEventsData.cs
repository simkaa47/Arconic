using Arconic.Core.Models.Event;

namespace Arconic.Core.Models.PlcData.Events;

public class PlcEventsData
{
    public PlcEventsData()
    {
        for (int i = 0; i < Events.Count; i++)
        {
            Events[i].SetCode((i+1000).ToString("0000"));
        }
    }
    
    private List<PlcEvent> Events { get; } =
    [
        new PlcEvent("Начало стандартизации",2,800, 0),
        new PlcEvent("Стандартизация закончилась успешно",2,800, 1),
        new PlcEvent("Стандартизация закончилась с ошибкой",2,800, 2),
        new PlcEvent("Начало полосы",2,800, 3),
        new PlcEvent("Конец полосы",2,800, 4),
        new PlcEvent("Закрыт завтор из-за долгого ожидания полосы",2,800, 5),
        new PlcEvent("Стандартизация прервана из-за новой полосы",2,800, 6),
        new PlcEvent("Старт единичного измерения",2,800, 7),
        new PlcEvent("Конец единичного измерения",2,801, 0),
        new PlcEvent("Ошибка единичного измерения",2,801, 1),
        new PlcEvent("Установлено поключение с АСУ",2,801, 2),
    ];
}