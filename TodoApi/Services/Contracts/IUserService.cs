using System.Collections.Generic;
using TodoApi.Entities;
using static TodoApi.Services.Dtos.UserDto;
using static TodoApi.Services.Dtos.CommonDto;
using TodoApi.Services.Dtos;

namespace TodoApi.Services
{
    public interface IUserService
    {
        AutoResponseDto<List<UserResponseDto>> GetAll(PaginationDto requestDto);
        AutoResponseDto<UserResponseDto> Get(int id);
        AutoResponseDto<UserResponseDto> AssignToDos(int userId, List<int> toDoIds);

        AutoResponseDto<User> Add(UserAddRequestDto user);
        AutoResponseDto<UserResponseDto> Update(UserUpdateRequestDto user);
        AutoResponseDto<string> Delete(int id);
        

    }
}