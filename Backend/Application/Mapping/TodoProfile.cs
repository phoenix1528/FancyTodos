using AutoMapper;
using Domain;
using Shared.Dtos.Todos;

namespace Application.Mapping
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<CreateTodoDto, Todo>();
            CreateMap<EditTodoDto, Todo>();
        }
    }
}
