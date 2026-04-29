using System;
using LeveInvestimentos.Domain.Enums;
using LeveInvestimentos.Domain.ValueObjects;

namespace LeveInvestimentos.Application.DTOs
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string LandlinePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string PhotoUrl { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
