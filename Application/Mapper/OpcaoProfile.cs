using Application.DTO;
using Application.InputModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class OpcaoProfile : Profile
    {
        public OpcaoProfile()
        {
            CreateMap<Opcao, DTOOpcao>().AfterMap((src, dest) => dest.IdChurrasco = src.Churrasco?.Id ?? 0).ReverseMap();
            CreateMap<Opcao, ChurrascoCreateOpcaoInputModel>().ReverseMap();
            CreateMap<Opcao, ChurrascoUpdateOpcaoInputModel>().ReverseMap();

        }
    }
}