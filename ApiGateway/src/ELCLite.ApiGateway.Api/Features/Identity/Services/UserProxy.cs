using AutoMapper;
using ELCLite.ApiGateway.Api.Features.Identity.Contracts;
using ELCLite.ApiGateway.Api.Features.Identity.Models;
using ELCLite.Identity.Features.Users;

namespace ELCLite.ApiGateway.Api.Features.Identity.Services
{
    public class UserProxy : IUserProxy
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserProxy(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var users = await _userService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<UserModel>(users);
        }

        public async Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var users = await _userService.GetByEmailAsync(email, cancellationToken);

            return _mapper.Map<UserModel>(users);
        }

        public async Task<List<UserModel>> GetAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetListAsync(cancellationToken);

            var data = _mapper.Map<List<UserModel>>(users);

            return data;
        }

        public async Task<UserModel> AddAsync(CreateUserModel createUserModel, CancellationToken cancellationToken)
        {
            var createUserDto = _mapper.Map<CreateUserDto>(createUserModel);
            var newUser = await _userService.AddAsync(createUserDto, cancellationToken);

            return _mapper.Map<UserModel>(newUser);
        }

        public async Task<UserModel> UpdateAsync(UpdateUserModel updateUserModel, CancellationToken cancellationToken)
        {
            var updateUserDto = _mapper.Map<UpdateUserDto>(updateUserModel);
            var user = await _userService.UpdateAsync(updateUserDto, cancellationToken);

            return _mapper.Map<UserModel>(user);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _userService.DeleteByIdAsync(id, cancellationToken);
        }
    }
}
