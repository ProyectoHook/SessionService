using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Interfaces.Queries;
using Application.Interfaces.Services;
using Application.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
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

        public Task<List<Participant>> GetAllParticipants()
        {
            throw new NotImplementedException();
        }

        public Task<Participant> GetByIdParticipant(int id)
        {
            throw new NotImplementedException();
        }
    }
}
