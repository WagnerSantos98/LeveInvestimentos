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

namespace LeveInvest.Web.Pages.Tasks
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

            if (User.HasClaim("IsManager", "True"))
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
    }
}
