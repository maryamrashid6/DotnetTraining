

namespace TodoApi.Services.Dtos
{
    public class ToDoDto
    {
        public class ToDoAddRequestDto : CommonDto.BaseDto
        {
            public bool IsCompleted { get; set; }
            public int? CategoryId { get; set; }
            public int? ParentToDoId { get; set; }
        }

        public class ToDoUpdateRequestDto : ToDoAddRequestDto
        {
            public int Id { get; set; }
        }
        
    }
}
