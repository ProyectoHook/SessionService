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
        Task<Session> CreateSession(Session newSession);
        Task<Session> UpdateSession(Session oldSession,int id);
    }
}
