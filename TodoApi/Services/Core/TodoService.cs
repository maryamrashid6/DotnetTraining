using System;
using System.Collections.Generic;
using TodoApi.Entities;
using static TodoApi.Services.Dtos.ToDoDto;
using TodoApi.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using static TodoApi.Services.Dtos.CommonDto;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ToDoContext _dbContext;

        //constructor
        public TodoService(ToDoContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AutoResponseDto<List<ToDo>> GetAll(PaginationDto requestDto)
        {
            // Logic to fetch all ToDo items from the database

            if (requestDto.PageSize != null && requestDto.PageNo != null)
            {
                var result = _dbContext.ToDos.Skip((requestDto.PageNo.Value - 1) * requestDto.PageSize.Value).Take(requestDto.PageSize.Value).ToList();
                foreach (var todo in result)
                {
                    todo.ParentToDo = _dbContext.ToDos.Find(todo.ParentToDoId);
                    todo.Category = _dbContext.Categories.Find(todo.CategoryId);
                }

                return new AutoResponseDto<List<ToDo>>
                {
                    Result = result
                };
            }
            else
            {
                var result = _dbContext.ToDos.ToList();
                foreach (var todo in result)
                {
                    todo.ParentToDo = _dbContext.ToDos.Find(todo.ParentToDoId);
                    todo.Category = _dbContext.Categories.Find(todo.CategoryId);
                }

                return new AutoResponseDto<List<ToDo>>
                {
                    Result = result
                };

            }

           

        }

        public AutoResponseDto<ToDoResponseDto> GetById(int id)
        {
            // Logic to fetch a ToDo item by its ID from the database
            var result = _dbContext.ToDos.Find(id);
  
            if (result == null)
            {
                return new AutoResponseDto<ToDoResponseDto>
                {
                    Success = false,
                    Message = "No record found"
                };
            }
            else
            {
                result.ParentToDo = _dbContext.ToDos.Find(result.ParentToDoId);
                result.Category = _dbContext.Categories.Find(result.CategoryId);
                // result users
                result.Users = _dbContext.Users.Where(x => x.ToDos.Contains(result)).ToList();
            }

            var users = new List<UserDto.UserNameEmailDto>();
            foreach (var user in result.Users)
            {
                users.Add(new UserDto.UserNameEmailDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Id = user.Id
                });
            }
            
            return new AutoResponseDto<ToDoResponseDto>
            {
                Result = new ToDoResponseDto
                {
                    Id = result.Id,
                    Title = result.Title,
                    Description = result.Description,
                    IsCompleted = result.IsCompleted,
                    Category = result.Category,
                    ParentToDo = result.ParentToDo,
                    Users = users
                }
            };
        }

        public AutoResponseDto<ToDo> Create(ToDoAddRequestDto requestDto)
        {
            // if CategoryId is not null, check if it exists in the database
            if (requestDto.CategoryId != null)
            {
                var category = _dbContext.Categories.Find(requestDto.CategoryId);
                if (category == null)
                {
                    return new AutoResponseDto<ToDo>
                    {
                        Success = false,
                        Message = "Category not found"
                    };
                }
            }

            // if ParentToDoId is not null, check if it exists in the database
            if (requestDto.ParentToDoId != null)
            {
                var parentToDo = _dbContext.ToDos.Find(requestDto.ParentToDoId);
                if (parentToDo == null)
                {
                    return new AutoResponseDto<ToDo>
                    {
                        Success = false,
                        Message = "Parent ToDo not found"
                    };
                }
            }

            // if ToDo item with same name and description already exists in the database, return an error
            var existingToDo = _dbContext.ToDos.FirstOrDefault(x => x.Title == requestDto.Title && x.Description == requestDto.Description);
            if (existingToDo != null)
            {
                return new AutoResponseDto<ToDo>
                {
                    Success = false,
                    Message = "ToDo item with same name and description already exists"
                };
            }
            

            // Logic to create a new ToDo item in the database
            var todo = new ToDo
            {
                Title = requestDto.Title,
                Description = requestDto.Description,
                CategoryId = requestDto.CategoryId,
                IsCompleted = requestDto.IsCompleted,
                ParentToDoId = requestDto.ParentToDoId
            };
            _dbContext.ToDos.Add(todo);
            _dbContext.SaveChanges();
            
            return new AutoResponseDto<ToDo>
            {
                Result = todo
            };
        }

        public AutoResponseDto<ToDo> Update(ToDoUpdateRequestDto requestDto)
        {
            // Logic to update a ToDo item in the database
            var todo = _dbContext.ToDos.Find(requestDto.Id);
            if (todo == null)
            {
                return new AutoResponseDto<ToDo>
                {
                    Success = false,
                    Message = "No record found"
                };
            }

            if (requestDto.CategoryId != null)
            {
                var category = _dbContext.Categories.Find(requestDto.CategoryId);
                if (category == null)
                {
                    return new AutoResponseDto<ToDo>
                    {
                        Success = false,
                        Message = "Category not found"
                    };
                }
            }

            if (requestDto.ParentToDoId != null)
            {
                var parentToDo = _dbContext.ToDos.Find(requestDto.ParentToDoId);
                if (parentToDo == null)
                {
                    return new AutoResponseDto<ToDo>
                    {
                        Success = false,
                        Message = "Parent ToDo not found"
                    };
                }
            }

            todo.Title = requestDto.Title;
            todo.Description = requestDto.Description;
            todo.CategoryId = requestDto.CategoryId;
            todo.IsCompleted = requestDto.IsCompleted;
            todo.ParentToDoId = requestDto.ParentToDoId;
            _dbContext.SaveChanges();
            
            return new AutoResponseDto<ToDo>
            {
                Result = todo
            };

        }

        public AutoResponseDto<List<ToDo>> Delete(int id)
        {
            // Logic to delete a ToDo item from the database
            var todo = _dbContext.ToDos.Find(id);
            if (todo == null)
            {
                return new AutoResponseDto<List<ToDo>>
                {
                    Success = false,
                    Message = "No record found"
                };
            }


            //if we are deleting a parent toDo (e.g a toDo with no parentToDoId), we should also delete all its child toDos
            if (todo.ParentToDoId == null)
            {
                var childToDos = _dbContext.ToDos.Where(x => x.ParentToDoId == todo.Id).ToList();
                foreach (var childToDo in childToDos)
                {
                    _dbContext.ToDos.Remove(childToDo);
                }
            }

            _dbContext.ToDos.Remove(todo);
            _dbContext.SaveChanges();

            return new AutoResponseDto<List<ToDo>>
            {
                Message = "Record deleted successfully",
                Result = _dbContext.ToDos.ToList()
            };

        }

        public AutoResponseDto<List<ToDo>> GetChildrenOfToDo(int id)
        {
            var result = _dbContext.ToDos.Where(x => x.ParentToDoId == id).ToList();
            foreach (var todo in result)
            {
                todo.ParentToDo = _dbContext.ToDos.Find(todo.ParentToDoId);
                todo.Category = _dbContext.Categories.Find(todo.CategoryId);
            }

            //if no children found
            if (result.Count == 0)
            {
                return new AutoResponseDto<List<ToDo>>
                {
                    Success = false,
                    Message = "No children found"
                };
            }

            return new AutoResponseDto<List<ToDo>>
            {
                Result = result
            };

        }
    }
}