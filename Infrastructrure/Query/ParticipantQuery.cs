using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Queries;
using Domain.Entities;
using Infrastructrure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructrure.Query
{
    public class ParticipantQuery : IParticipantQuery
    {
        private readonly AppDbContext _context;

        public ParticipantQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Participant>> GetAll()
        {
            return await _context.Participant.ToListAsync();
        }

        public async Task<Participant> GetById(int id)
        {
            return await _context.Participant.FindAsync(id);
        }
        
    }
}
