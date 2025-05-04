using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISessionService
    {
        Task<CreateSessionResponse> CreateSession(CreateSessionRequest request);
        Task<CreateSessionResponse> CloseSession(SessionRequest request);
        Task<List<Session>> GetAllSessions();

    }
}
