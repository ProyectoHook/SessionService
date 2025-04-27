using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.InterfaceSession;
using Domain.Entities;

namespace Infrastructrure.Query
{
    public class ParticipantQuery : IParticipantQuery
    {
        public Task<List<Participant>> GetAllSession_activity()
        {
            throw new NotImplementedException();
        }

        public Task<Participant> GetSession_activityById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
