namespace Arconic.Core.Abstractions.FileAccess;

public interface IFileDialog
{
    Task<string> GetDirectory();

    Task<string> GetFile();
}