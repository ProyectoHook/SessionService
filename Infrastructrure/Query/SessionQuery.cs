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

namespace Infrastructrure.Query
{
    public class SessionQuery : ISessionQuery
    {
        public Task<List<Session>> GetAllSession()
        {
            throw new NotImplementedException();
        }

        public Task<Session> GetSessionById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
