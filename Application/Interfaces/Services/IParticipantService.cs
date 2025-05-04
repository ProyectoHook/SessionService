using Application.Request;
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
        Task<bool> CreateParticipant(CreateParticipantRequest request);
        Task<Participant> GetByIdParticipant(int id);
        Task<List<Participant>> GetAllParticipants();
    }
}
