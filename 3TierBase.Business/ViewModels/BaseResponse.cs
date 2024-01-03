using Microsoft.AspNetCore.Http;

namespace _3TierBase.Business.ViewModels
{
    public static class SuccessMessageResponse
    {
        public const string SEND_REQUEST = "Send request successfully";
        public const string CREATED_REQUEST = "Created successfully";
        public const string UPDATED_REQUEST = "Updated successfully";
        public const string LOGIN_REQUEST = "Login successfully";
    }

    public class BaseResponse<T>
    {
        public int StatusCode { get; }
        public string Msg { get; }
        public bool Success { get; }
        public T? Data { get; }
        public BaseResponse(T? data, int statusCode = StatusCodes.Status200OK, string msg = SuccessMessageResponse.SEND_REQUEST, bool success = true)
        {
            StatusCode = statusCode;
            Success = success;
            Msg = msg;
            Data = data;
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; }
        public string? Msg { get; }
        public string? Detail { get; }
        public bool Success { get; }
        public ErrorResponse(string? msg, string? detail, int statusCode = StatusCodes.Status500InternalServerError, bool success = false)
        {
            StatusCode = statusCode;
            Success = success;
            Msg = msg;
            Detail = detail;
        }
    }

    public class ModelsResponse<T>
    {
        public int StatusCode { get; }
        public string Msg { get; }
        public bool Success { get; }
        public IList<T> Data { get; }
        public PagingResponse Paging { get; }
        public ModelsResponse(PagingResponse paging, IList<T> data, int statusCode = StatusCodes.Status200OK,
            bool success = true, string msg = SuccessMessageResponse.SEND_REQUEST)
        {
            StatusCode = statusCode;
            Success = success;
            Msg = msg;
            Data = data;
            Paging = paging;
        }
    }

    public class ModelDataLoginResponse
    {
        public string Token { get; set; }
        public ModelDataLoginResponse(string token)
        {
            Token = token;
        }
    }

    public class ModelLoginResponse
    {
        public int StatusCode { get; }
        public string Msg { get; }
        public bool Success { get; }
        public ModelDataLoginResponse Data { get; }
        public ModelLoginResponse(ModelDataLoginResponse data, int statusCode = StatusCodes.Status200OK,
            bool success = true, string msg = SuccessMessageResponse.LOGIN_REQUEST)
        {
            StatusCode = statusCode;
            Success = success;
            Msg = msg;
            Data = data;
        }
    }

    public class PagingResponse
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
    }
}