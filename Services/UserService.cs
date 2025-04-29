using Auth.Dtos.User;
using Auth.Interfaces;
using Auth.Models;
using AutoMapper;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IPasswordService passwordService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            if (await _userRepository.ExistsByEmailAsync(createUserDto.Email))
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            var user = _mapper.Map<User>(createUserDto);
            user.PasswordHash = _passwordService.HashPassword(createUserDto.Password);

            await _userRepository.CreateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateAsync(Guid userId, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Email) &&
                updateUserDto.Email.ToLower() != user.Email.ToLower())
            {
                if (await _userRepository.ExistsByEmailAsync(updateUserDto.Email))
                {
                    throw new InvalidOperationException("Email is already in use.");
                }

                user.Email = updateUserDto.Email;
            }

            if (!string.IsNullOrEmpty(updateUserDto.DisplayName))
            {
                user.DisplayName = updateUserDto.DisplayName;
            }

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, UpdatePasswordDto updatePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            if (!_passwordService.VerifyPassword(updatePasswordDto.OldPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("Incorrect old password.");
            }

            user.PasswordHash = _passwordService.HashPassword(updatePasswordDto.NewPassword);

            return await _userRepository.UpdateAsync(user);
        }
    }
}
