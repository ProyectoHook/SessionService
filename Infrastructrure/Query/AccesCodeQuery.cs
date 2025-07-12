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
    public class AccesCodeQuery : IAccesCodeQuery
    {
        private readonly AppDbContext _context;

        public AccesCodeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccesCode>> GetAll()
        {
            return await _context.AccesCode.ToListAsync();
        }

        public async Task<AccesCode> GetById(int id)
        {
            return await _context.AccesCode.FindAsync(id);
        }
    }
}
