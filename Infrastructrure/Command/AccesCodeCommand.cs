using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Commands;
using Domain.Entities;
using Infrastructrure.Persistence;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace Infrastructrure.Command
{
    public class AccesCodeCommand : IAccesCodeCommand
    {
        private readonly AppDbContext _context;

        public AccesCodeCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AccesCode> Create(AccesCode acces_code)
        {
            _context.AccesCode.AddAsync(acces_code);
            await _context.SaveChangesAsync();
            return acces_code;
        }

        public async Task Delete(AccesCode acces_code)
        {
            _context.AccesCode.Remove(acces_code);
            await _context.SaveChangesAsync();
        }

        public async Task Update(AccesCode acces_code)
        {
            _context.AccesCode.Update(acces_code);
            await _context.SaveChangesAsync();
        }
    }
}
