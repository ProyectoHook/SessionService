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
        private readonly IAccesCodeCommand _accesCodeCommand;
        private readonly IAccesCodeQuery _accesCodeQuery;

        public SessionService(ISessionQuery sessionQuery, ISessionCommand sessionCommand, IAccesCodeCommand accesCodeCommand, IAccesCodeQuery accesCodeQuery)
        {
            _sessionQuery = sessionQuery;
            _sessionCommand = sessionCommand;
            _accesCodeCommand = accesCodeCommand;
            _accesCodeQuery = accesCodeQuery;
        }

        public async Task<bool> EndSession(int id)
        {
            var result = await _sessionQuery.GetById(id);

            if (result == null) { throw new ExceptionNotFound("Session no encontrada"); }

            if (result.active_status == false) { throw new ExceptionBadRequest("Sesión ya se encuentra finalizada"); }

            result.active_status = false;

            //Seteo el accescode en false para poder volver a usarlo
            var sessionAccesCode = await _accesCodeQuery.GetById(result.acces_code.Value);
            sessionAccesCode.status = false;
            await _accesCodeCommand.Update(sessionAccesCode);

            result.acces_code = null;

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
                acces_code = availableCode.idCode,
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




