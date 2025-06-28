using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Interfaces.Queries;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantCommand _participantCommand;
        private readonly IParticipantQuery _participantQuery;
        private readonly ISessionQuery _sessionQuery;
        private readonly IMapper _mapper;

        public ParticipantService(IParticipantCommand participantCommand, IParticipantQuery participantQuery, ISessionQuery sessionQuery,IMapper mapper)
        {
            _participantCommand = participantCommand;
            _participantQuery = participantQuery;
            _sessionQuery = sessionQuery;
            _mapper = mapper;
        }

        public async Task<GetParticipantResponse> CreateParticipant(CreateParticipantRequest request)
        {
            Guid sesion_id = request.idSession;
            Session sesion_db = await _sessionQuery.GetById(sesion_id);

            GetParticipantResponse getParticipantResponse;

            //Comprobación de la existencia de la sesión
            if (sesion_db == null) { throw new ExceptionNotFound("Sesión no encontrada"); }

            //Comprobación del estado de la sesión
            if (sesion_db.active_status == false) { throw new ExceptionBadRequest("La sesión no se encuentra activa"); }

            
            List<Participant> listaDeParticipantesDeLaSesion = await _participantQuery.GetAllBySessionId(sesion_id);

            //var response = new createParticipantResponse() {
            //    presentationId = sesion_db.presentation_id,
            //    message = "Usuario añadido"};
                      

            //Comprobar que el mismo usuario no se vuelva a unir
            //List<Participant> users = await _participantQuery.GetAll();

            if (listaDeParticipantesDeLaSesion.Any(p => p.idUser == request.idUser))
            {
                //response.message = "El usuario ya se encuentra registrado como participante. Devolviendo presentationId";
                Participant participant = listaDeParticipantesDeLaSesion.Where(p => p.idUser == request.idUser).First();
                getParticipantResponse = _mapper.Map<GetParticipantResponse>(participant);
                return getParticipantResponse;
            }
            else
            {
                var participant = new Domain.Entities.Participant()
                {
                    idUser = request.idUser,
                    connectionStart = DateTime.Now,
                    activityStatus = true,
                    idSession = request.idSession
                };

                await _participantCommand.Create(participant);

                getParticipantResponse = _mapper.Map<GetParticipantResponse>(participant);
                return getParticipantResponse;
            }

        }
    
    

        public async Task<List<GetParticipantResponse>> GetAllParticipants()
        {
            var results = await _participantQuery.GetAll();

            List<GetParticipantResponse> participants = new List<GetParticipantResponse>();

            foreach (var result in results) 
            {
                var temp = new GetParticipantResponse() 
                {
                    activityStatus = result.activityStatus,
                    connectionId = result.connectionId,
                    SessionId = result.idSession,
                    idParticipant = result.idParticipant,
                    connectionStart = result.connectionStart,
                    idUser = result.idUser,
                };

                participants.Add(temp);
            }

            return participants;
        }

        public async Task<GetParticipantResponse> GetByIdParticipant(int id)
        {
            var result = await _participantQuery.GetById(id);
            if (result == null) { throw new ExceptionNotFound("Participante no encontrado."); }
            var participant = new GetParticipantResponse()
            {
                activityStatus = result.activityStatus,
                connectionId = result.connectionId,
                SessionId = result.idSession,
                idParticipant = result.idParticipant,
                connectionStart = result.connectionStart,
                idUser = result.idUser,
            };   

            return participant;
        }
    }
}
