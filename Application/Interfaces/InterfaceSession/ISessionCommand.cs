using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceSession
{
    public interface ISessionCommand
    {
        Task<Session> CreateSession(Session request);
        Task UpdateSession(Session session);
        
    }
}
