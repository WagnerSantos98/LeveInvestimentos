using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeveInvestimentos.Web.Pages.Tasks
{
    [Authorize(Policy = "ManagerOnly")]
    public class EditModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public EditModel(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        [BindProperty]
        public AppTask TaskItem { get; set; }

        public IEnumerable<SelectListItem> UserOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            TaskItem = await _taskService.GetByIdAsync(id);

            if (TaskItem == null)
            {
                return NotFound();
            }

            var users = await _userService.GetAllAsync();
            UserOptions = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.FullName} ({u.Email})"
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var users = await _userService.GetAllAsync();
                UserOptions = users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FullName} ({u.Email})"
                });
                return Page();
            }

            try
            {
                await _taskService.UpdateAsync(TaskItem);
                TempData["SuccessMessage"] = "Tarefa atualizada com sucesso!";
                return RedirectToPage("/Tasks/Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/Tasks/Index");
            }
        }
    }
}
