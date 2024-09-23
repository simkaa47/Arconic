namespace Arconic.Core.Models.PlcData.DiagnBoards;

public class DbData
{
    public List<DiagnBoardInfo> Data { get; } =
    [
        new DiagnBoardInfo(1000, 250, "Диагностическая плата в БД"),
        new DiagnBoardInfo(1018, 310, "Диагностическая плата около БД"),
        new DiagnBoardInfo(1036, 370, "Диагностическая плата около БИ"),
        new DiagnBoardInfo(1054, 430, "Диагностическая плата  под генератором"),
    ];
}