using AutoMapper;
using ELCLite.Identity.Api.Data.Contracts;
using ELCLite.Identity.Api.Data.Entities;

namespace ELCLite.Identity.Api.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            if (mapper is null)
            {
                throw new ArgumentException($"{nameof(mapper)} is null");
            }
            if (userRepository is null)
            {
                throw new ArgumentException($"{nameof(userRepository)} is null");
            }
            if (roleRepository is null)
            {
                throw new ArgumentException($"{nameof(roleRepository)} is null");
            }

            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} is required");
            }

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user is null)
            {
                throw new ApplicationException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"{nameof(email)} is required");
            }

            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (user is null)
            {
                throw new ApplicationException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetListAsync(cancellationToken);

            var data = _mapper.Map<List<UserDto>>(users);

            return data;
        }

        public async Task<UserDto> AddAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            if (createUserDto is null)
            {
                throw new ArgumentException($"{nameof(createUserDto)} is required");
            }

            var newUser = _mapper.Map<User>(createUserDto);

            newUser.AssignToRoles(createUserDto.RoleIds);

            await _userRepository.AddAsync(newUser, cancellationToken);

            // Load role info for the response.
            _ = await _roleRepository.GetListByIdAsync(newUser.UserRoles.Select(x => x.RoleId), cancellationToken);

            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<UserDto> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            if (updateUserDto is null)
            {
                throw new ArgumentException($"{nameof(updateUserDto)} is required");
            }

            if (updateUserDto.Id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(updateUserDto.Id)} is required");
            }

            var user = await _userRepository.GetByIdAsync(updateUserDto.Id, cancellationToken);

            if (user is null)
            {
                throw new ApplicationException("User not found");
            }

            _mapper.Map<UpdateUserDto, User>(updateUserDto, user);

            user.UpdateRoles(updateUserDto.RoleIds);

            await _userRepository.UpdateAsync(user, cancellationToken);

            // Load role info for the response.
            _ = await _roleRepository.GetListByIdAsync(user.UserRoles.Select(x => x.RoleId), cancellationToken);

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} is required");
            }

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user is null)
            {
                throw new ApplicationException("User not found");
            }

            await _userRepository.DeleteAsync(user, cancellationToken);
        }

        public async Task<bool> TryUpdateUserInfoByEmailAsync(string email, string firstname, string lastname, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (user is null) return false;

            user.Update(firstname, lastname, user.PreferredLanguageISOCode, user.IsStaff);

            await _userRepository.UpdateAsync(user, cancellationToken);

            return true;
        }

        public async Task<UserDto> TryGetStaffUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetStaffByEmailAsync(email, cancellationToken);

            if (user is null) return null;

            return _mapper.Map<UserDto>(user);
        }
    }
}
