
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceSession
{
    public interface IParticipantService
    {
        Task<Participant> CreateSession_activity(Participant request);
        Task<Participant> UpdateSession_activity(Participant request, int id);
        Task<Participant> GetSession_activityById(int id);
        Task<List<Participant>> GetAllSession_activity();
    }
}
