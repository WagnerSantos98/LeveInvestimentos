using System;
using System.Collections.Generic;

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
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public string PasswordHash { get; set; }
        public bool IsManager { get; set; }

        public ICollection<AppTask> CreatedTasks { get; set; }
        public ICollection<AppTask> AssignedTasks { get; set; }

        public User()
        {
            CreatedTasks = new List<AppTask>();
            AssignedTasks = new List<AppTask>();
        }
    }
}
