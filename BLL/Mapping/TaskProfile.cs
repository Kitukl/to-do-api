using AutoMapper;
using BLL.DTOs;
using DLA.Entities;

namespace BLL.Mapping;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskDTOs>();
    }
}