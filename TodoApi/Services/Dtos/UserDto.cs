using TodoApi.Entities;
using static TodoApi.Services.Dtos.ToDoDto;

namespace TodoApi.Services.Dtos
{
    public class UserDto
    {
        public class UserAddRequestDto 
        {
            public required string Name { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }

        }

        public class UserUpdateRequestDto : UserAddRequestDto
        {
            public int Id { get; set; }
        }

        //Hide Password
        public class UserResponseDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public List<ToDoUpdateRequestDto>? ToDos { get; set; }
        }

        //User Response Dto without ToDos
        public class UserNameEmailDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}



