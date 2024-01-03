using _3TierBase.Business.Commons.Paging;
using _3TierBase.Business.Enum;
using _3TierBase.Business.ViewModels;
using _3TierBase.Business.ViewModels.Users;

namespace _3TierBase.Business.Services.UserServices
{
    public interface IUserService
    {
        public Task<string> Login(UserLoginModel requestBody);
        public Task<(IList<GetUserDetailModel>, int)> GetAll(PagingParam<UserEnum.UserSort> paginationModel, SearchUserModel searchUserModel);
        public Task<GetUserDetailModel> GetById(Guid id);
        public Task<GetUserDetailModel> Create(CreateUserModel requestBody);
        public Task<GetUserDetailModel> Update(Guid id, UpdateUserModel requestBody);
        public Task Delete(Guid id);
    }
}
