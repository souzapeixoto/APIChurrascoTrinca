using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Usuario, RegisterUser>().ReverseMap();
            CreateMap<Usuario, DTOUsuario>().ReverseMap();

        }
    }
}
