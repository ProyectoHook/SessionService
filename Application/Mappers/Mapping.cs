using Application.Request;
using Application.Response;
using AutoMapper;

namespace Application.Mappers
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<CreateSessionResponse,CreateParticipantRequest>();
            
        }
    }
}
