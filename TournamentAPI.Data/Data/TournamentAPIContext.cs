using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class TournamentAPIContext : DbContext
    {
        public TournamentAPIContext(DbContextOptions<TournamentAPIContext> options) : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tournament>()
                .HasMany(g => g.Games)
                .WithOne(t => t.Tournament)
                .HasForeignKey(t => t.TournamentId);
        }

    }
}
