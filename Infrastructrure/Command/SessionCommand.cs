using Application.Interfaces;
using Application.Interfaces.Commands;
using Domain.Entities;
using Infrastructrure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrure.Command
{
    public class SessionCommand : ISessionCommand
    {
        private readonly AppDbContext _context;

        public SessionCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Session> Create(Session session)
        {
            _context.Session.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public Task Delete(Session session)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Session session)
        {
            _context.Session.Update(session);
            await _context.SaveChangesAsync();
        }
    }
}
