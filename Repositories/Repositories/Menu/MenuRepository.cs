using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces;
using Persistence;

namespace Repositories.Repositories
{
    public class MenuRepository :BaseRepository<Data.Entities.Menu.Menu> , IMenuRepository
    {
        private readonly Context _context;
        public MenuRepository(Context context) : base(context)
        {
            _context = context;
        }

    }
}
