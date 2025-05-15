using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries
{
    public interface IAccesCodeQuery
    {
        Task<AccesCode> GetById(int id);
        Task<List<AccesCode>> GetAll();
    }
}
