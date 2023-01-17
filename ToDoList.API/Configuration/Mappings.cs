using AutoMapper;
using ToDoList.API.Core.Commands.InsertTodoItem;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;
using ToDoList.API.Infrastructure.Data.Entities;

namespace ToDoList.API.Configuration
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<UserRegistrationRequest, AppUser>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.UserName));

            CreateMap<CreateToDoRequest, ToDo>();
            CreateMap<InsertTodoItemCommand, ToDo>();
            CreateMap<UpdateToDoRequest, ToDo>();
            CreateMap<ToDo, ToDoItemResponse>();

            CreateMap<ToDo, TodoItem>();
            CreateMap<TodoItem, ToDo>();
            
            CreateMap<User, AppUser>();
            CreateMap<AppUser, User>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email));;
        }
    }
}
