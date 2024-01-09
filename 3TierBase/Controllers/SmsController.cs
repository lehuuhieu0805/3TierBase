using _3TierBase.Business.Services.SendSms;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
    [ControllerName("Sm")]
    public class SmsController : BaseApiController
    {
        private readonly ISmsService _sendSmsService;
        public SmsController(ISmsService sendSmsService)
        {
            _sendSmsService = sendSmsService;
        }

        /// <summary>
        /// Endpoint for send an SMS verification code
        /// </summary>
        /// <returns>Token of user</returns>
        /// <response code="200">Returns a token of user</response>
        [HttpGet("sendOTP")]
        public IActionResult SendAVerificationCode(string receiverPhoneNumber)
        {
            _sendSmsService.SendASMSVerificationCode(receiverPhoneNumber);
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Msg = $"Send an verification code to {receiverPhoneNumber} successfully",
                Success = true,
                Data = "",
            });
        }

        /// <summary>
        /// Endpoint for check a verification code
        /// </summary>
        /// <returns>Token of user</returns>
        /// <response code="200">Returns a token of user</response>
        [HttpGet("verificationOTP")]
        public IActionResult CheckAnVerificationCode(string receiverPhoneNumber, string code)
        {
            bool isValid = _sendSmsService.CheckAVerificationCode(receiverPhoneNumber, code);
            if (isValid)
            {
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Msg = "Verification code is valid",
                    Success = true,
                    Data = "",
                });
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Msg = "Verification code is not valid",
                    Success = false,
                    Data = "",
                });
            }
        }
    }
}
