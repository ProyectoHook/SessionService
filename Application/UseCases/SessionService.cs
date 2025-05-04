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

       
        public async Task<SessionResponse> CreateSession(SessionRequest request)
        {

            Session _session = new Session
            {
                acces_code = request.acces_code,
                idParticipant = request.idParticipant,
                description = request.description,
                interation_count = request.interation_count,
                active_status = request.active_status,
                max_participants = request.max_participants,
                start_time = request.start_time,
                end_time = request.end_time,    
                presentation_id = request.presentation_id,
            };

            await _sessionCommand.Create(_session);

            SessionResponse sessionResponse = new SessionResponse
            {
                idSession = _session.idSession,
                acces_code = _session.acces_code,
                idParticipant = _session.idParticipant,
                description = _session.description,
                interation_count = _session.interation_count,
                active_status = _session.active_status,
                max_participants = _session.max_participants,
                start_time = _session.start_time,
                end_time = _session.end_time,
                presentation_id = _session.presentation_id,
            };

            return sessionResponse;
        }

        public Task<List<Session>> GetAllSession()
        {
            throw new NotImplementedException();
        }

        public Task<Session> GetSessionById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSession(Session session)
        {
            await _sessionCommand.Update(session);
        }

    }
}




