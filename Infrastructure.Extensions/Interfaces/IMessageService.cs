namespace Infrastructure.Extensions.Interfaces
{
    public interface IMessageService
    {
        void ConsoleMessage(string Message, string CallerName = null);
        void InfoMessage(string Message, string CallerName = null);
        void ErrorMessage(string Message, string CallerName = null);
        void ExceptionMessage(string Message, string CallerName = null);
    }
}
