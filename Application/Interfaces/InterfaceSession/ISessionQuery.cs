using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceSession
{
    public interface ISessionQuery
    {
        Task<Session> GetSessionById(int id);
        Task<List<Session>> GetAllSession();
    }
}
