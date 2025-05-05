using Application.Exceptions;
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

        public async Task<bool> EndSession(int id)
        {
            var result = await _sessionQuery.GetById(id);

            if (result == null) { throw new ExceptionNotFound("Session no encontrada"); }

            result.active_status = false;

            await _sessionCommand.Update(result);
            
            return true;
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
                presentation_id = request.presentation_id,
                created_by = request.user_id

                
            };
            await _sessionCommand.Create(_session);

            CreateSessionResponse response = new CreateSessionResponse()
            {
                idSession = _session.idSession,
                acces_code = _session.acces_code
            };
            return response;
        }

        public async Task<List<GetSessionResponse>> GetAllSessions()
        {
            var results = await _sessionQuery.GetAll();

            List<GetSessionResponse> response = new List<GetSessionResponse>();

            foreach (var result in results)
            {
                var temp = new GetSessionResponse()
                {
                    idSession = result.idSession,
                    acces_code = result.acces_code,
                    description = result.description,
                    interation_count = result.interation_count,
                    active_status = result.active_status,
                    max_participants = result.max_participants,
                    start_time = result.start_time,
                    presentation_id = result.presentation_id
                };
                response.Add(temp);
            }

            return response;
        }
    }
}




