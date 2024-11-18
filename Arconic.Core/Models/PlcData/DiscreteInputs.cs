using Arconic.Core.Models.Parameters;
using S7.Net;

namespace Arconic.Core.Models.PlcData;

public class DiscreteInputs
{
    #region Металл в клети
    public Parameter<bool> MetallOnRoll { get; } =
        new Parameter<bool>("Металл в клети", false, true, DataType.Input,  0, 1, 0);
    #endregion
    #region Авария привода
    public Parameter<bool> DriveError { get; } =
        new Parameter<bool>("Авария привода", false, true, DataType.Input,  0, 11, 0);
    #endregion
    #region Защитный автомат чиллера
    public Parameter<bool> QfChiller { get; } =
        new Parameter<bool>("Защитный автомат чиллера", false, true, DataType.Input,  0, 11, 1);
    #endregion
    #region Защитный автомат генератора
    public Parameter<bool> QfGenerator { get; } =
        new Parameter<bool>("Защитный автомат генератора", false, true, DataType.Input,  0, 11, 2);
    #endregion
    #region Защитный автомат привода
    public Parameter<bool> QfDrive { get; } =
        new Parameter<bool>("Защитный автомат привода", false, true, DataType.Input,  0, 11, 3);
    #endregion
    #region Контактор чиллера
    public Parameter<bool> SqContactorChiller { get; } =
        new Parameter<bool>("Контактор чиллера", false, true, DataType.Input,  0, 11, 5);
    #endregion
    #region Контактор генератора
    public Parameter<bool> SqGeneratorChiller { get; } =
        new Parameter<bool>("Контактор генератора", false, true, DataType.Input,  0, 11, 6);
    #endregion
    #region Контактор привода
    public Parameter<bool> SqDriveChiller { get; } =
        new Parameter<bool>("Контактор привода", false, true, DataType.Input,  0, 11, 7);
    #endregion
    
    #region Кнопка ЛПУ "Сервис"
    public Parameter<bool> SbService { get; } =
        new Parameter<bool>("Кнопка ЛПУ \"Сервис\"", false, true, DataType.Input,  0, 10, 5);
    #endregion
    
    #region Датчик открытого затвора
    public Parameter<bool> SqGateOpen0 { get; } =
        new Parameter<bool>("Датчик открытого затвора", false, true, DataType.Input,  0, 15, 0);
    #endregion
    #region Датчик закрытого затвора
    public Parameter<bool> SqGateСlose0 { get; } =
        new Parameter<bool>("Датчик закрытого затвора", false, true, DataType.Input,  0, 15, 1);
    #endregion
    #region Образец 5 мм открыт
    public Parameter<bool> SqGateOpen1 { get; } =
        new Parameter<bool>("Образец 5 мм открыт", false, true, DataType.Input,  0, 15, 2);
    #endregion
    #region Датчик открытого дополнительного затвора 2
    public Parameter<bool> SqGateClose1 { get; } =
        new Parameter<bool>("Образец 10 мм открыт", false, true, DataType.Input,  0, 15, 3);
    #endregion
    #region Датчик открытого дополнительного затвора 1
    public Parameter<bool> SqGateOpen2 { get; } =
        new Parameter<bool>("Образец 5 мм закрыт", false, true, DataType.Input,  0, 15, 4);
    #endregion
    #region Датчик открытого дополнительного затвора 2
    public Parameter<bool> SqGateClose2 { get; } =
        new Parameter<bool>("Образец 10 мм закрыт", false, true, DataType.Input,  0, 15, 5);
    #endregion
    
    
    
    
}