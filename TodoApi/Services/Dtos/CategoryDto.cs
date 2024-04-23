using TodoApi.Entities;

namespace TodoApi.Services.Dtos
{
    public class CategoryDto
    {
        public class CategoryAddRequestDto : CommonDto.BaseDto
        {

        }

        public class CategoryUpdateRequestDto : CategoryAddRequestDto
        {
            public int Id { get; set; }
        }

        public class CategoryResponseDto : CategoryUpdateRequestDto
        {
            public List<ToDo>? ToDos { get; set; }

        }
    }
}
