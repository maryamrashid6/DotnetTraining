using FluentValidation;
using TodoApi.Entities;
using static TodoApi.Services.Dtos.UserDto;
using FluentValidation;

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

        public class ToDoValidator : AbstractValidator<ToDoAddRequestDto>
        {
            public ToDoValidator()
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
                RuleFor(x => x.Title).MaximumLength(50).WithMessage("Title cannot be more than 50 characters");
                RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description cannot be more than 500 characters");
                RuleFor(x => x.ReminderDate).GreaterThan(DateTime.Now).WithMessage("Reminder date cannot be in the past");
            }
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
