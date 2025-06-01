using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Interfaces.Queries;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace Application.UseCases
{
    public class SessionService : ISessionService
    {
        private readonly ISessionQuery _sessionQuery;
        private readonly ISessionCommand _sessionCommand;
        private readonly IParticipantService _participantService;
        private readonly IPresentationServiceClient _presentationServiceClient;
        private readonly IMapper _mapper;

        public SessionService(ISessionQuery sessionQuery, ISessionCommand sessionCommand,
                              IMapper mapper, IParticipantService participantService,
                              IPresentationServiceClient presentationServiceClient)
        {
            _sessionQuery = sessionQuery;
            _sessionCommand = sessionCommand;
            _mapper = mapper;
            _participantService = participantService;
            _presentationServiceClient = presentationServiceClient;
        }

        public async Task<bool> EndSession(Guid id)
        {
            var result = await _sessionQuery.GetById(id);

            if (result == null) { throw new ExceptionNotFound("Session no encontrada"); }

            result.active_status = false;

            await _sessionCommand.Update(result);
            
            return true;
        }


        public async Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            string accessCode; 

            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";

            StringBuilder primeraParte = new StringBuilder();
            StringBuilder segundaParte = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int indiceLetras = random.Next(letras.Length);
                int indiceNumeros = random.Next(numeros.Length);
                primeraParte.Append(letras[indiceLetras]);
                segundaParte.Append(numeros[indiceNumeros]);
            }

            accessCode = primeraParte.Append(segundaParte).ToString();

            var _session = new Session
            {
                access_code = accessCode,
                description = request.description,
                interation_count = 0,
                active_status = true,
                max_participants = request.max_participants,
                start_time = DateTime.Now,
                presentation_id = request.presentation_id,
                created_by = request.user_id,
                currentSlide = 1                
            };

            await _sessionCommand.Create(_session);

            PresentationResponseDTO presentationResponse = await _presentationServiceClient.GetPresentationByIdAsync(request.presentation_id);

            CreateSessionResponse response = new CreateSessionResponse()
            {
                idSession = _session.idSession,
                access_code = _session.access_code,
                presentation = presentationResponse
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
                    access_code = result.access_code,
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

        public async Task<GetSessionResponse> GetSessionByAccessCode(string accessCode)
        {
            Session results = await _sessionQuery.GetByAccessCode(accessCode);
            PresentationResponseDTO presentation;

            if (results != null)
            {
                presentation = await _presentationServiceClient.GetPresentationByIdAsync(results.presentation_id);
                GetSessionResponse response = _mapper.Map<GetSessionResponse>(results);
                response.presentation = presentation;
                return response;
            }

            return null;
        }

        public async Task<GetParticipantResponse> Join(Guid sessionId, Guid userId)
        {

            CreateParticipantRequest newParticipant = new CreateParticipantRequest
            {
                idSession = sessionId,
                idUser = userId
            };

            Participant results = await _participantService.CreateParticipant(newParticipant);

            return _mapper.Map<GetParticipantResponse>(results);
        }

        public async Task<CreateSessionResponse> UpdateCurrentSlideBySessionId(Guid sessionId, int currentSlide)
        {

            Session session = await _sessionQuery.GetById(sessionId);

            if (session != null)
            {
                session.currentSlide = currentSlide;
                await _sessionCommand.Update(session);
            }

            CreateSessionResponse response = _mapper.Map<CreateSessionResponse>(session);
            return response;

        }

    }
}




