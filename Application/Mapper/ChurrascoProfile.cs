using Application.DTO;
using Application.InputModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class ChurrascoProfile : Profile
    {
        public ChurrascoProfile()
        {
            CreateMap<Churrasco, DTOChurrasco>().ReverseMap();
            CreateMap<Churrasco, ChurrascoCreateInputModel>().ReverseMap();
            CreateMap<Churrasco, ChurrascoUpdateInputModel>().ReverseMap();
        }
    }
}
