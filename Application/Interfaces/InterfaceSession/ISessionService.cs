
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceSession
{
    public interface ISessionService
    {
        Task<Session> CreateSession(Session newSession);
        Task<Session> UpdateSession(Session oldSession, int id);
        Task<Session> GetSessionById(int id);
        Task<List<Session>> GetAllSession();
    }
}
