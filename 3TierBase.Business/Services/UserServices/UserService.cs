using _3Tier.Business.Common;
using _3TierBase.Business.Commons;
using _3TierBase.Business.Commons.Paging;
using _3TierBase.Business.Enum;
using _3TierBase.Business.Utilities;
using _3TierBase.Business.Utilities.ErrorHandling;
using _3TierBase.Business.ViewModels.Users;
using _3TierBase.Data.Entities;
using _3TierBase.Data.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace _3TierBase.Business.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _jwtHelper;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtHelper jwtHelper, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _mapper = mapper;
        }

        public async Task<GetUserDetailModel> Create(CreateUserModel requestBody)
        {
            User user = _mapper.Map<User>(requestBody);

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHashed;
            user.CreatedAt = DateTime.Now;

            await _userRepository.InsertAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<GetUserDetailModel>(user);
        }

        public async Task Delete(Guid id)
        {
            User user = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("Id is not exist");
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
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<GetUserDetailModel>(user);
        }

        public async Task<string> Login(UserLoginModel requestBody)
        {
            User user = await _userRepository.GetFirstOrDefaultAsync(u => requestBody.Username == u.Username)
                ?? throw new CException()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Username or password not correct"
                };

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(requestBody.Password, user.Password);
            if (!isValidPassword)
            {
                throw new CException()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Username or password not correct"
                };
            }

            return _jwtHelper.GenerateJwtToken(user.Username, "User", user.Id);
        }

        public async Task<GetUserDetailModel> Update(Guid id, UpdateUserModel requestBody)
        {
            if (id != requestBody.Id)
            {
                throw new Exception("Id not match");
            }
            User user = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User is not exist");
            _mapper.Map(requestBody, user);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<GetUserDetailModel>(user);
        }
    }
}
