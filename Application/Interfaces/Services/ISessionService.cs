using Application.Request;
using Application.Response;

namespace Application.Interfaces.Services
{
    public interface ISessionService
    {
        Task<CreateSessionResponse> CreateSession(CreateSessionRequest request);
        Task<bool> EndSession(Guid id);
        Task<List<GetSessionResponse>> GetAllSessions();
        Task<GetSessionResponse> GetSessionByAccessCode(string accessCode);
        Task<GetParticipantResponse> Join(Guid sessionId, Guid userId);
        Task<CreateSessionResponse> UpdateCurrentSlideBySessionId(Guid sessionId, int currentSlide);
    }
}
