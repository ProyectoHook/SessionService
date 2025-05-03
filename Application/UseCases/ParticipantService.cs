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

        public Task<Participant> CreateSession_activity(Participant request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(DeleteParticipant request)
        {
            //
            return null;
        }

        public Task<List<Participant>> GetAllSession_activity()
        {
            throw new NotImplementedException();
        }

        public Task<Participant> GetSession_activityById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
