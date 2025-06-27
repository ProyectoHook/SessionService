using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IParticipantService
    {
        Task<GetParticipantResponse> CreateParticipant(CreateParticipantRequest request);
        Task<GetParticipantResponse> GetByIdParticipant(int id);
        Task<List<GetParticipantResponse>> GetAllParticipants();
    }
}
