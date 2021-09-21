using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Infrastructure.Mapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Usuario, RegisterUser>().ReverseMap();
          
        }
    }
}
