using Application.Request;
using Application.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<CreateSessionResponse,CreateParticipantRequest>();
            CreateMap<Session, GetSessionResponse>();
            CreateMap<Participant, GetParticipantResponse>();
            CreateMap<Session, CreateSessionResponse>();
            
        }
    }
}
