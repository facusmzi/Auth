using Auth.Dtos.User;

namespace Auth.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);
        Task<bool> UpdateAsync(Guid userId, UpdateUserDto updateUserDto);
        Task<bool> UpdatePasswordAsync(Guid userId, UpdatePasswordDto updatePasswordDto);
    }
}
