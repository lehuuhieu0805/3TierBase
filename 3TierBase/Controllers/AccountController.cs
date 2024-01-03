using _3TierBase.Business.Services.UserServices;
using _3TierBase.Business.ViewModels;
using _3TierBase.Business.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Endpoint for user login
        /// </summary>
        /// <returns>Token of user</returns>
        /// <response code="200">Returns a token of user</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModelLoginResponse), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Login(UserLoginModel requestBody)
        {
            return Ok(new ModelLoginResponse(
                new ModelDataLoginResponse(token: await _userService.Login(requestBody))
            ));
        }
    }
}
