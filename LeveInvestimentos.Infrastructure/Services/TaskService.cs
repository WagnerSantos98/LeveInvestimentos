using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using LeveInvestimentos.Domain.Enums;
using LeveInvestimentos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeveInvestimentos.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public TaskService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<AppTask> CreateAsync(AppTask task)
        {
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;
            task.Status = AppTaskStatus.Pending;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Notify Assignee
            var assignee = await _context.Users.FindAsync(task.AssigneeId);
            if (assignee != null)
            {
                await _emailService.SendEmailAsync(assignee.Email, "Nova Tarefa Atribuída", $"Você recebeu uma nova tarefa: {task.Description}. Prazo: {task.DueDate:dd/MM/yyyy}");
            }

            return task;
        }

        public async Task<IEnumerable<AppTask>> GetAllByAssigneeAsync(Guid assigneeId)
        {
            return await _context.Tasks
                .Include(t => t.Creator)
                .Where(t => t.AssigneeId == assigneeId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<AppTask>> GetAllByCreatorAsync(Guid creatorId)
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.CreatorId == creatorId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<AppTask> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Creator)
                .Include(t => t.Assignee)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task MarkAsCompletedAsync(Guid taskId, Guid userId)
        {
            var task = await _context.Tasks.Include(t => t.Creator).FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null) return;

            // Only assignee can complete it (or manager, but let's say assignee)
            if (task.AssigneeId != userId) throw new UnauthorizedAccessException("Somente o responsável pode concluir a tarefa.");

            task.Status = AppTaskStatus.Completed;
            await _context.SaveChangesAsync();

            // Notify Creator
            if (task.Creator != null)
            {
                await _emailService.SendEmailAsync(task.Creator.Email, "Tarefa Concluída", $"A tarefa '{task.Description}' foi concluída.");
            }
        }
    }
}
