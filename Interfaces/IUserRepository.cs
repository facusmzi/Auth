﻿using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
