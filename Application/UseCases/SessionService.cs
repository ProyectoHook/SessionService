using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.InterfaceSession;
using Domain.Entities;

namespace Application.UseCases
{
    public class SessionService : ISessionService
    {
        public Task<Session> CreateSession(Session newSession)
        {
            throw new NotImplementedException();
        }

        public Task<List<Session>> GetAllSession()
        {
            throw new NotImplementedException();
        }

        public Task<Session> GetSessionById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Session> UpdateSession(Session oldSession, int id)
        {
            throw new NotImplementedException();
        }
    }
}
