using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Commands;
using Application.Interfaces.Queries;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCases
{
    public class SessionService : ISessionService
    {
        private readonly ISessionQuery _sessionQuery;
        private readonly ISessionCommand _sessionCommand;

        public SessionService(ISessionQuery sessionQuery, ISessionCommand sessionCommand)
        {
            _sessionQuery = sessionQuery;
            _sessionCommand = sessionCommand;
        }

        public Task<SessionResponse> CloseSession(SessionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SessionResponse> CreateSession(SessionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<Session>> GetAllSessions()
        {
            throw new NotImplementedException();
        }
    }
}




