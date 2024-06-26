﻿using System.Collections.Generic;
using TodoApi.Entities;
using static TodoApi.Services.Dtos.ToDoDto;
using static TodoApi.Services.Dtos.CommonDto;
using TodoApi.Services.Dtos;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        AutoResponseDto<List<ToDo>> GetAll(PaginationDto requestDto);
        AutoResponseDto<ToDoResponseDto> GetById(int id);
        AutoResponseDto<ToDo> Create(ToDoAddRequestDto requestDto);
        AutoResponseDto<ToDo> Update(ToDoUpdateRequestDto requestDto);
        AutoResponseDto<List<ToDo>> Delete(int id);
        AutoResponseDto<List<ToDo>> GetChildrenOfToDo(int id);
    }
}