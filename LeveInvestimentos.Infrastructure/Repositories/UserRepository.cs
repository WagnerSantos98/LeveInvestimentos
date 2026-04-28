using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Domain.Enities;
using LeveInvestimentos.Application.Interfaces.Repositories;
using LeveInvestimentos.Infrastructure.Data;

namespace LeveInvestimentos.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LeveInvestimentosDbContext _context;
    public UserRepository(LeveInvestimentosDbContext context)
    {
       _context = context; 
    }

    public async Task<AppUser?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(AppUser user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AppUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}