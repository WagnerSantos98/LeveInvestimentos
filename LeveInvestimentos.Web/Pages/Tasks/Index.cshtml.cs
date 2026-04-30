using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeveInvestimentos.Web.Pages.Tasks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;

        public IndexModel(ITaskService taskService)
        {
            _taskService = taskService;
            AssignedTasks = new List<AppTask>();
            CreatedTasks = new List<AppTask>();
        }

        public IEnumerable<AppTask> AssignedTasks { get; set; }
        public IEnumerable<AppTask> CreatedTasks { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            AssignedTasks = await _taskService.GetAllByAssigneeAsync(userId);

            if (User.HasClaim(ClaimTypes.Role, "Manager"))
            {
                CreatedTasks = await _taskService.GetAllByCreatorAsync(userId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCompleteAsync(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                await _taskService.MarkAsCompletedAsync(id, userId);
                TempData["SuccessMessage"] = "Tarefa concluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToPage("/Tasks/Index");
        }

        // Reabir tarefa
        public async Task<IActionResult> OnPostReopenAsync(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                await _taskService.ReopenAsync(id, userId);
                TempData["SuccessMessage"] = "Tarefa reaberta com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToPage("/Tasks/Index");
        }

        // Excluir tarefa
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Segurança extra de confirmação
            if (!User.HasClaim(ClaimTypes.Role, "Manager"))
            {
                TempData["ErrorMessage"] = "Você não tem permissão para excluir tarefas.";
                return RedirectToPage("/Tasks/Index");
            }

            try
            {
                await _taskService.DeleteAsync(id, userId);
                TempData["SuccessMessage"] = "Tarefa excluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToPage("/Tasks/Index");
        }
    }
}
