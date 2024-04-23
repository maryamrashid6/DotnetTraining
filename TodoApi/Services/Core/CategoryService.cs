using static TodoApi.Services.Dtos.CategoryDto;
using TodoApi.Entities;
using TodoApi.Services.Dtos;
using TodoApi.Services.Contracts;

namespace TodoApi.Services.Core
{
    public class CategoryService : ICategoryService
    {
        private readonly ToDoContext _dbContext;

        //constructor
        public CategoryService(ToDoContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AutoResponseDto<List<Category>> GetAll()
        {
            // Logic to fetch all Category items from the database

            var result = _dbContext.Categories.ToList();

            return new AutoResponseDto<List<Category>>
            {
                Result = result
            };

        }

        public AutoResponseDto<CategoryResponseDto> GetById(int id)
        {
            // Logic to fetch a Category item by its ID from the database
            var result = _dbContext.Categories.Find(id);

            if (result == null)
            {
                return new AutoResponseDto<CategoryResponseDto>
                {
                    Success = false,
                    Message = "No record found"
                };
            }
            

            return new AutoResponseDto<CategoryResponseDto>
            {
                Result = new CategoryResponseDto
                {
                    Id = result.Id,
                    Title = result.Title,
                    Description = result.Description,
                    ToDos = _dbContext.ToDos.Where(x => x.CategoryId == result.Id).ToList()
                }
            };
        }

        public AutoResponseDto<Category> Create(CategoryAddRequestDto requestDto)
        {
            
            // if category item with same name and description already exists in the database, return an error
            var existingCategory = _dbContext.Categories.FirstOrDefault(x => x.Title == requestDto.Title && x.Description == requestDto.Description);
            if (existingCategory != null)
            {
                return new AutoResponseDto<Category>
                {
                    Success = false,
                    Message = "Category with same name and description already exists"
                };
            }


            // Logic to create a new Category item in the database
            var item = new Category
            {
                Title = requestDto.Title,
                Description = requestDto.Description
            };
            
            _dbContext.Categories.Add(item);
            _dbContext.SaveChanges();

            return new AutoResponseDto<Category>
            {
                Result = item
            };
        }

        public AutoResponseDto<Category> Update(CategoryUpdateRequestDto requestDto)
        {
            // Logic to update a category item in the database
            var item = _dbContext.Categories.Find(requestDto.Id);
            if (item == null)
            {
                return new AutoResponseDto<Category>
                {
                    Success = false,
                    Message = "No record found"
                };
            }

            

            item.Title = requestDto.Title;
            item.Description = requestDto.Description;

            _dbContext.SaveChanges();

            return new AutoResponseDto<Category>
            {
                Result = item
            };

        }

        public AutoResponseDto<List<Category>> Delete(int id)
        {
            // Logic to delete a category item from the database
            var item = _dbContext.Categories.Find(id);
            if (item == null)
            {
                return new AutoResponseDto<List<Category>>
                {
                    Success = false,
                    Message = "No record found"
                };
            }


            _dbContext.Categories.Remove(item);
            _dbContext.SaveChanges();

            return new AutoResponseDto<List<Category>>
            {
                Message = "Record deleted successfully",
                Result = _dbContext.Categories.ToList()
            };

        }
    }
}
