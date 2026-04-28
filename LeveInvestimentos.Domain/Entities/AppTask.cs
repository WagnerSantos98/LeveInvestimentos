using System;
using LeveInvestimentos.Domain.Enums;

namespace LeveInvestimentos.Domain.Entities
{
    public class AppTask
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public AppTaskStatus Status { get; set; }
        
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }

        public Guid AssigneeId { get; set; }
        public User Assignee { get; set; }

        public DateTime CreatedAt { get; set; }

        public AppTask()
        {
            CreatedAt = DateTime.UtcNow;
            Status = AppTaskStatus.Pending;
        }
    }
}
