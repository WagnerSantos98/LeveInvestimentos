using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeveInvestimentos.Domain.Entities;

namespace LeveInvestimentos.Application.Interfaces
{
    public interface ITaskService
    {
        Task<AppTask> GetByIdAsync(Guid id);
        Task<IEnumerable<AppTask>> GetAllByCreatorAsync(Guid creatorId);
        Task<IEnumerable<AppTask>> GetAllByAssigneeAsync(Guid assigneeId);
        Task<IEnumerable<AppTask>> GetAllAsync();
        Task<AppTask> CreateAsync(AppTask task);
        Task MarkAsCompletedAsync(Guid taskId, Guid userId);
        Task ReopenAsync(Guid taskId, Guid userId);
        Task DeleteAsync(Guid taskId, Guid userId);
    }
}
