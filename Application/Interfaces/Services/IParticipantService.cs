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
        Task<Participant> CreateSession_activity(Participant request);
        Task<Participant> GetSession_activityById(int id);
        Task<List<Participant>> GetAllSession_activity();

        Task Delete(DeleteParticipant request);
    }
}
