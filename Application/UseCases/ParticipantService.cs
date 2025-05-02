using Application.Exceptions;
using Application.Interfaces.InterfaceSession;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ParticipantService : IParticipantService
    {
        public Task<Participant> CreateSession_activity(Participant request)
        {
            throw new NotImplementedException();
        }

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
