using _3TierBase.Business.ViewModels;
using Microsoft.AspNetCore.Http;

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

            ErrorResponse errorResponse = new(statusCode: StatusCodes.Status400BadRequest, msg: cEx.ErrorMessage, detail: null);

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
