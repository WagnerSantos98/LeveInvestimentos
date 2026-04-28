using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeveInvestimentos.Web.Pages.Users
{
    [Authorize(Policy = "ManagerOnly")]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Nome é obrigatório")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "E-mail é obrigatório")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public DateTime BirthDate { get; set; }

            public string LandlinePhone { get; set; }

            [Required(ErrorMessage = "Celular é obrigatório")]
            public string MobilePhone { get; set; }

            [Required(ErrorMessage = "Endereço é obrigatório")]
            public string Address { get; set; }

            public string PhotoUrl { get; set; }

            [Required(ErrorMessage = "Senha é obrigatória")]
            public string Password { get; set; }

            public bool IsManager { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = new User
            {
                FullName = Input.FullName,
                Email = Input.Email,
                BirthDate = Input.BirthDate,
                LandlinePhone = Input.LandlinePhone,
                MobilePhone = Input.MobilePhone,
                Address = Input.Address,
                PhotoUrl = Input.PhotoUrl,
                IsManager = Input.IsManager
            };

            await _userService.CreateAsync(user, Input.Password);

            TempData["SuccessMessage"] = "Usuário criado com sucesso!";
            return RedirectToPage("/Users/Index");
        }
    }
}
