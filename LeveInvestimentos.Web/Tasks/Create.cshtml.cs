using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeveInvest.Web.Pages.Tasks
{
    [Authorize(Policy = "ManagerOnly")]
    public class CreateModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public CreateModel(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IEnumerable<User> Users { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "A descrição é obrigatória.")]
            public string Description { get; set; }

            [Required(ErrorMessage = "A data limite é obrigatória.")]
            public DateTime DueDate { get; set; }

            [Required(ErrorMessage = "Selecione um usuário para atribuir a tarefa.")]
            public Guid AssigneeId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new InputModel { DueDate = DateTime.Today.AddDays(1) };
            Users = await _userService.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Users = await _userService.GetAllAsync();
                return Page();
            }

            var creatorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var task = new AppTask
            {
                Description = Input.Description,
                DueDate = Input.DueDate,
                AssigneeId = Input.AssigneeId,
                CreatorId = creatorId
            };

            await _taskService.CreateAsync(task);

            TempData["SuccessMessage"] = "Tarefa atribuída com sucesso!";
            return RedirectToPage("/Tasks/Index");
        }
    }
}
