using Application.Interfaces;
using Application.Interfaces.Queries;
using Domain.Entities;
using Infrastructrure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrure.Query
{
    public class SessionQuery : ISessionQuery
    {
        private readonly AppDbContext _context;

        public SessionQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAll()
        {
            return await _context.Session.ToListAsync();
        }

        public async Task<Session> GetById(int id)
        {
            return await _context.Session.FindAsync(id);
        }
    }
}
