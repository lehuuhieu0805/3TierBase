using _3TierBase.Business.Commons.Paging;
using _3TierBase.Business.Enum;
using _3TierBase.Business.Services.UserServices;
using _3TierBase.Business.ViewModels;
using _3TierBase.Business.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Endpoint for get all user with condition
        /// </summary>
        /// <param name="searchUserModel">An object contains value wanna search</param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of user</returns>
        /// <response code="200">Returns the list of user</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModelsResponse<IList<GetUserDetailModel>>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll(
            [FromQuery] PagingParam<UserEnum.UserSort> paginationModel,
            [FromQuery] SearchUserModel searchUserModel)
        {
            var (users, total) = await _userService.GetAll(paginationModel, searchUserModel);

            return Ok(new ModelsResponse<GetUserDetailModel>(
                    paging: new PagingResponse()
                    {
                        Page = paginationModel.PageIndex,
                        Size = paginationModel.PageSize ?? total,
                        Total = total
                    },
                    data: users
                ));
        }

        /// <summary>
        /// Endpoint for get user by Id
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>List of user</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">Returns if the user is not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<GetUserDetailModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(new BaseResponse<GetUserDetailModel>(
                    data: await _userService.GetById(id)
                ));
        }

        /// <summary>
        /// Endpoint for create user
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <param name="requestBody">An obj contains input info of a user</param>
        /// <returns>A user within status 201 or error status</returns>
        /// <response code="201">Returns the user</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BaseResponse<GetUserDetailModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUserModel requestBody)
        {
            return Ok(new BaseResponse<GetUserDetailModel>(
                    statusCode: 201, data: await _userService.Create(requestBody), msg: SuccessMessageResponse.CREATED_REQUEST
                ));
        }

        /// <summary>
        /// Endpoint for user edit user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestBody">An obj contains update info of a user</param>
        /// <returns>A user within status 200 or error status</returns>
        /// <response code="200">Returns user after update</response>
        /// <response code="404">Returns if the user is not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse<GetUserDetailModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> Update(Guid id, UpdateUserModel requestBody)
        {
            return Ok(new BaseResponse<GetUserDetailModel>(
                    data: await _userService.Update(id, requestBody), msg: SuccessMessageResponse.UPDATED_REQUEST
                ));
        }

        /// <summary>
        /// Endpoint for user delete a user
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>A user within status 200 or 404 status</returns>
        /// <response code="204">Returns 204 status if delete successfully</response>
        /// <response code="404">Returns if the user is not exist</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
