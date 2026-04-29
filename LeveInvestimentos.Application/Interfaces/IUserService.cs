using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeveInvestimentos.Domain.Entities;
using LeveInvestimentos.Application.DTOs;

namespace LeveInvestimentos.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(CreateUserDto dto);
        Task UpdateAsync(User user);
    }
}
