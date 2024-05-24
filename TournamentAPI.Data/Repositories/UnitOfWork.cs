using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentAPIContext _context;
        private ITournamentRepository _tournamentRepository;
        private IGameRepository _gameRepository;

        public UnitOfWork(TournamentAPIContext context)
        {
            _context = context;
            _tournamentRepository = new TournamentRepository(_context);
            _gameRepository = new GameRepository(_context);
        }
        public ITournamentRepository TournamentRepository => _tournamentRepository;

        public IGameRepository GameRepository => _gameRepository;

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
 
    }
}
