using Microsoft.AspNetCore.Mvc;
using TodoApi.Entities;
using TodoApi.Services;
using static TodoApi.Services.Dtos.ToDoDto;
using static TodoApi.Services.Dtos.CommonDto;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public ToDoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PaginationDto requestDto)
        {
            var todos = _todoService.GetAll(requestDto);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todo = _todoService.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ToDoAddRequestDto requestDto)
        {
           
            return Ok(_todoService.Create(requestDto));
        }

        [HttpPut("{id}")]
        public IActionResult Put(ToDoUpdateRequestDto requestDto)
        {     
            return Ok(_todoService.Update(requestDto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_todoService.Delete(id));
        }

        [HttpGet("GetChildrenOfToDo/{id}")]
        public IActionResult GetChildrenOfToDo (int id)
        {
            return Ok(_todoService.GetChildrenOfToDo(id));
        }
    }
}
