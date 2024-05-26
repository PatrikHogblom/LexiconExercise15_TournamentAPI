using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament> GetAsync(int id);
        Task<Tournament> GetAsyncWithGames(int id);
        Task<bool> AnyAsync(int id);
        void Add(Tournament tournament);
        public void Update(Tournament tournament);
        public void Remove(Tournament tournament);
    }
}
