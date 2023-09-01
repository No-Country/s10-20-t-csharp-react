using AutoMapper;
using quejapp.DTO;
using quejapp.Models;

namespace s10.Back.Handler;

public class AutoMapperHandler : Profile
{
    public AutoMapperHandler()
    {
        CreateMap<Category, CategoryDTO>();
    }
}
