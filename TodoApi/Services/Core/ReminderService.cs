using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoApi.Entities;
using TodoApi.Services.Contracts;

namespace TodoApi.Services.Core
{
    public class ReminderService : IReminderService
    {
        private readonly ToDoContext _dbContext;

        //constructor
        public ReminderService(ToDoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CheckReminders()
        {
            // Logic to check reminders
            var tomorrow = DateTime.UtcNow.Date.AddDays(1);
            var tasksWithReminders = _dbContext.ToDos
                .Where(t => t.IsReminderSet && t.ReminderDate.Value.Date == tomorrow && !t.IsCompleted)
                .Include(t => t.Users)
            .ToList();

            Console.WriteLine($"Running Check Reminders....");

            foreach (var task in tasksWithReminders)
            {
                var users = task.Users;
                

                foreach (var user in users)
                {
                    Console.WriteLine($"Reminder: {user.Name}, {user.Email} should get a reminder that '{task.Title}' is due tomorrow.");

                }


            }

        }
    }
}
