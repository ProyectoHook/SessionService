using Application.Interfaces;
using Application.Interfaces.InterfaceSession;
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
        public Task<Session> CreateSession(Session newSession)
        {
            throw new NotImplementedException();
        }

        public Task<Session> UpdateSession(Session oldSession, int id)
        {
            throw new NotImplementedException();
        }
    }
}
