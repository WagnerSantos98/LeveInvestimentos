using System;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeveInvestimentos.Web.Pages.Users
{
    [Authorize(Policy = "ManagerOnly")]
    public class EditModel : PageModel
    {
        public readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User UserItem {get; set;}

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            UserItem = await _userService.GetByIdAsync(id);
            if(UserItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Remover erros de validação para campos não incluídos no formulário
            ModelState.Remove("UserItem.TasksCreated");
            ModelState.Remove("UserItem.TasksAssigned");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var existingUser = await _userService.GetByIdAsync(UserItem.Id);
                if(existingUser == null) return NotFound();

                // Atualiza apenas as propriedades necessárias para evitar sobrescrever o hash da senha inesperadamente.
                existingUser.FullName = UserItem.FullName;
                existingUser.Email = UserItem.Email;
                existingUser.MobilePhone = UserItem.MobilePhone;
                existingUser.LandlinePhone = UserItem.LandlinePhone;
                existingUser.Address = UserItem.Address;
                existingUser.Role = UserItem.Role;

                await _userService.UpdateAsync(existingUser);
                TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
                return RedirectToPage("/Users/Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/Users/Index");
            }
        }
    }
}