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

        public Task<CreateSessionResponse> CloseSession(SessionRequest request)
        {
            throw new NotImplementedException();
        }


        public async Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            var _accesCode = Guid.NewGuid();
            var _session = new Session
            {
                acces_code = _accesCode,
                description = request.description,
                interation_count = 0,
                active_status = true,
                max_participants = request.max_participants,
                start_time = DateTime.Now,
                presentation_id = request.presentation_id
            };
            await _sessionCommand.Create(_session);

            CreateSessionResponse response = new CreateSessionResponse()
            {
                idSession = _session.idSession,
                acces_code = _session.acces_code
            };
            return response;
        }

        public Task<List<Session>> GetAllSessions()
        {
            throw new NotImplementedException();
        }
    }
}




