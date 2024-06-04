using TodoApi.Entities;
using static TodoApi.Services.Dtos.UserDto;

namespace TodoApi.Services.Dtos
{
    public class ToDoDto
    {
        public class ToDoAddRequestDto : CommonDto.BaseDto
        {
            public bool IsCompleted { get; set; }
            public int? CategoryId { get; set; }
            public int? ParentToDoId { get; set; }

            public bool IsReminderSet { get; set; }
            public DateTime? ReminderDate { get; set; }
        }

        public class ToDoUpdateRequestDto : ToDoAddRequestDto
        {
            public int Id { get; set; }
        }

        public class ToDoResponseDto : CommonDto.BaseDto
        {
            public int Id { get; set; }
            public bool IsCompleted { get; set; }
            public Category? Category { get; set; }
            public ToDo? ParentToDo { get; set; }
            public List<UserNameEmailDto>? Users { get; set; }
            public DateTime? ReminderDate { get; set; }
            public bool IsReminderSet { get; set; }
        }
        
    }
}
