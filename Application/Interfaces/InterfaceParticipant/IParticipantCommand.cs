using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceSession
{
    public interface IParticipantCommand
    {
        Task<Participant> CreateSession_activity(Participant session);
        Task<Participant> UpdateSession_activity(Participant session,int id);
    }
}
