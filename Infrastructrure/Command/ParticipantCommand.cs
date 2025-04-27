using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.InterfaceSession;
using Domain.Entities;

namespace Infrastructrure.Command
{
    public class ParticipantCommand : IParticipantCommand
    {
        public Task<Participant> CreateSession_activity(Participant session)
        {
            throw new NotImplementedException();
        }

        public Task<Participant> UpdateSession_activity(Participant session, int id)
        {
            throw new NotImplementedException();
        }
    }
}
