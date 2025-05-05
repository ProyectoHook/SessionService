using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands
{
    public interface IParticipantCommand
    {
        Task<Participant> Create(Participant participant);
          
        Task Delete(Participant participant);

        Task Update(Participant participant);

    }
}
