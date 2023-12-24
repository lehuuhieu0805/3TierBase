using _3Tier.Business.Common;
using _3TierBase.Business.Commons;
using _3TierBase.Business.Commons.Paging;
using _3TierBase.Business.Enum;
using _3TierBase.Business.ViewModels.Users;
using _3TierBase.Data.Entities;
using _3TierBase.Data.Repositories.UserRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _3TierBase.Business.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserDetailModel> Create(CreateUserModel requestBody)
        {
            User user = _mapper.Map<User>(requestBody);

            await _userRepository.InsertAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<GetUserDetailModel>(user);
        }

        public async Task Delete(Guid id)
        {
            User user = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("Username not exist");
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<(IList<GetUserDetailModel>, int)> GetAll(PagingParam<UserEnum.UserSort> paginationModel, SearchUserModel searchUserModel)
        {
            IQueryable<User> query = _userRepository.GetAll();
            query = query.GetWithSearch(searchUserModel);
            // Apply sort
            query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);
            var total = await query.CountAsync();
            // Apply Paging
            query = query.GetWithPaging(paginationModel.PageIndex, paginationModel.PageSize).AsQueryable();
            var result = _mapper.ProjectTo<GetUserDetailModel>(query);
            return (result.ToList(), total);
        }

        public async Task<GetUserDetailModel> GetById(Guid id)
        {
            var admin = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<GetUserDetailModel>(admin);
        }

        public async Task<GetUserDetailModel> Update(Guid id, UpdateUserModel requestBody)
        {
            if (id != requestBody.Id)
            {
                throw new Exception("Id not match");
            }
            User user = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("User not exist");
            }
            _mapper.Map(requestBody, user);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<GetUserDetailModel>(user);
        }
    }
}
