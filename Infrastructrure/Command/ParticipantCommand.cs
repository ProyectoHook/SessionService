using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.InterfaceSession;
using Domain.Entities;
using Infrastructrure.Persistence;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace Infrastructrure.Command
{
    public class ParticipantCommand : IParticipantCommand
    {
        private readonly AppDbContext _context;

        public ParticipantCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Participant> CreateSession_activity(Participant participant)
        {
            _context.Participant.AddAsync(participant);
            await _context.SaveChangesAsync();
            return participant;
        }
    }
}
