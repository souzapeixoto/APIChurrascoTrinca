using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Infrastructure.Mapper
{
    public class ChurrascoProfile : Profile
    {
        public ChurrascoProfile()
        {
            CreateMap<Churrasco, DTOChurrasco>().ReverseMap();
        }
    }
}
