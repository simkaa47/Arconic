namespace Arconic.Core.Abstractions.Common;

public interface IQuestionDialog
{
    Task<bool> AskAsync(string title, string message);
}