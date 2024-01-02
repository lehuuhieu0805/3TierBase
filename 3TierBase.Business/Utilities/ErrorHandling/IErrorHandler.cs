using _3TierBase.Business.ViewModels;

namespace _3TierBase.Business.Utilities.ErrorHandling
{
    public interface IErrorHandler
    {
        ErrorResponse? HandlerError(Exception exception);
        ErrorResponse? UnhandlerError(Exception exception);
    }
}
