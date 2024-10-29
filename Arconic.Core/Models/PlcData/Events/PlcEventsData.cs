using Arconic.Core.Models.Event;
using Arconic.Core.Models.Parameters;
using S7.Net;

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
        new PlcEvent("Начало стандартизации",ParameterBase.IndicationDbNum,800, 0),
        new PlcEvent("Стандартизация закончилась успешно",ParameterBase.IndicationDbNum,800, 1),
        new PlcEvent("Стандартизация закончилась с ошибкой",ParameterBase.IndicationDbNum,800, 2),
        new PlcEvent("Начало полосы",ParameterBase.IndicationDbNum,800, 3),
        new PlcEvent("Конец полосы",ParameterBase.IndicationDbNum,800, 4),
        new PlcEvent("Закрыт завтор из-за долгого ожидания полосы",ParameterBase.IndicationDbNum,800, 5),
        new PlcEvent("Стандартизация прервана из-за новой полосы",ParameterBase.IndicationDbNum,800, 6),
        new PlcEvent("Старт единичного измерения",ParameterBase.IndicationDbNum,800, 7),
        new PlcEvent("Конец единичного измерения",ParameterBase.IndicationDbNum,801, 0),
        new PlcEvent("Ошибка единичного измерения",ParameterBase.IndicationDbNum,801, 1),
        new PlcEvent("Установлено поключение с АСУ",ParameterBase.IndicationDbNum,801, 2),
        new PlcEvent("Нажата кнопка \"Рама - движение в позицию Измерение\" на ЛПУ",ParameterBase.IndicationDbNum,801, 3),
        new PlcEvent("Нажата кнопка \"Рама - движение в позицию Парковка\" на ЛПУ",ParameterBase.IndicationDbNum,801, 4),
        new PlcEvent("Нажата кнопка \"Аварийный стоп\" на ЛПУ",ParameterBase.IndicationDbNum,801, 5),
        new PlcEvent("Отжата кнопка \"Аварийный стоп\" на ЛПУ",ParameterBase.IndicationDbNum,801, 6),
        new PlcEvent("Нажата кнопка \"Открыть затвор\" на ЛПУ",ParameterBase.IndicationDbNum,801, 7),
        new PlcEvent("Нажата кнопка \"Закрыть затвор\" на ЛПУ",ParameterBase.IndicationDbNum,802, 0),
        new PlcEvent("Нажата кнопка \"Сервисный режим\" на ЛПУ",ParameterBase.IndicationDbNum,802, 1),
        new PlcEvent("Отжата кнопка \"Сервисный режим\" на ЛПУ",ParameterBase.IndicationDbNum,802, 2),
        new PlcEvent("Нажата кнопка \"Включить генератор\" на ЛПУ",ParameterBase.IndicationDbNum,802, 3),
        new PlcEvent("Нажата кнопка \"Выключить генератор\" на ЛПУ",ParameterBase.IndicationDbNum,802, 4),
        new PlcEvent("Отжата кнопка \"Выключить генератор\" на ЛПУ",ParameterBase.IndicationDbNum,802, 5),
        new PlcEvent("БИ - питание на генератор подано",ParameterBase.IndicationDbNum,802, 6),
        new PlcEvent("БИ - питание на генератор снято",ParameterBase.IndicationDbNum,802, 7),
        new PlcEvent("БИ - HV генератора активно",ParameterBase.IndicationDbNum,803, 0),
        new PlcEvent("БИ - HV генератора снято",ParameterBase.IndicationDbNum,803, 1),
        new PlcEvent("БИ - режим кондиционирования трубки включен",ParameterBase.IndicationDbNum,803, 2),
        new PlcEvent("БИ - кондиционирование трубки выполнено успешно",ParameterBase.IndicationDbNum,803, 3),
        new PlcEvent("БИ - открыт затвор излучателя",ParameterBase.IndicationDbNum,803, 4),
        new PlcEvent("БИ - закрыт затвор излучателя",ParameterBase.IndicationDbNum,803, 5),
        new PlcEvent("БИ - есть связь с генератором",ParameterBase.IndicationDbNum,803, 6),
        new PlcEvent("БИ - нет связи с генератором",ParameterBase.IndicationDbNum,803, 7),
    ];

    public Parameter<bool> StripStart { get; } =
         new Parameter<bool>("Начало полосы", false, true, DataType.DataBlock, 2, 800, 3);
    public Parameter<bool> StripEnd { get; } =
        new Parameter<bool>("Конец полосы", false, true, DataType.DataBlock, 2, 800, 4);
 }