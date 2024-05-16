using Microsoft.AspNetCore.Mvc;
using static TodoApi.Services.Dtos.CommonDto;
using static TodoApi.Services.Dtos.UserDto;
using TodoApi.Services;
using TodoApi.Entities;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PaginationDto requestDto)
        {
            var users = _userService.GetAll(requestDto);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.Get(id);
            return Ok(user);
        }

        [HttpPost("{userId}/todos")]
        public IActionResult AssignToDos(int userId, [FromBody] List<int> toDoIds)
        {
            var user = _userService.AssignToDos(userId, toDoIds);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserAddRequestDto user)
        {
            var result = _userService.Add(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_userService.Delete(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] UserUpdateRequestDto user)
        {
            return Ok(_userService.Update(user));
        }
    }
}
