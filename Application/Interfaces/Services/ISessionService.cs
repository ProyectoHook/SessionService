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
        Task<SessionResponse> CreateSession(SessionRequest request);
        Task<Session> GetSessionById(int id);
        Task<List<Session>> GetAllSession();

        Task UpdateSession(Session session);
    }
}
