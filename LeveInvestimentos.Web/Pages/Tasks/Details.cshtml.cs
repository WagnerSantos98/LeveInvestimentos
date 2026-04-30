using System;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeveInvestimentos.Web.Pages.Tasks
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ITaskService _taskService;

        public DetailsModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public AppTask TaskItem { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            TaskItem = await _taskService.GetByIdAsync(id);

            if (TaskItem == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}