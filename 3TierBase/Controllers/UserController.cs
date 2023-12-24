using _3TierBase.Business.Commons.Paging;
using _3TierBase.Business.Enum;
using _3TierBase.Business.Services.UserServices;
using _3TierBase.Business.ViewModels;
using _3TierBase.Business.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
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
        /// <param name="searchUserModel"></param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of user</returns>
        /// <response code="200">Returns the list of user</response>
        /// <response code="204">Returns if list of user is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModelsResponse<Task<(IList<GetUserDetailModel>, int)>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] PagingParam<UserEnum.UserSort> paginationModel,
            [FromQuery] SearchUserModel searchUserModel)
        {
            var (users, total) = await _userService.GetAll(paginationModel, searchUserModel);

            return Ok(new ModelsResponse<GetUserDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Send Request Successful",
                Paging = new PagingMetadata()
                {
                    Page = paginationModel.PageIndex,
                    Size = paginationModel.PageSize ?? total,
                    Total = total
                },
                Data = users,
            });
        }

        /// <summary>
        /// Endpoint for get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of user</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="204">Returns if the user is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<GetUserDetailModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(new BaseResponse<GetUserDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Data = await _userService.GetById(id),
                Msg = "Send Request Successful"
            });
        }

        /// <summary>
        /// Endpoint for create user
        /// </summary>
        /// <param name="requestBody">An obj contains input info of a user.</param>
        /// <returns>A user within status 201 or error status.</returns>
        /// <response code="201">Returns the user</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<GetUserDetailModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUserModel requestBody)
        {
            return Created(string.Empty, new BaseResponse<GetUserDetailModel>()
            {
                Code = StatusCodes.Status201Created,
                Data = await _userService.Create(requestBody),
                Msg = "Created Successful"
            });
        }

        /// <summary>
        /// Endpoint for user edit user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestBody">An obj contains update info of a user.</param>
        /// <returns>A user within status 200 or error status.</returns>
        /// <response code="200">Returns user after update</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse<GetUserDetailModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateuserAsync(Guid id, [FromBody] UpdateUserModel requestBody)
        {
            return Ok(new BaseResponse<GetUserDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Data = await _userService.Update(id, requestBody),
                Msg = "Updated Successful"
            });
        }

        /// <summary>
        /// Endpoint for user Delete a user.
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>A user within status 200 or 204 status.</returns>
        /// <response code="200">Returns 200 status</response>
        /// <response code="204">Returns NoContent status</response>
        [HttpDelete("{id}")]
        // [Authorize(users = usersConstants.user)]
        public async Task<IActionResult> DeleteClassAsync(Guid id)
        {
            await _userService.Delete(id);
            return Ok(new BaseResponse<GetUserDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Data = null,
                Msg = $"Deleted id: {id} Successful"
            });
        }
    }
}
