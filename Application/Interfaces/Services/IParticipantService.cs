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
        Task<Participant> CreateParticipant(Participant request);
        Task<Participant> GetParticipantById(int id);
        Task<List<Participant>> GetAllParticipants();

        Task DeleteParticipant(DeleteParticipant request);
    }
}
