using Application.DTO;
using Application.InputModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class ConvidadoProfile : Profile
    {
        public ConvidadoProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Convidado, DTOConvidado>().AfterMap((src, dest) => dest.IdChurrasco = src.Churrasco?.Id ?? 0).ReverseMap();
            CreateMap<Convidado, ChurrascoCreateConvidadoInputModel>().ReverseMap();
            CreateMap<Convidado, ChurrascoUpdateConvidadoInputModel>().ReverseMap();

        }
    }
}
