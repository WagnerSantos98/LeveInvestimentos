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
    }
}
