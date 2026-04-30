using System.Collections.Generic;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeveInvestimentos.Web.Pages.Users
{
    [Authorize(Policy = "ManagerOnly")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        
        public IEnumerable<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _userService.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> onPostDeleteAsync(System.Guid id)
        {
            if(!User.HasClaim(System.Security.Claims.ClaimTypes.Role, "Manager"))
            {
                TempData["ErrorMessage"] = "Você não tem permissão para excluir usuários.";
                return RedirectToPage("/Users/Index");
            }
            try
            {
                await _userService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Usuário excluído com sucesso!";
            }
            catch(System.Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("/Users/Index");
        }
    }
}
