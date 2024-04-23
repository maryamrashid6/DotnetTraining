using static TodoApi.Services.Dtos.CategoryDto;
using TodoApi.Entities;
using TodoApi.Services.Dtos;

namespace TodoApi.Services.Contracts
{
    public interface ICategoryService
    {
        AutoResponseDto<List<Category>> GetAll();
        AutoResponseDto<CategoryResponseDto> GetById(int id);
        AutoResponseDto<Category> Create(CategoryAddRequestDto requestDto);
        AutoResponseDto<Category> Update(CategoryUpdateRequestDto requestDto);
        AutoResponseDto<List<Category>> Delete(int id);
    }
}
