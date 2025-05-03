using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries
{
    public interface IParticipantQuery
    {
        Task<Participant> GetSession_activityById(int id);
        Task<List<Participant>> GetAllSession_activity();
    }
}
