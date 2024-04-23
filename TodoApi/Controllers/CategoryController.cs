using Microsoft.AspNetCore.Mvc;
using static TodoApi.Services.Dtos.CategoryDto;
using TodoApi.Services;
using TodoApi.Services.Contracts;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryAddRequestDto requestDto)
        {

            return Ok(_categoryService.Create(requestDto));
        }

        [HttpPut("{id}")]
        public IActionResult Put(CategoryUpdateRequestDto requestDto)
        {
            return Ok(_categoryService.Update(requestDto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_categoryService.Delete(id));
        }
    }
}
