using System.Collections.Generic;
using System.Threading.Tasks;
using LeveInvestimentos.Domain.Entities;

namespace LeveInvestimentos.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(int id);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task AddAsync(AppUser user);
    Task UpdateAsync(AppUser user);
}
