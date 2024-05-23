using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Data.Data;


namespace TournamentAPI.Data
{
    public class TournamentAPIContextFactory : IDesignTimeDbContextFactory<TournamentAPIContext>
    {
        
            public TournamentAPIContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TournamentAPIContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TournamentDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");

                return new TournamentAPIContext(optionsBuilder.Options);
            }
        

    }
}
