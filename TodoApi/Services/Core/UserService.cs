using System;
using System.Collections.Generic;
using TodoApi.Entities;
using static TodoApi.Services.Dtos.UserDto;
using TodoApi.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using static TodoApi.Services.Dtos.CommonDto;

namespace TodoApi.Services
{
    public class UserService: IUserService
    {
        private readonly ToDoContext _dbContext;

        //constructor
        public UserService(ToDoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AutoResponseDto<List<UserResponseDto>> GetAll(PaginationDto requestDto)
        {
            // Logic to fetch all User items from the database

            if (requestDto.PageSize != null && requestDto.PageNo != null)
            {
                var result = _dbContext.Users
                    .Skip((requestDto.PageNo.Value - 1) * requestDto.PageSize.Value)
                    .Take(requestDto.PageSize.Value)
                    .Select(u => new UserResponseDto
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email
                    })
                    .ToList();


                return new AutoResponseDto<List<UserResponseDto>>
                {
                    Result = result
                };
            }
            else
            {
                var result = _dbContext.Users.Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList();

                return new AutoResponseDto<List<UserResponseDto>>
                {
                    Result = result
                };

            }

        }

        public AutoResponseDto<UserResponseDto> Get(int id)
        {
            // Logic to fetch a User item by its ID from the database
            var result = _dbContext.Users.Find(id);

            if (result == null)
            {
                return new AutoResponseDto<UserResponseDto>
                {
                    Success = false,
                    Message = "No record found"
                };
            }

            // get toDos
            result.ToDos = _dbContext.ToDos.Where(x => x.Users.Contains(result)).ToList();

            var toDos = new List<ToDoDto.ToDoUpdateRequestDto>();
            foreach (var todo in result.ToDos)
            {
                toDos.Add(new ToDoDto.ToDoUpdateRequestDto
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description
                });
            }

            return new AutoResponseDto<UserResponseDto>
            {
                Result = new UserResponseDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Email = result.Email,
                    ToDos = toDos
                    
                }
            };
        }

        public AutoResponseDto<UserResponseDto> AssignToDos(int userId, List<int> toDoIds)
        {
            // Logic to assign ToDos to a User
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return new AutoResponseDto<UserResponseDto>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            foreach (var todoId in toDoIds)
            {
                // find that ToDo
                var todo = _dbContext.ToDos.Find(todoId);
                
                // if ToDo is not found, skip
                if (todo == null)
                {
                    continue;
                }
                else if (user.ToDos == null)
                {
                    user.ToDos = new List<ToDo> { todo };
                }
                else
                {
                    user.ToDos.Add(todo);
                }
            }

            _dbContext.SaveChanges();



            return new AutoResponseDto<UserResponseDto>
            {
                Result = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email, 
                    ToDos = user.ToDos.Select(x => new ToDoDto.ToDoUpdateRequestDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description
                    }).ToList()
                }
            };
        }

        public AutoResponseDto<User> Add(UserAddRequestDto user)
        {
            // Logic to add a new User item to the database
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return new AutoResponseDto<User>
            {
                Result = newUser
            };
        }

    }
}
