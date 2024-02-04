using AutoMapper;
using ToDoListApi.DTOs;
using ToDoListApi.Models;

namespace ToDoListApi
{
    public class MappingProfile
    {
        public class TaskItemProfile : Profile
        {
            public TaskItemProfile()
            {
                CreateMap<TaskItem, TaskItemDTO>(); // On peux custom, mais j'ai manqué d"inspiration 
                CreateMap<TaskItemDTO, TaskItem>();
            }
        }

    }
}
