using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Infrastructure.Mapper
{
    public class OpcaoProfile : Profile
    {
        public OpcaoProfile()
        {
            CreateMap<Opcao, DTOOpcao>().AfterMap((src, dest) => dest.IdChurrasco = src.Churrasco?.Id ?? 0).ReverseMap(); ;

        }
    }
}