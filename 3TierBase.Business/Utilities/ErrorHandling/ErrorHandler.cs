using _3TierBase.Business.ViewModels;

namespace _3TierBase.Business.Utilities.ErrorHandling
{
    public class ErrorHandler : IErrorHandler
    {
        public ErrorResponse? HandlerError(Exception exception)
        {
            if (exception == null || exception is not CException cEx)
            {
                return null;
            }

            ErrorResponse errorResponse = new(statusCode: cEx.StatusCode, msg: cEx.ErrorMessage, detail: cEx.StackTrace);

            return errorResponse;
        }

        public ErrorResponse? UnhandlerError(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            ErrorResponse errorResponse = new(msg: exception.Message, detail: exception.StackTrace);

            return errorResponse;
        }
    }
}
