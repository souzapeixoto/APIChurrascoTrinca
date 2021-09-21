using Domain.DTO;
using Domain.Entities;
using AutoMapper;

namespace Infrastructure.Mapper
{
    public class ParticipanteProfile : Profile
    {
        public ParticipanteProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Participante, DTOParticipante>().AfterMap((src, dest) => dest.IdChurrasco = src.Churrasco?.Id ?? 0).ReverseMap();;

        }
    }
}
