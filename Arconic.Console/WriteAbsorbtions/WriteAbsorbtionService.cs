using Arconic.Core.Services.Plc;

namespace Arconic.Console.WriteAbsorbtions;

public class WriteAbsorbtionService(MainPlcService plcService)
{
    private int _dbNumber = 78;
    
    public async Task WriteElementInfo(string elementName, int elementNumber, int byteOffset, float density)
    {
        System.Console.WriteLine("Запуск сканирования с ПЛК");
        Task.Run(async ()=> await plcService.ScanPlcAsync());
        while (!plcService.IsConnected)
        {
            System.Console.WriteLine("Ожидание связи с ПЛК");
            Thread.Sleep(5000);
        }
        System.Console.WriteLine("Связь с ПЛК появилась");
        try
        {
            var strs = File.ReadAllLines($"NIST/{elementNumber}.txt");
            var elementInfo = new ElementInfo(_dbNumber, byteOffset);
            elementInfo.Density.WriteValue = density;
            float temp = 0;
            elementInfo.Name.WriteValue = elementName;
            var endNumer = Math.Min(60, strs.Length);
            int j = 0;
            for (int i = 1; i < endNumer; i++)
            {
                var strValues = strs[i].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var values =     strValues.Where(s=> float.TryParse(s.Replace(".",","), out temp)).Select(s=>temp).ToList();
                if (values.Count == 3)
                {
                    elementInfo.AbsorbtionInfos[j].EnergyLevel.WriteValue = values[0];
                    elementInfo.AbsorbtionInfos[j].AbsCoeff.WriteValue = values[1];
                    j++;
                }
            }
            //plcService.WriteParameter(elementInfo.Name);
            plcService.WriteParameter(elementInfo.Density);
            foreach (var abs in elementInfo.AbsorbtionInfos)
            {
                plcService.WriteParameter(abs.AbsCoeff);
                plcService.WriteParameter(abs.EnergyLevel);
            }
            System.Console.WriteLine("Запись в ПЛК выполнена успешно");
            System.Console.ReadKey();

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }
}