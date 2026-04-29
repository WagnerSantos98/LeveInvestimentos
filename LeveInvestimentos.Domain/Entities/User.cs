using System;
using System.Collections.Generic;
using LeveInvestimentos.Domain.ValueObjects;
using LeveInvestimentos.Domain.Enums;

namespace LeveInvestimentos.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string LandlinePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string PhotoUrl { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        public ICollection<AppTask> CreatedTasks { get; set; }
        public ICollection<AppTask> AssignedTasks { get; set; }

        public User()
        {
            CreatedTasks = new List<AppTask>();
            AssignedTasks = new List<AppTask>();
        }
    }
}
