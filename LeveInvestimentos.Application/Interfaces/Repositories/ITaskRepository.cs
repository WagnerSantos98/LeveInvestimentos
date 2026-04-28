using System.Collections.Generic;
using System.Threading.Tasks;
using LeveInvestimentos.Domain.Entities;

namespace LeveInvestimentos.Application.Interfaces.Repositories;

public interface ITaskRepository
{
    Task<AppTask?> GetByIdAsync(int id);
    Task<IEnumerable<AppTask>> GetAllByCreatedByIdAsync(int createdById);
    Task<IEnumerable<AppTask>> GetAllByAssignedToIdAsync(int assignedToId);
    Task AddAsync(AppTask task);
    Task UpdateAsync(AppTask task);
}
