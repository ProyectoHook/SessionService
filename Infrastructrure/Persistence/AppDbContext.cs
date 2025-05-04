using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Session> Session { get; set; }
        public DbSet<Participant> Participant { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.idSession);
                entity.Property(e => e.idSession).ValueGeneratedOnAdd();

            });
            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(e => e.idParticipant);
                entity.Property(e => e.idParticipant).ValueGeneratedOnAdd();


                entity.HasOne(s => s.session)
                .WithMany(l => l.Participants)
                .HasForeignKey(n => n.idSession)
                .OnDelete(DeleteBehavior.NoAction);

            });
        }
    }
}
