using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands
{
    public interface ISessionCommand
    {
        Task<Session> Create(Session request);

        Task Update(Session session);

        Task Delete(Session session);

    }
}
