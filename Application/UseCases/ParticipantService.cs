using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Interfaces.Queries;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
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

        public ParticipantService(IParticipantCommand participantCommand, IParticipantQuery participantQuery, ISessionQuery sessionQuery)
        {
            _participantCommand = participantCommand;
            _participantQuery = participantQuery;
            _sessionQuery = sessionQuery;
        }

        public async Task<createParticipantResponse> CreateParticipant(CreateParticipantRequest request)
        {
            Guid sesion_id = request.idSession;
            var sesion_db = await _sessionQuery.GetById(sesion_id);
            
           

            //Comprobación de la existencia de la sesión
            if (sesion_db == null) { throw new ExceptionNotFound("Sesión no encontrada"); }

            var response = new createParticipantResponse() { presentationId = sesion_db.presentation_id, message = "Usuario añadido"};
            

            //Comprobación del estado de la sesión
            if (sesion_db.active_status == false) { throw new ExceptionBadRequest("La sesión no se encuentra activa"); }

            //Comprobar que el mismo usuario no se vuelva a unir
            var users = await _participantQuery.GetAll();
            users = users.Where(c => c.idUser == request.idUser).ToList();
            if (users.Count > 0) {
                response.message = "El usuario ya se encuentra registrado como participante. Devolviendo presentationId";
                return response; 
            }

            var participant = new Domain.Entities.Participant()
            { 
            idUser = request.idUser,
            connectionStart = DateTime.Now,
            activityStatus = true,
            idSession = request.idSession
            };

            await _participantCommand.Create(participant);

            return response;
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
