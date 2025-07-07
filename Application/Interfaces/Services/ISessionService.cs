using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISessionService
    {
        Task<CreateSessionResponse> CreateSession(CreateSessionRequest request);
        Task<bool> EndSession(Guid id);
        Task<GetParticipantResponse> Join(Guid sessionId, Guid userId);
        Task<List<GetSessionResponse>> GetAllSessions();
        Task<GetSessionResponse> GetSessionByAccessCode(string accessCode);
        Task<CreateSessionResponse> UpdateCurrentSlide(Guid sessionId, int slideIndex);
        Task<SessionDuration> GetDurationByGuid(Guid sessionId);

    }
}
