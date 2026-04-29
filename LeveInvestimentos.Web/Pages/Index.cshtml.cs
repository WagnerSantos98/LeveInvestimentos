using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LeveInvestimentos.Application.Interfaces;
using LeveInvestimentos.Domain.Entities;
using LeveInvestimentos.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeveInvestimentos.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public IndexModel(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        public bool IsManager { get; set; }
        
        // Common stats
        public int TotalTasks { get; set; }
        public int PendingTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int OverdueTasks { get; set; }
        public IEnumerable<AppTask> RecentTasks { get; set; }
        public IEnumerable<AppTask> DueTodayAlerts { get; set; }
        public IEnumerable<AppTask> OverdueAlerts { get; set; }

        // Manager stats
        public int TotalUsers { get; set; }
        public int TotalTeamTasks { get; set; }
        public double CompletionPercentage { get; set; }
        public Dictionary<string, (int Total, int Completed)> UserPerformance { get; set; }
        
        // Chart data
        public int PendingChartData { get; set; }
        public int CompletedChartData { get; set; }

        public async Task OnGetAsync()
        {
            IsManager = User.HasClaim(ClaimTypes.Role, "Manager");
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (IsManager)
            {
                var users = await _userService.GetAllAsync();
                var tasks = await _taskService.GetAllAsync();

                TotalUsers = users.Count();
                TotalTeamTasks = tasks.Count();
                CompletedChartData = tasks.Count(t => t.Status == AppTaskStatus.Completed);
                PendingChartData = tasks.Count(t => t.Status == AppTaskStatus.Pending);
                
                CompletionPercentage = TotalTeamTasks == 0 ? 0 : Math.Round((double)CompletedChartData / TotalTeamTasks * 100, 2);

                UserPerformance = new Dictionary<string, (int, int)>();
                foreach (var user in users)
                {
                    var userTasks = tasks.Where(t => t.AssigneeId == user.Id);
                    UserPerformance[user.FullName] = (userTasks.Count(), userTasks.Count(t => t.Status == AppTaskStatus.Completed));
                }
            }
            else
            {
                var tasks = await _taskService.GetAllByAssigneeAsync(userId);
                
                TotalTasks = tasks.Count();
                PendingTasks = tasks.Count(t => t.Status == AppTaskStatus.Pending);
                CompletedTasks = tasks.Count(t => t.Status == AppTaskStatus.Completed);
                
                var now = DateTime.UtcNow.Date;
                OverdueTasks = tasks.Count(t => t.Status == AppTaskStatus.Pending && t.DueDate.Date < now);
                
                RecentTasks = tasks.OrderByDescending(t => t.CreatedAt).Take(5);
                DueTodayAlerts = tasks.Where(t => t.Status == AppTaskStatus.Pending && t.DueDate.Date == now);
                OverdueAlerts = tasks.Where(t => t.Status == AppTaskStatus.Pending && t.DueDate.Date < now);
            }
        }
    }
}
