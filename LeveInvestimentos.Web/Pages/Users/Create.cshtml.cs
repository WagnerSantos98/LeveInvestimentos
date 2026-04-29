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

            [Required]
            public string ZipCode { get; set; }

            [Required]
            public string Street { get; set; }

            [Required]
            public string Number { get; set; }

            public string Neighborhood { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string PhotoUrl { get; set; }

            [Required(ErrorMessage = "Senha é obrigatória")]
            public string Password { get; set; }

            public LeveInvestimentos.Domain.Enums.UserRole Role { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var dto = new LeveInvestimentos.Application.DTOs.CreateUserDto
            {
                FullName = Input.FullName,
                Email = Input.Email,
                BirthDate = Input.BirthDate,
                LandlinePhone = Input.LandlinePhone,
                MobilePhone = Input.MobilePhone,
                Address = new LeveInvestimentos.Domain.ValueObjects.Address(
                    Input.ZipCode, Input.Street, Input.Number, Input.Neighborhood, Input.City, Input.State),
                PhotoUrl = Input.PhotoUrl,
                Password = Input.Password,
                Role = Input.Role
            };

            await _userService.CreateAsync(dto);

            TempData["SuccessMessage"] = "Usuário criado com sucesso!";
            return RedirectToPage("/Users/Index");
        }
    }
}
