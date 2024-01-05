using _3TierBase.Business.Services.MailServices;
using _3TierBase.Business.ViewModels;
using _3TierBase.Business.ViewModels.Mails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
    public class MailController : BaseApiController
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Endpoint for send email
        /// </summary>
        /// <returns>Successfully message</returns>
        /// <response code="200">Send email successfully</response>
        [HttpPost()]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public IActionResult Create(MailInputModel requestBody)
        {
            _mailService.SendEmail(requestBody);
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Msg = $"Send to email: {requestBody.ToEmail} successfully",
                Success = true,
                Data = "",
            });
        }
    }
}
