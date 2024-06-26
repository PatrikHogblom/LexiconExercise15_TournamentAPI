﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetAsync(int id);
        Task<IEnumerable<Game>> GetAsyncByTitle(string title);
        Task<bool> AnyAsync(int id);
        void Add(Game game);
        public void Update(Game game);
        public void Remove(Game game);
    }
}
