using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Interfaces.Queries;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases
{
    public class SessionService : ISessionService
    {
        private readonly ISessionQuery _sessionQuery;
        private readonly ISessionCommand _sessionCommand;
        private readonly IAccesCodeCommand _accesCodeCommand;
        private readonly IAccesCodeQuery _accesCodeQuery;
        private readonly IMapper _mapper;

        public SessionService(ISessionQuery sessionQuery, ISessionCommand sessionCommand, IAccesCodeCommand accesCodeCommand, IAccesCodeQuery accesCodeQuery, IMapper mapper)
        {
            _sessionQuery = sessionQuery;
            _sessionCommand = sessionCommand;
            _accesCodeCommand = accesCodeCommand;
            _accesCodeQuery = accesCodeQuery;
            _mapper = mapper;
        }

        public async Task<bool> EndSession(int id)
        {
            var result = await _sessionQuery.GetById(id);

            if (result == null) { throw new ExceptionNotFound("Session no encontrada"); }

            if (result.active_status == false) { throw new ExceptionBadRequest("Sesión ya se encuentra finalizada"); }

            result.active_status = false;

            //Seteo el accescode en false para poder volver a usarlo
            var sessionAccesCode = await _accesCodeQuery.GetById(result.access_code.Value);
            sessionAccesCode.status = false;
            await _accesCodeCommand.Update(sessionAccesCode);

            result.access_code = null;

            await _sessionCommand.Update(result);
            
            return true;
        }


        public async Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            var acces_codes = await _accesCodeQuery.GetAll();
            var availableCode = acces_codes.FirstOrDefault(ac => ac.status == false);


            if (availableCode == null)
            {

                Guid newGuid = Guid.NewGuid();
                string guidString = newGuid.ToString("N"); // sin guiones
                string newCode = guidString.Substring(0, 6).ToUpper();

                availableCode = new AccesCode()
                {
                    status = true,
                    code = newCode
                };
                availableCode = await _accesCodeCommand.Create(availableCode);

            }

            else 
            {
                availableCode.status = true;
                await _accesCodeCommand.Update(availableCode);
            }



            var _session = new Session
            {
                access_code = availableCode.idCode,
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
                acces_code = availableCode.code,
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
                    acces_code = result.access_code,
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

        public async Task<CreateSessionResponse> UpdateCurrentSlide(int sessionId, int slideIndex)
        {

            Session session =  await _sessionQuery.GetById(sessionId);

            if (session != null)
            {
                session.currentSlide = slideIndex;
                await _sessionCommand.Update(session);
            }

            CreateSessionResponse response = _mapper.Map<CreateSessionResponse>(session);
            
            return response;

        }

        public async Task<GetSessionResponse> GetSessionByAccessCode(string accessCode)
        {

            var sessions = (await _sessionQuery.GetAll());

            var session = sessions.FirstOrDefault(s => s.AccesCode != null && s.AccesCode.code == accessCode);

            if (session == null) { throw new ExceptionNotFound("Session no encontrada"); }

            var sessionDto = new GetSessionResponse() {
                idSession = session.idSession,
                acces_code = session.access_code,
                description = session.description,
                interation_count = session.interation_count,
                max_participants = session.max_participants,
                presentation_id=session.presentation_id,
                start_time = session.start_time,
                active_status = session.active_status
             };
            return sessionDto;
        }
    }
}




