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

        public ParticipantService(IParticipantCommand participantCommand, IParticipantQuery participantQuery)
        {
            _participantCommand = participantCommand;
            _participantQuery = participantQuery;
        }

        public async Task<bool> CreateParticipant(CreateParticipantRequest request)
        {
            var participant = new Domain.Entities.Participant()
            { 
            idUser = request.idUser,
            connectionStart = DateTime.Now,
            activityStatus = true,
            connectionId = request.connectionId,
            idSession = request.idSession
            };

            await _participantCommand.Create(participant);
            return true;
        }

        public Task<List<GetParticipantResponse>> GetAllParticipants()
        {
            throw new NotImplementedException();
        }

        public async Task<GetParticipantResponse> GetByIdParticipant(int id)
        {
            var result = await _participantQuery.GetById(id);
            if (result == null) { throw new ExceptionBadRequest("Participante no encontrado."); }
            var participant = new GetParticipantResponse()
            {
                activityStatus = result.activityStatus,
                connectionId = result.connectionId,
                idSession = result.idSession,
                idParticipant = result.idParticipant,
                connectionStart = result.connectionStart,
                idUser = result.idUser,
            };   

            return participant;
        }
    }
}
