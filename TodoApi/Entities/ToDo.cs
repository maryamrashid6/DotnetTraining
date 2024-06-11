using TodoApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Entities
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        // Foreign key property for Category
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // Foreign key property for Parent Task
        public int? ParentToDoId { get; set; }
        public ToDo? ParentToDo { get; set; }

        //Many to many relationship with Users
        //A Task can be assigned to multiple users
        public ICollection<User>? Users { get; set; }

        public DateTime? ReminderDate { get; set; }
        public bool IsReminderSet { get; set; }


        public ToDo()
        {
            Users = new List<User>();
        }

    }


}


